using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// Represents a request to create a new WiFi broadcast.
/// </summary>
/// <param name="Name">The name of the WiFi broadcast (SSID).</param>
/// <param name="Type">The type of WiFi broadcast.</param>
/// <param name="Enabled">Indicates whether the WiFi broadcast is enabled.</param>
/// <param name="Network">The network configuration.</param>
/// <param name="SecurityConfiguration">The security configuration.</param>
/// <param name="BroadcastingDeviceFilter">The broadcasting device filter.</param>
/// <param name="Channel2gLockedTo6">Indicates if the 2.4 GHz channel is locked to channel 6.</param>
/// <param name="DtimPeriod2gLockedTo3">Indicates if the 2.4 GHz DTIM period is locked to 3.</param>
public record CreateWifiBroadcastRequest(
    string Name,
    WifiBroadcastType Type,
    bool Enabled,
    WifiBroadcastNetwork? Network = null,
    WifiBroadcastSecurity? SecurityConfiguration = null,
    WifiBroadcastDeviceFilter? BroadcastingDeviceFilter = null,
    bool? Channel2gLockedTo6 = null,
    bool? DtimPeriod2gLockedTo3 = null
);
