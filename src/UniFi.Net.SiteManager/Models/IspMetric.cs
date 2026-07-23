using System.Text.Json.Serialization;

namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Represents ISP metrics for a site.
/// </summary>
/// <param name="MetricType">The metric interval type (5m or 1h).</param>
/// <param name="Periods">The metric data periods.</param>
/// <param name="HostId">Unique identifier of the host device.</param>
/// <param name="SiteId">Unique identifier of the site.</param>
public record IspMetric(
    string MetricType,
    IReadOnlyList<IspMetricPeriod> Periods,
    string HostId,
    string SiteId
);

/// <summary>
/// Represents the result of an ISP metrics query.
/// </summary>
/// <param name="Metrics">The ISP metrics for the queried sites.</param>
/// <param name="Message">An informational message about the query, if any.</param>
/// <param name="Status">The status of the query, if any.</param>
public record IspMetricsQueryResult(
    IReadOnlyList<IspMetric> Metrics,
    string? Message,
    string? Status
);

/// <summary>
/// Represents a single period of ISP metric data.
/// </summary>
/// <param name="Data">The metric data for the period.</param>
/// <param name="MetricTime">The time the metrics were recorded.</param>
/// <param name="Version">The metrics data version.</param>
public record IspMetricPeriod(
    IspMetricData Data,
    DateTimeOffset MetricTime,
    string Version
);

/// <summary>
/// Represents the metric data for a period.
/// </summary>
/// <param name="Wan">The WAN metrics.</param>
public record IspMetricData(
    IspMetricWan Wan
);

/// <summary>
/// Represents WAN metrics for a period.
/// </summary>
/// <param name="AvgLatency">Average latency in milliseconds.</param>
/// <param name="DownloadKbps">Download speed in kilobits per second.</param>
/// <param name="Downtime">Downtime in milliseconds.</param>
/// <param name="IspAsn">The ISP's autonomous system number.</param>
/// <param name="IspName">The ISP name.</param>
/// <param name="MaxLatency">Maximum latency in milliseconds.</param>
/// <param name="PacketLoss">Packet loss percentage.</param>
/// <param name="UploadKbps">Upload speed in kilobits per second.</param>
/// <param name="Uptime">Uptime in milliseconds.</param>
public record IspMetricWan(
    int AvgLatency,
    [property: JsonPropertyName("download_kbps")] int DownloadKbps,
    int Downtime,
    string IspAsn,
    string IspName,
    int MaxLatency,
    int PacketLoss,
    [property: JsonPropertyName("upload_kbps")] int UploadKbps,
    int Uptime
);
