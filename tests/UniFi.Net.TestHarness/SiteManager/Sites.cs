namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    private async Task DoSites(CancellationToken cancellationToken)
    {
        Clear();
        WriteHeading("Sites");

        var sites = await client.ListSitesAsync(cancellationToken: cancellationToken);

        foreach (var site in sites.Data)
        {
            WriteLine($"Site: {site.Meta.Name} ({site.SiteId})");
            WriteLine($"  Description: {site.Meta.Desc}");
            WriteLine($"  Host:        {site.HostId}");
            WriteLine($"  Gateway:     {site.Statistics.Gateway?.Shortname} ({site.Meta.GatewayMac})");
            WriteLine($"  Time zone:   {site.Meta.TimeZone}");
            WriteLine($"  ISP:         {site.Statistics.IspInfo?.Name} ({site.Statistics.IspInfo?.Organization})");
            WriteLine($"  Permission:  {site.Permission} (owner: {site.IsOwner})");
            WriteLine($"  Counts:      {String.Join(", ", site.Statistics.Counts.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
            WriteLine($"  Percentages: {String.Join(", ", site.Statistics.Percentages.Select(kvp => $"{kvp.Key}={kvp.Value}"))}");
            WriteLine();
        }

        WriteLine($"{sites.Data.Count} site(s) returned. Next token: {sites.NextToken ?? "(none)"}");

        PressAnyKeyToContinue();
    }
}
