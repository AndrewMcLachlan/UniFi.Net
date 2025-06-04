namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    public async Task DoSites(CancellationToken cancellationToken)
    {
        var sites = await client.ListSitesAsync(cancellationToken: cancellationToken);

        foreach (var site in sites.Data)
        {
            WriteLine($"Site ID: {site.SiteId}, Site Name: {site.Meta.Name}");
        }

        PressAnyKeyToContinue();
    }
}
