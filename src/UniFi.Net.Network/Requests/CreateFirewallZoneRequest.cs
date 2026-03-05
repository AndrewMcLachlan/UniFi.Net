namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to create a new firewall zone.
/// </summary>
/// <param name="Name">The name of the firewall zone.</param>
/// <param name="NetworkIds">The list of network IDs associated with this zone.</param>
public record CreateFirewallZoneRequest(
    string Name,
    IReadOnlyList<Guid> NetworkIds
);
