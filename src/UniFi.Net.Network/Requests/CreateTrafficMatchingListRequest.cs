using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to create a new traffic matching list.
/// </summary>
/// <param name="Name">The name of the traffic matching list.</param>
/// <param name="Type">The type of traffic matching list.</param>
/// <param name="Ports">The list of ports (for port-based matching lists).</param>
/// <param name="Ipv4Addresses">The list of IPv4 addresses (for IPv4 address-based matching lists).</param>
/// <param name="Ipv6Addresses">The list of IPv6 addresses (for IPv6 address-based matching lists).</param>
public record CreateTrafficMatchingListRequest(
    string Name,
    TrafficMatchingListType Type,
    IReadOnlyList<int>? Ports = null,
    IReadOnlyList<string>? Ipv4Addresses = null,
    IReadOnlyList<string>? Ipv6Addresses = null
);
