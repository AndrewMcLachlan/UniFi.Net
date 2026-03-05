using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of ACL rule.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum AclRuleType
{
    /// <summary>
    /// IPv4 ACL rule.
    /// </summary>
    Ipv4 = 0,

    /// <summary>
    /// MAC address ACL rule.
    /// </summary>
    Mac = 1,
}

/// <summary>
/// The action of an ACL rule.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum AclRuleAction
{
    /// <summary>
    /// Allow the traffic.
    /// </summary>
    Allow = 0,

    /// <summary>
    /// Block the traffic.
    /// </summary>
    Block = 1,
}

/// <summary>
/// Represents an ACL rule.
/// </summary>
/// <param name="Id">The unique identifier of the ACL rule.</param>
/// <param name="Type">The type of ACL rule.</param>
/// <param name="Enabled">Indicates whether the ACL rule is enabled.</param>
/// <param name="Name">The name of the ACL rule.</param>
/// <param name="Description">The description of the ACL rule.</param>
/// <param name="Action">The action to take when the rule matches.</param>
/// <param name="Index">The index position of the ACL rule.</param>
/// <param name="ProtocolFilter">The protocol filter for the rule.</param>
/// <param name="NetworkId">The network ID associated with the rule.</param>
/// <param name="EnforcingDeviceFilter">The enforcing device filter.</param>
/// <param name="SourceFilter">The source filter for the rule.</param>
/// <param name="DestinationFilter">The destination filter for the rule.</param>
/// <param name="Metadata">The metadata for the ACL rule.</param>
public record AclRule(
    Guid Id,
    AclRuleType Type,
    bool Enabled,
    string Name,
    string? Description,
    AclRuleAction Action,
    int Index,
    AclProtocolFilter? ProtocolFilter,
    Guid? NetworkId,
    AclDeviceFilter? EnforcingDeviceFilter,
    AclEndpointFilter? SourceFilter,
    AclEndpointFilter? DestinationFilter,
    ResourceMetadata Metadata
);

/// <summary>
/// Represents a protocol filter for an ACL rule.
/// </summary>
/// <param name="Protocol">The protocol to filter.</param>
/// <param name="Ports">The list of ports to filter.</param>
public record AclProtocolFilter(
    string? Protocol,
    IReadOnlyList<int>? Ports
);

/// <summary>
/// Represents a device filter for an ACL rule.
/// </summary>
/// <param name="DeviceTagIds">The list of device tag IDs to filter by.</param>
public record AclDeviceFilter(
    IReadOnlyList<Guid>? DeviceTagIds
);

/// <summary>
/// Represents an endpoint filter for an ACL rule source or destination.
/// </summary>
/// <param name="IpAddresses">The list of IP addresses to filter.</param>
/// <param name="MacAddresses">The list of MAC addresses to filter.</param>
/// <param name="Ports">The list of ports to filter.</param>
/// <param name="TrafficMatchingListIds">The list of traffic matching list IDs to filter.</param>
public record AclEndpointFilter(
    IReadOnlyList<string>? IpAddresses,
    IReadOnlyList<string>? MacAddresses,
    IReadOnlyList<int>? Ports,
    IReadOnlyList<Guid>? TrafficMatchingListIds
);

/// <summary>
/// Represents the ordering of ACL rules.
/// </summary>
/// <param name="OrderedAclRuleIds">The ordered list of ACL rule IDs.</param>
public record AclRuleOrdering(
    IReadOnlyList<Guid> OrderedAclRuleIds
);
