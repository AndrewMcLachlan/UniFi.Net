using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to update an existing WiFi broadcast.
/// </summary>
/// <param name="Name">The name of the WiFi broadcast (SSID).</param>
/// <param name="Type">The type of WiFi broadcast.</param>
/// <param name="Enabled">Indicates whether the WiFi broadcast is enabled.</param>
/// <param name="Network">The network configuration.</param>
/// <param name="SecurityConfiguration">The security configuration.</param>
/// <param name="BroadcastingDeviceFilter">The broadcasting device filter.</param>
public record UpdateWifiBroadcastRequest(
    string Name,
    WifiBroadcastType Type,
    bool Enabled,
    WifiBroadcastNetwork? Network = null,
    WifiBroadcastSecurity? SecurityConfiguration = null,
    WifiBroadcastDeviceFilter? BroadcastingDeviceFilter = null
);
