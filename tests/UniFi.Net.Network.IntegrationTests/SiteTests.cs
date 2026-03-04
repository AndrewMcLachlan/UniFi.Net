using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class SiteTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a configured Network client,
    /// When ListSites is called with no parameters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListSites_ReturnsPagedResponse()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListSites();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} site(s).");
    }

    /// <summary>
    /// Given a configured Network client,
    /// When ListSites is called with a limit of 1,
    /// Then the response contains at most 1 site.
    /// </summary>
    [Fact]
    public async Task ListSites_WithLimit_RespectsLimit()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListSites(limit: 1);

        Assert.NotNull(result);
        Assert.True(result.Data.Count <= 1);
    }

    /// <summary>
    /// Given a configured Network client with at least one site,
    /// When ListSites is called,
    /// Then each site has a non-empty ID and name.
    /// </summary>
    [Fact]
    public async Task ListSites_EachSite_HasRequiredFields()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListSites(limit: 10);

        foreach (var site in result.Data)
        {
            Assert.NotEqual(Guid.Empty, site.Id);
            Assert.False(String.IsNullOrWhiteSpace(site.Name));
            Output.WriteLine($"  Site: {site.Name} ({site.Id})");
        }
    }
}
