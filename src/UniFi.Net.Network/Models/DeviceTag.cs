namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a device tag.
/// </summary>
/// <param name="Id">The unique identifier of the device tag.</param>
/// <param name="Name">The name of the device tag.</param>
/// <param name="DeviceIds">The list of device IDs associated with this tag.</param>
/// <param name="Metadata">The metadata for the device tag.</param>
public record DeviceTag(
    Guid Id,
    string Name,
    IReadOnlyList<Guid> DeviceIds,
    ResourceMetadata Metadata
);
