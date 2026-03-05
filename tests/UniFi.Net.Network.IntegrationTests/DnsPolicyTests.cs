using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class DnsPolicyTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListDnsPolicies is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListDnsPolicies_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListDnsPolicies(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} DNS policy/ies.");
    }

    /// <summary>
    /// Given a site with at least one DNS policy,
    /// When GetDnsPolicy is called with the first policy's ID,
    /// Then the policy is returned with a matching ID, non-empty domain, and metadata.
    /// </summary>
    [Fact]
    public async Task GetDnsPolicy_WhenPoliciesExist_ReturnsPolicy()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListDnsPolicies(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No DNS policies found on site."); return; }

        var policy = await Client.GetDnsPolicy(SiteId, list.Data[0].Id);

        Assert.NotNull(policy);
        Assert.Equal(list.Data[0].Id, policy.Id);
        Assert.False(String.IsNullOrWhiteSpace(policy.Domain));
        Assert.NotNull(policy.Metadata);
        Output.WriteLine($"  DNS Policy: {policy.Domain} ({policy.Type})");
    }
}
