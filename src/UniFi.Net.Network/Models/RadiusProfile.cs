namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a RADIUS profile.
/// </summary>
/// <param name="Id">The unique identifier of the RADIUS profile.</param>
/// <param name="Name">The name of the RADIUS profile.</param>
/// <param name="Metadata">The metadata for the RADIUS profile.</param>
public record RadiusProfile(
    Guid Id,
    string Name,
    ResourceMetadata Metadata
);
