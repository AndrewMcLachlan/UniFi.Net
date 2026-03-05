namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a device that is pending adoption.
/// </summary>
/// <param name="MacAddress">The MAC address of the pending device.</param>
/// <param name="IpAddress">The IP address of the pending device.</param>
/// <param name="Model">The model identifier of the pending device.</param>
/// <param name="State">The current state of the pending device.</param>
/// <param name="Supported">Indicates whether the device is supported.</param>
/// <param name="FirmwareVersion">The current firmware version of the pending device.</param>
/// <param name="FirmwareUpdatable">Indicates if a firmware update is available for the device.</param>
/// <param name="Features">Features supported by the pending device.</param>
/// <param name="AdoptionTargetSiteIds">The site IDs that the device can be adopted to.</param>
public record PendingDevice(
    string MacAddress,
    string IpAddress,
    string Model,
    string State,
    bool Supported,
    string FirmwareVersion,
    bool FirmwareUpdatable,
    IReadOnlyList<string> Features,
    IReadOnlyList<Guid> AdoptionTargetSiteIds
);
