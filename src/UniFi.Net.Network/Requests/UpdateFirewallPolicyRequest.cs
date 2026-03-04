using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to update an existing firewall policy.
/// </summary>
/// <param name="Enabled">Indicates whether the firewall policy is enabled.</param>
/// <param name="Name">The name of the firewall policy.</param>
/// <param name="Action">The action to take when the policy matches.</param>
/// <param name="Source">The source configuration for the policy.</param>
/// <param name="Destination">The destination configuration for the policy.</param>
/// <param name="Description">The description of the firewall policy.</param>
/// <param name="IpProtocolScope">The IP protocol scope for the policy.</param>
/// <param name="ConnectionStateFilter">The connection state filter for the policy.</param>
/// <param name="LoggingEnabled">Indicates whether logging is enabled for the policy.</param>
/// <param name="Schedule">The schedule configuration for the policy.</param>
public record UpdateFirewallPolicyRequest(
    bool Enabled,
    string Name,
    FirewallPolicyAction Action,
    FirewallPolicyEndpoint Source,
    FirewallPolicyEndpoint Destination,
    string? Description = null,
    FirewallIpProtocolScope? IpProtocolScope = null,
    IReadOnlyList<ConnectionState>? ConnectionStateFilter = null,
    bool? LoggingEnabled = null,
    FirewallSchedule? Schedule = null
);
