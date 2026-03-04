namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a firewall zone.
/// </summary>
/// <param name="Id">The unique identifier of the firewall zone.</param>
/// <param name="Name">The name of the firewall zone.</param>
/// <param name="NetworkIds">The list of network IDs associated with this zone.</param>
/// <param name="Metadata">The metadata for the firewall zone.</param>
public record FirewallZone(
    Guid Id,
    string Name,
    IReadOnlyList<Guid> NetworkIds,
    ResourceMetadata Metadata
);
