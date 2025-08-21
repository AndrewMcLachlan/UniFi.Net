namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a UniFi device with detailed information and configuration.
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
/// <param name="AdoptedAt">The date and time when the device was adopted.</param>
/// <param name="ProvisionedAt">The date and time when the device was provisioned.</param>
/// <param name="ConfigurationId">The configuration identifier associated with the device.</param>
/// <param name="Uplink">Information about the uplink connection of the device.</param>
/// <param name="Features">Features supported by the device.</param>
/// <param name="Interfaces">Network interfaces and radios of the device.</param>
public record Device(
    Guid Id,
    string Name,
    string Model,
    bool Supported,
    string MacAddress,
    string IpAddress,
    string State,
    string FirmwareVersion,
    bool FirmwareUpdatable,
    DateTimeOffset AdoptedAt,
    DateTimeOffset ProvisionedAt,
    string ConfigurationId,
    DeviceUplink Uplink,
    DeviceFeatures Features,
    DeviceInterfaces Interfaces
) : BaseDevice(Id, Name, Model, MacAddress, IpAddress, State);

/// <summary>
/// Represents the uplink information for a device.
/// </summary>
/// <param name="DeviceId">The unique identifier of the uplink device.</param>
public record DeviceUplink(
    Guid DeviceId
);

/// <summary>
/// Represents the features supported by a device.
/// </summary>
/// <param name="Switching">Switching feature information (if available).</param>
/// <param name="AccessPoint">Access point feature information (if available).</param>
public record DeviceFeatures(
    object? Switching,
    object? AccessPoint
);

/// <summary>
/// Represents the network interfaces and radios of a device.
/// </summary>
/// <param name="Ports">A list of network ports on the device.</param>
/// <param name="Radios">A list of radios on the device.</param>
public record DeviceInterfaces(
    IReadOnlyList<DevicePort> Ports,
    IReadOnlyList<DeviceRadio> Radios
);

/// <summary>
/// Represents a network port on a device.
/// </summary>
/// <param name="Idx">The index of the port.</param>
/// <param name="State">The current state of the port (e.g., UP, DOWN).</param>
/// <param name="Connector">The type of connector (e.g., RJ45).</param>
/// <param name="MaxSpeedMbps">The maximum supported speed in Mbps.</param>
/// <param name="SpeedMbps">The current speed in Mbps.</param>
/// <param name="PoE">Power over Ethernet (PoE) information for the port.</param>
public record DevicePort(
    int Idx,
    string State,
    string Connector,
    int MaxSpeedMbps,
    int SpeedMbps,
    DevicePoe PoE
);

/// <summary>
/// Represents Power over Ethernet (PoE) information for a port.
/// </summary>
/// <param name="Standard">The PoE standard (e.g., 802.3bt).</param>
/// <param name="Type">The PoE type.</param>
/// <param name="Enabled">Indicates if PoE is enabled on the port.</param>
/// <param name="State">The current PoE state (e.g., UP, DOWN).</param>
public record DevicePoe(
    string Standard,
    int Type,
    bool Enabled,
    string State
);

/// <summary>
/// Represents a radio interface on a device.
/// </summary>
/// <param name="WlanStandard">The wireless LAN standard (e.g., 802.11a).</param>
/// <param name="FrequencyGHz">The operating frequency in GHz.</param>
/// <param name="ChannelWidthMHz">The channel width in MHz.</param>
/// <param name="Channel">The channel number.</param>
public record DeviceRadio(
    string WlanStandard,
    float FrequencyGHz,
    int ChannelWidthMHz,
    int Channel
);
