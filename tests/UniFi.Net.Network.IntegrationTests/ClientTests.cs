using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class ClientTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListClients is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListClients_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListClients(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} client(s).");
    }

    /// <summary>
    /// Given a site on the controller,
    /// When ListClients is called with a limit of 1,
    /// Then the response contains at most 1 client.
    /// </summary>
    [Fact]
    public async Task ListClients_WithLimit_RespectsLimit()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListClients(SiteId, limit: 1);

        Assert.NotNull(result);
        Assert.True(result.Data.Count <= 1);
    }

    /// <summary>
    /// Given a site with at least one connected client,
    /// When ListClients is called,
    /// Then each client has a non-empty ID, MAC address, type, and access information.
    /// </summary>
    [Fact]
    public async Task ListClients_EachClient_HasRequiredFields()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListClients(SiteId, limit: 10);

        foreach (var client in result.Data)
        {
            Assert.NotEqual(Guid.Empty, client.Id);
            Assert.False(String.IsNullOrWhiteSpace(client.MacAddress));
            Assert.False(String.IsNullOrWhiteSpace(client.Type));
            Assert.NotNull(client.Access);
            Output.WriteLine($"  Client: {client.Name ?? "(unnamed)"} ({client.MacAddress}) - {client.Type}");
        }
    }

    /// <summary>
    /// Given a site with at least one connected client,
    /// When GetClient is called with the first client's ID,
    /// Then the full client details are returned with a matching ID.
    /// </summary>
    [Fact]
    public async Task GetClient_WhenClientsExist_ReturnsClient()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListClients(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No clients found on site."); return; }

        var client = await Client.GetClient(SiteId, list.Data[0].Id);

        Assert.NotNull(client);
        Assert.Equal(list.Data[0].Id, client.Id);
        Output.WriteLine($"  Client: {client.Name ?? "(unnamed)"}, IP: {client.IpAddress ?? "N/A"}");
    }
}
