using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class TrafficMatchingListTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListTrafficMatchingLists is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListTrafficMatchingLists_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListTrafficMatchingLists(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} traffic matching list(s).");
    }

    /// <summary>
    /// Given a site with at least one traffic matching list,
    /// When GetTrafficMatchingList is called with the first list's ID,
    /// Then the list is returned with a matching ID and a non-empty name.
    /// </summary>
    [Fact]
    public async Task GetTrafficMatchingList_WhenListsExist_ReturnsList()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListTrafficMatchingLists(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No traffic matching lists found on site."); return; }

        var item = await Client.GetTrafficMatchingList(SiteId, list.Data[0].Id);

        Assert.NotNull(item);
        Assert.Equal(list.Data[0].Id, item.Id);
        Assert.False(String.IsNullOrWhiteSpace(item.Name));
        Output.WriteLine($"  Traffic Matching List: {item.Name} ({item.Type})");
    }
}
