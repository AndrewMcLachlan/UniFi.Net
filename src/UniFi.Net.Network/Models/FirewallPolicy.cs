using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The action type for a firewall policy.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum FirewallPolicyActionType
{
    /// <summary>
    /// Allow the traffic.
    /// </summary>
    Allow = 0,

    /// <summary>
    /// Block the traffic.
    /// </summary>
    Block = 1,

    /// <summary>
    /// Reject the traffic.
    /// </summary>
    Reject = 2,

    /// <summary>
    /// Drop the traffic.
    /// </summary>
    Drop = 3,

    /// <summary>
    /// Route the traffic.
    /// </summary>
    Route = 4,
}

/// <summary>
/// Represents the action configuration for a firewall policy.
/// </summary>
/// <param name="Type">The action type.</param>
/// <param name="AllowReturnTraffic">Indicates whether return traffic is allowed.</param>
public record FirewallPolicyAction(
    FirewallPolicyActionType Type,
    bool? AllowReturnTraffic
);

/// <summary>
/// Connection state for firewall policy filtering.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum ConnectionState
{
    /// <summary>
    /// New connection.
    /// </summary>
    New = 0,

    /// <summary>
    /// Invalid connection.
    /// </summary>
    Invalid = 1,

    /// <summary>
    /// Established connection.
    /// </summary>
    Established = 2,

    /// <summary>
    /// Related connection.
    /// </summary>
    Related = 3,
}

/// <summary>
/// IP version for firewall policy protocol scope.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum IpVersion
{
    /// <summary>
    /// IPv4 only.
    /// </summary>
    Ipv4 = 0,

    /// <summary>
    /// IPv6 only.
    /// </summary>
    Ipv6 = 1,

    /// <summary>
    /// Both IPv4 and IPv6.
    /// </summary>
    Ipv4AndIpv6 = 2,
}

/// <summary>
/// Schedule mode for a firewall policy.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum FirewallScheduleMode
{
    /// <summary>
    /// The schedule is always active.
    /// </summary>
    Always = 0,

    /// <summary>
    /// The schedule follows a recurring pattern.
    /// </summary>
    Recurring = 1,

    /// <summary>
    /// The schedule is active every day within a time range.
    /// </summary>
    EveryDay = 2,

    /// <summary>
    /// The schedule is active on specific days of the week within a time range.
    /// </summary>
    EveryWeek = 3,
}

/// <summary>
/// Represents a firewall policy.
/// </summary>
/// <param name="Id">The unique identifier of the firewall policy.</param>
/// <param name="Enabled">Indicates whether the firewall policy is enabled.</param>
/// <param name="Name">The name of the firewall policy.</param>
/// <param name="Description">The description of the firewall policy.</param>
/// <param name="Index">The index position of the firewall policy.</param>
/// <param name="Action">The action to take when the policy matches.</param>
/// <param name="Source">The source configuration for the policy.</param>
/// <param name="Destination">The destination configuration for the policy.</param>
/// <param name="IpProtocolScope">The IP protocol scope for the policy.</param>
/// <param name="ConnectionStateFilter">The connection state filter for the policy.</param>
/// <param name="LoggingEnabled">Indicates whether logging is enabled for the policy.</param>
/// <param name="Schedule">The schedule configuration for the policy.</param>
/// <param name="Metadata">The metadata for the firewall policy.</param>
public record FirewallPolicy(
    Guid Id,
    bool Enabled,
    string Name,
    string? Description,
    int Index,
    FirewallPolicyAction Action,
    FirewallPolicyEndpoint Source,
    FirewallPolicyEndpoint Destination,
    FirewallIpProtocolScope? IpProtocolScope,
    IReadOnlyList<ConnectionState>? ConnectionStateFilter,
    bool? LoggingEnabled,
    FirewallSchedule? Schedule,
    ResourceMetadata Metadata
);

/// <summary>
/// Represents a source or destination endpoint for a firewall policy.
/// </summary>
/// <param name="ZoneId">The firewall zone ID.</param>
/// <param name="TrafficFilter">The traffic filter configuration.</param>
public record FirewallPolicyEndpoint(
    Guid ZoneId,
    FirewallTrafficFilter? TrafficFilter
);

/// <summary>
/// Represents a traffic filter for a firewall policy endpoint.
/// </summary>
/// <param name="Type">The type of traffic filter (e.g., "NETWORK", "IP_ADDRESS").</param>
/// <param name="NetworkFilter">The network filter, present when <see cref="Type"/> is "NETWORK".</param>
/// <param name="IpAddressFilter">The IP address filter, present when <see cref="Type"/> is "IP_ADDRESS".</param>
/// <param name="PortFilter">The port filter, optionally present with IP address filters.</param>
public record FirewallTrafficFilter(
    string Type,
    FirewallNetworkFilter? NetworkFilter,
    FirewallIpAddressFilter? IpAddressFilter,
    FirewallPortFilter? PortFilter
);

