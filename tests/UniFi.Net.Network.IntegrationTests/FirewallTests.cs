using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class FirewallTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    // Zones

    /// <summary>
    /// Given a site on the controller,
    /// When ListFirewallZones is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListFirewallZones_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListFirewallZones(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} firewall zone(s).");
    }

    /// <summary>
    /// Given a site with at least one firewall zone,
    /// When ListFirewallZones is called,
    /// Then each zone has a non-empty ID, name, network IDs list, and metadata.
    /// </summary>
    [Fact]
    public async Task ListFirewallZones_EachZone_HasRequiredFields()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListFirewallZones(SiteId, limit: 10);

        foreach (var zone in result.Data)
        {
            Assert.NotEqual(Guid.Empty, zone.Id);
            Assert.False(String.IsNullOrWhiteSpace(zone.Name));
            Assert.NotNull(zone.NetworkIds);
            Assert.NotNull(zone.Metadata);
            Output.WriteLine($"  Zone: {zone.Name} ({zone.NetworkIds.Count} network(s))");
        }
    }

    /// <summary>
    /// Given a site with at least one firewall zone,
    /// When GetFirewallZone is called with the first zone's ID,
    /// Then the zone is returned with a matching ID.
    /// </summary>
    [Fact]
    public async Task GetFirewallZone_WhenZonesExist_ReturnsZone()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListFirewallZones(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No firewall zones found on site."); return; }

        var zone = await Client.GetFirewallZone(SiteId, list.Data[0].Id);

        Assert.NotNull(zone);
        Assert.Equal(list.Data[0].Id, zone.Id);
    }

    // Policies

    /// <summary>
    /// Given a site on the controller,
    /// When ListFirewallPolicies is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListFirewallPolicies_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListFirewallPolicies(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} firewall policy/ies.");
    }

    /// <summary>
    /// Given a site with at least one firewall policy,
    /// When ListFirewallPolicies is called,
    /// Then each policy has a non-empty ID, name, source, destination, and metadata.
    /// </summary>
    [Fact]
    public async Task ListFirewallPolicies_EachPolicy_HasRequiredFields()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListFirewallPolicies(SiteId, limit: 10);

        foreach (var policy in result.Data)
        {
            Assert.NotEqual(Guid.Empty, policy.Id);
            Assert.False(String.IsNullOrWhiteSpace(policy.Name));
            Assert.NotNull(policy.Source);
            Assert.NotNull(policy.Destination);
            Assert.NotNull(policy.Metadata);
            Output.WriteLine($"  Policy: {policy.Name} ({policy.Action}, Enabled: {policy.Enabled})");
        }
    }

    /// <summary>
    /// Given a site with at least one firewall policy,
    /// When GetFirewallPolicy is called with the first policy's ID,
    /// Then the policy is returned with a matching ID.
    /// </summary>
    [Fact]
    public async Task GetFirewallPolicy_WhenPoliciesExist_ReturnsPolicy()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListFirewallPolicies(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No firewall policies found on site."); return; }

        var policy = await Client.GetFirewallPolicy(SiteId, list.Data[0].Id);

        Assert.NotNull(policy);
        Assert.Equal(list.Data[0].Id, policy.Id);
    }

    // Policy Ordering

    /// <summary>
    /// Given a site with at least two firewall zones,
    /// When GetFirewallPolicyOrdering is called with the first two zone IDs,
    /// Then the ordering response contains a non-null list of policy IDs.
    /// </summary>
    [Fact]
    public async Task GetFirewallPolicyOrdering_ReturnsOrdering()
    {
        if (SkipIfNoSite()) return;

        var zones = await Client.ListFirewallZones(SiteId, limit: 2);
        if (zones.Data.Count < 2) { SkipBecause("Need at least 2 firewall zones to query policy ordering."); return; }

        var ordering = await Client.GetFirewallPolicyOrdering(SiteId, zones.Data[0].Id, zones.Data[1].Id);

        Assert.NotNull(ordering);
        Assert.NotNull(ordering.OrderedFirewallPolicyIds);
        Output.WriteLine($"Firewall policy ordering ({zones.Data[0].Name} -> {zones.Data[1].Name}): {ordering.OrderedFirewallPolicyIds.BeforeSystemDefined.Count} before system-defined, {ordering.OrderedFirewallPolicyIds.AfterSystemDefined.Count} after.");
    }
}
