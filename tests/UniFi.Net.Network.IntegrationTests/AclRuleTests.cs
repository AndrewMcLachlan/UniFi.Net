using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class AclRuleTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListAclRules is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListAclRules_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListAclRules(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} ACL rule(s).");
    }

    /// <summary>
    /// Given a site with at least one ACL rule,
    /// When GetAclRule is called with the first rule's ID,
    /// Then the rule is returned with a matching ID, non-empty name, and metadata.
    /// </summary>
    [Fact]
    public async Task GetAclRule_WhenRulesExist_ReturnsRule()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListAclRules(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No ACL rules found on site."); return; }

        var rule = await Client.GetAclRule(SiteId, list.Data[0].Id);

        Assert.NotNull(rule);
        Assert.Equal(list.Data[0].Id, rule.Id);
        Assert.False(String.IsNullOrWhiteSpace(rule.Name));
        Assert.NotNull(rule.Metadata);
        Output.WriteLine($"  ACL Rule: {rule.Name} ({rule.Type}, {rule.Action})");
    }

    /// <summary>
    /// Given a site on the controller,
    /// When GetAclRuleOrdering is called,
    /// Then the ordering response contains a non-null list of rule IDs.
    /// </summary>
    [Fact]
    public async Task GetAclRuleOrdering_ReturnsOrdering()
    {
        if (SkipIfNoSite()) return;

        var ordering = await Client.GetAclRuleOrdering(SiteId);

        Assert.NotNull(ordering);
        Assert.NotNull(ordering.OrderedAclRuleIds);
        Output.WriteLine($"ACL rule ordering has {ordering.OrderedAclRuleIds.Count} rule(s).");
    }
}