/// <summary>
/// Represents a network filter for a firewall traffic filter.
/// </summary>
/// <param name="NetworkIds">The list of network IDs to filter.</param>
/// <param name="MatchOpposite">Indicates whether to match the opposite of the specified networks.</param>
public record FirewallNetworkFilter(
    IReadOnlyList<Guid> NetworkIds,
    bool MatchOpposite
);

/// <summary>
/// Represents an IP address filter for a firewall traffic filter.
/// </summary>
/// <param name="Type">The type of IP address filter (e.g., "IP_ADDRESSES").</param>
/// <param name="MatchOpposite">Indicates whether to match the opposite of the specified addresses.</param>
/// <param name="Items">The list of IP address filter items.</param>
public record FirewallIpAddressFilter(
    string Type,
    bool MatchOpposite,
    IReadOnlyList<FirewallFilterItem>? Items
);

/// <summary>
/// Represents a port filter for a firewall traffic filter.
/// </summary>
/// <param name="Type">The type of port filter (e.g., "PORTS").</param>
/// <param name="MatchOpposite">Indicates whether to match the opposite of the specified ports.</param>
/// <param name="Items">The list of port filter items.</param>
public record FirewallPortFilter(
    string Type,
    bool MatchOpposite,
    IReadOnlyList<FirewallFilterItem>? Items
);

/// <summary>
/// Represents a single item in a firewall filter (IP address or port).
/// </summary>
/// <param name="Type">The type of filter item (e.g., "IP_ADDRESS", "PORT_NUMBER").</param>
/// <param name="Value">The value of the filter item.</param>
public record FirewallFilterItem(
    string Type,
    object Value
);

/// <summary>
/// Represents the IP protocol scope for a firewall policy.
/// </summary>
/// <param name="IpVersion">The IP version scope.</param>
/// <param name="ProtocolFilter">The protocol filter configuration.</param>
public record FirewallIpProtocolScope(
    IpVersion IpVersion,
    FirewallProtocolFilter? ProtocolFilter
);

/// <summary>
/// Represents a protocol filter for a firewall policy.
/// </summary>
/// <param name="Type">The type of protocol filter (e.g., "PRESET", "NAMED_PROTOCOL").</param>
/// <param name="Preset">The preset protocol configuration.</param>
/// <param name="Protocol">The named protocol configuration.</param>
/// <param name="MatchOpposite">Indicates whether to match the opposite of the specified protocol.</param>
public record FirewallProtocolFilter(
    string Type,
    FirewallProtocolPreset? Preset,
    FirewallProtocolPreset? Protocol,
    bool? MatchOpposite
);

/// <summary>
/// Represents a protocol preset or named protocol.
/// </summary>
/// <param name="Name">The name of the protocol preset (e.g., "TCP_UDP", "TCP").</param>
public record FirewallProtocolPreset(
    string Name
);

/// <summary>
/// Represents a schedule configuration for a firewall policy.
/// </summary>
/// <param name="Mode">The schedule mode.</param>
/// <param name="TimeFilter">The time filter configuration for the schedule.</param>
/// <param name="RepeatOnDays">The days of the week the schedule repeats on, when <see cref="Mode"/> is <see cref="FirewallScheduleMode.EveryWeek"/>.</param>
public record FirewallSchedule(
    FirewallScheduleMode Mode,
    FirewallTimeFilter? TimeFilter,
    IReadOnlyList<string>? RepeatOnDays
);

/// <summary>
/// Represents a time filter for a firewall schedule.
/// </summary>
/// <param name="StartTime">The start time in HH:mm format.</param>
/// <param name="StopTime">The stop time in HH:mm format.</param>
public record FirewallTimeFilter(
    string StartTime,
    string StopTime
);

/// <summary>
/// Represents the ordering of firewall policies.
/// </summary>
/// <param name="OrderedFirewallPolicyIds">The ordered firewall policy IDs grouped by system-defined boundary.</param>
public record FirewallPolicyOrdering(
    FirewallPolicyOrderingIds OrderedFirewallPolicyIds
);

/// <summary>
/// Represents the ordered firewall policy IDs grouped by system-defined boundary.
/// </summary>
/// <param name="BeforeSystemDefined">Policy IDs ordered before system-defined policies.</param>
/// <param name="AfterSystemDefined">Policy IDs ordered after system-defined policies.</param>
public record FirewallPolicyOrderingIds(
    IReadOnlyList<Guid> BeforeSystemDefined,
    IReadOnlyList<Guid> AfterSystemDefined
);
