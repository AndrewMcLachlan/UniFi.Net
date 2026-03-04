namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents metadata for a resource.
/// </summary>
/// <param name="Origin">The origin of the resource.</param>
/// <param name="Configurable">Indicates whether the resource is configurable.</param>
public record ResourceMetadata(
    MetadataOrigin Origin,
    bool? Configurable
);
