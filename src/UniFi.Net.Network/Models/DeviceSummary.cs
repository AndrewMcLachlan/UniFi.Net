namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a summary of a UniFi device.
/// </summary>
/// <param name="Id">The unique identifier of the device.</param>
/// <param name="Name">The display name of the device.</param>
/// <param name="Model">The model identifier of the device.</param>
/// <param name="Supported">Indicates whether the device is supported.</param>
/// <param name="MacAddress">The MAC address of the device.</param>
/// <param name="IpAddress">The IP address assigned to the device.</param>
/// <param name="State">The current state of the device (e.g., ONLINE, OFFLINE).</param>
/// <param name="FirmwareVersion">The current firmware version of the device.</param>
/// <param name="FirmwareUpdatable">Indicates if a firmware update is available for the device.</param>
/// <param name="Features">Features supported by the device.</param>
/// <param name="Interfaces">Network interfaces of the device.</param>
public record DeviceSummary(
    Guid Id,
    string Name,
    string Model,
    bool Supported,
    string MacAddress,
    string IpAddress,
    string State,
    string FirmwareVersion,
    bool FirmwareUpdatable,
    IReadOnlyList<string> Features,
    IReadOnlyList<string> Interfaces
) : BaseDevice(Id, Name, Model, Supported, MacAddress, IpAddress, State);
