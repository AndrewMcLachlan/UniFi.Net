using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class NetworkTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListNetworks is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListNetworks_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListNetworks(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} network(s).");
    }

    /// <summary>
    /// Given a site with at least one network,
    /// When ListNetworks is called,
    /// Then each network has a non-empty ID, name, and metadata.
    /// </summary>
    [Fact]
    public async Task ListNetworks_EachNetwork_HasRequiredFields()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListNetworks(SiteId, limit: 10);

        foreach (var network in result.Data)
        {
            Assert.NotEqual(Guid.Empty, network.Id);
            Assert.False(String.IsNullOrWhiteSpace(network.Name));
            Assert.NotNull(network.Metadata);
            Output.WriteLine($"  Network: {network.Name} (VLAN {network.VlanId}, {network.Management})");
        }
    }

    /// <summary>
    /// Given a site with at least one network,
    /// When GetNetwork is called with the first network's ID,
    /// Then the network is returned with a matching ID.
    /// </summary>
    [Fact]
    public async Task GetNetwork_WhenNetworksExist_ReturnsNetwork()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListNetworks(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No networks found on site."); return; }

        var network = await Client.GetNetwork(SiteId, list.Data[0].Id);

        Assert.NotNull(network);
        Assert.Equal(list.Data[0].Id, network.Id);
        Output.WriteLine($"  Network: {network.Name}, Default: {network.Default}");
    }

    /// <summary>
    /// Given a site with at least one network,
    /// When GetNetworkReferences is called with the first network's ID,
    /// Then a references object is returned with a non-null resource list.
    /// </summary>
    [Fact]
    public async Task GetNetworkReferences_WhenNetworksExist_ReturnsReferences()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListNetworks(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No networks found on site."); return; }

        var refs = await Client.GetNetworkReferences(SiteId, list.Data[0].Id);

        Assert.NotNull(refs);
        Assert.NotNull(refs.ReferenceResources);
        Output.WriteLine($"  Network '{list.Data[0].Name}' has {refs.ReferenceResources.Count} reference type(s).");
    }
}
