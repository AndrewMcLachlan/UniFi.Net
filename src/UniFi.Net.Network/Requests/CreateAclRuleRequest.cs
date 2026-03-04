using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to create a new ACL rule.
/// </summary>
/// <param name="Type">The type of ACL rule.</param>
/// <param name="Enabled">Indicates whether the ACL rule is enabled.</param>
/// <param name="Name">The name of the ACL rule.</param>
/// <param name="Action">The action to take when the rule matches.</param>
/// <param name="Description">The description of the ACL rule.</param>
/// <param name="ProtocolFilter">The protocol filter for the rule.</param>
/// <param name="NetworkId">The network ID associated with the rule.</param>
/// <param name="EnforcingDeviceFilter">The enforcing device filter.</param>
/// <param name="SourceFilter">The source filter for the rule.</param>
/// <param name="DestinationFilter">The destination filter for the rule.</param>
public record CreateAclRuleRequest(
    AclRuleType Type,
    bool Enabled,
    string Name,
    AclRuleAction Action,
    string? Description = null,
    AclProtocolFilter? ProtocolFilter = null,
    Guid? NetworkId = null,
    AclDeviceFilter? EnforcingDeviceFilter = null,
    AclEndpointFilter? SourceFilter = null,
    AclEndpointFilter? DestinationFilter = null
);
