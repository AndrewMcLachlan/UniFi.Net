using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of traffic matching list.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum TrafficMatchingListType
{
    /// <summary>
    /// Port-based matching list.
    /// </summary>
    Ports = 0,

    /// <summary>
    /// IPv4 address-based matching list.
    /// </summary>
    Ipv4Addresses = 1,

    /// <summary>
    /// IPv6 address-based matching list.
    /// </summary>
    Ipv6Addresses = 2,
}

/// <summary>
/// Represents a traffic matching list.
/// </summary>
/// <param name="Id">The unique identifier of the traffic matching list.</param>
/// <param name="Name">The name of the traffic matching list.</param>
/// <param name="Type">The type of traffic matching list.</param>
/// <param name="Ports">The list of ports (for port-based matching lists).</param>
/// <param name="Ipv4Addresses">The list of IPv4 addresses (for IPv4 address-based matching lists).</param>
/// <param name="Ipv6Addresses">The list of IPv6 addresses (for IPv6 address-based matching lists).</param>
public record TrafficMatchingList(
    Guid Id,
    string Name,
    TrafficMatchingListType Type,
    IReadOnlyList<int>? Ports,
    IReadOnlyList<string>? Ipv4Addresses,
    IReadOnlyList<string>? Ipv6Addresses
);
