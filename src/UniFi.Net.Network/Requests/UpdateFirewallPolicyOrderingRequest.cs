namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to update the ordering of firewall policies.
/// </summary>
/// <param name="OrderedFirewallPolicyIds">The ordered firewall policy IDs grouped by system-defined boundary.</param>
public record UpdateFirewallPolicyOrderingRequest(
    Models.FirewallPolicyOrderingIds OrderedFirewallPolicyIds
);
