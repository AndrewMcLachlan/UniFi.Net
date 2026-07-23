using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    private async Task DoIspMetrics(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var action = PromptOption(
                Title() + "\nISP Metrics",
                [
                    "Get 5-minute metrics (last 24 hours, by timestamp)",
                    "Get 1-hour metrics (duration 7d)",
                    "Query metrics for all sites (5-minute, last 24 hours)",
                ]);

            switch (action)
            {
                case 0:
                    return;
                case 1:
                    var byTimestamp = await client.GetIspMetricsAsync(
                        MetricInterval.FiveMinutes,
                        DateTimeOffset.UtcNow.AddDays(-1),
                        DateTimeOffset.UtcNow,
                        cancellationToken);
                    PrintMetrics(byTimestamp.Data);
                    PressAnyKeyToContinue();
                    break;
                case 2:
                    var byDuration = await client.GetIspMetricsAsync(MetricInterval.OneHour, "7d", cancellationToken);
                    PrintMetrics(byDuration.Data);
                    PressAnyKeyToContinue();
                    break;
                case 3:
                    var sites = await client.ListSitesAsync(cancellationToken: cancellationToken);
                    var queries = sites.Data.Select(site => new IspMetricsQuery
                    {
                        BeginTimestamp = DateTimeOffset.UtcNow.AddDays(-1),
                        EndTimestamp = DateTimeOffset.UtcNow,
                        HostId = site.HostId,
                        SiteId = site.SiteId,
                    }).ToList();

                    if (queries.Count == 0)
                    {
                        WriteLine("No sites found to query.");
                        PressAnyKeyToContinue();
                        break;
                    }

                    var result = await client.QueryIspMetricsAsync(MetricInterval.FiveMinutes, queries, cancellationToken);
                    WriteLine($"Status: {result.Data.Status ?? "(none)"}, Message: {result.Data.Message ?? "(none)"}");
                    PrintMetrics(result.Data.Metrics);
                    PressAnyKeyToContinue();
                    break;
            }
        }
    }

    private static void PrintMetrics(IReadOnlyList<IspMetric> metrics)
    {
        WriteLine("ISP Metrics");
        WriteLine("-----------");

        foreach (var metric in metrics)
        {
            WriteLine($"Host: {metric.HostId}");
            WriteLine($"  Site:    {metric.SiteId}");
            WriteLine($"  Type:    {metric.MetricType}, Periods: {metric.Periods.Count}");

            var latest = metric.Periods.OrderByDescending(p => p.MetricTime).FirstOrDefault();
            if (latest is not null)
            {
                var wan = latest.Data.Wan;
                WriteLine($"  Latest ({latest.MetricTime:yyyy-MM-dd HH:mm:ss}):");
                WriteLine($"    ISP:       {wan.IspName} (ASN {wan.IspAsn})");
                WriteLine($"    Download:  {wan.DownloadKbps} kbps, Upload: {wan.UploadKbps} kbps");
                WriteLine($"    Latency:   avg {wan.AvgLatency} ms, max {wan.MaxLatency} ms");
                WriteLine($"    Loss:      {wan.PacketLoss}%, Uptime: {wan.Uptime}, Downtime: {wan.Downtime}");
            }
            WriteLine();
        }

        WriteLine($"{metrics.Count} metric set(s) returned.");
    }
}
