using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of WiFi broadcast.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum WifiBroadcastType
{
    /// <summary>
    /// Standard WiFi broadcast.
    /// </summary>
    Standard = 0,

    /// <summary>
    /// IoT optimized WiFi broadcast.
    /// </summary>
    IotOptimized = 1,
}

/// <summary>
/// Represents a summary of a WiFi broadcast (returned by list endpoint).
/// </summary>
/// <param name="Id">The unique identifier of the WiFi broadcast.</param>
/// <param name="Name">The name of the WiFi broadcast (SSID).</param>
/// <param name="Type">The type of WiFi broadcast.</param>
/// <param name="Enabled">Indicates whether the WiFi broadcast is enabled.</param>
/// <param name="Metadata">The metadata for the WiFi broadcast.</param>
/// <param name="Network">The network configuration for the WiFi broadcast.</param>
/// <param name="SecurityConfiguration">The security configuration for the WiFi broadcast.</param>
/// <param name="BroadcastingDeviceFilter">The broadcasting device filter.</param>
public record WifiBroadcastSummary(
    Guid Id,
    string Name,
    WifiBroadcastType Type,
    bool Enabled,
    ResourceMetadata Metadata,
    WifiBroadcastNetwork? Network,
    WifiBroadcastSecurity? SecurityConfiguration,
    WifiBroadcastDeviceFilter? BroadcastingDeviceFilter
);

/// <summary>
/// Represents a WiFi broadcast with full details (returned by get endpoint).
/// </summary>
/// <param name="Id">The unique identifier of the WiFi broadcast.</param>
/// <param name="Name">The name of the WiFi broadcast (SSID).</param>
/// <param name="Type">The type of WiFi broadcast.</param>
/// <param name="Enabled">Indicates whether the WiFi broadcast is enabled.</param>
/// <param name="Metadata">The metadata for the WiFi broadcast.</param>
/// <param name="Network">The network configuration for the WiFi broadcast.</param>
/// <param name="SecurityConfiguration">The security configuration for the WiFi broadcast.</param>
/// <param name="BroadcastingDeviceFilter">The broadcasting device filter.</param>
/// <param name="MulticastToUnicastConversionEnabled">Indicates if multicast to unicast conversion is enabled.</param>
/// <param name="ClientIsolationEnabled">Indicates if client isolation is enabled.</param>
/// <param name="HideName">Indicates if the SSID name is hidden.</param>
/// <param name="UapsdEnabled">Indicates if U-APSD is enabled.</param>
public record WifiBroadcast(
    Guid Id,
    string Name,
    WifiBroadcastType Type,
    bool Enabled,
    ResourceMetadata Metadata,
    WifiBroadcastNetwork? Network,
    WifiBroadcastSecurity? SecurityConfiguration,
    WifiBroadcastDeviceFilter? BroadcastingDeviceFilter,
    bool? MulticastToUnicastConversionEnabled,
    bool? ClientIsolationEnabled,
    bool? HideName,
    bool? UapsdEnabled
);

/// <summary>
/// Represents the network configuration for a WiFi broadcast.
/// </summary>
/// <param name="NetworkId">The ID of the network this broadcast is associated with.</param>
public record WifiBroadcastNetwork(
    Guid NetworkId
);

/// <summary>
/// Represents the security configuration for a WiFi broadcast.
/// </summary>
/// <param name="Protocol">The security protocol (e.g., WPA2, WPA3).</param>
public record WifiBroadcastSecurity(
    string Protocol
);

/// <summary>
/// Represents the device filter for a WiFi broadcast.
/// </summary>
/// <param name="DeviceTagIds">The list of device tag IDs to filter by.</param>
public record WifiBroadcastDeviceFilter(
    IReadOnlyList<Guid>? DeviceTagIds
);
