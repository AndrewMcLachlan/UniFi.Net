namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a UniFi device with detailed information and configuration.
/// </summary>
/// <param name="Id">The unique identifier of the device.</param>
/// <param name="Name">The display name of the device.</param>
/// <param name="Model">The model identifier of the device.</param>
/// <param name="MacAddress">The MAC address of the device.</param>
/// <param name="IpAddress">The IP address assigned to the device.</param>
/// <param name="State">The current state of the device (e.g., ONLINE, OFFLINE).</param>
/// <param name="Features">Features supported by the device.</param>
/// <param name="Interfaces">Network interfaces and radios of the device.</param>
public record DeviceSummary(
        Guid Id,
    string Name,
    string Model,
    string MacAddress,
    string IpAddress,
    string State,
    IList<string> Features,
    IList<string> Interfaces
) : BaseDevice(Id, Name, Model, MacAddress, IpAddress, State);
