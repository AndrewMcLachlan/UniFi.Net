using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class WifiBroadcastTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListWifiBroadcasts is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListWifiBroadcasts_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListWifiBroadcasts(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} WiFi broadcast(s).");
    }

    /// <summary>
    /// Given a site with at least one WiFi broadcast,
    /// When ListWifiBroadcasts is called,
    /// Then each broadcast has a non-empty ID, name, and metadata.
    /// </summary>
    [Fact]
    public async Task ListWifiBroadcasts_EachBroadcast_HasRequiredFields()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListWifiBroadcasts(SiteId, limit: 10);

        foreach (var broadcast in result.Data)
        {
            Assert.NotEqual(Guid.Empty, broadcast.Id);
            Assert.False(String.IsNullOrWhiteSpace(broadcast.Name));
            Assert.NotNull(broadcast.Metadata);
            Output.WriteLine($"  WiFi: {broadcast.Name} ({broadcast.Type}, Enabled: {broadcast.Enabled})");
        }
    }

    /// <summary>
    /// Given a site with at least one WiFi broadcast,
    /// When GetWifiBroadcast is called with the first broadcast's ID,
    /// Then the full broadcast details are returned with a matching ID.
    /// </summary>
    [Fact]
    public async Task GetWifiBroadcast_WhenBroadcastsExist_ReturnsBroadcast()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListWifiBroadcasts(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No WiFi broadcasts found on site."); return; }

        var broadcast = await Client.GetWifiBroadcast(SiteId, list.Data[0].Id);

        Assert.NotNull(broadcast);
        Assert.Equal(list.Data[0].Id, broadcast.Id);
        Output.WriteLine($"  WiFi: {broadcast.Name}, Hidden: {broadcast.HideName}, Isolation: {broadcast.ClientIsolationEnabled}");
    }
}
