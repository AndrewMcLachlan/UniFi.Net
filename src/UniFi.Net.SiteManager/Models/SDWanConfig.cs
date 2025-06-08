namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Basic configuration for SD-WAN.
/// </summary>
/// <param name="Id">Unique identifier of the SD-WAN config.</param>
/// <param name="Name">Name of the SD-WAN config.</param>
/// <param name="Type">Type of SD-WAN config - Currently only supports sdwan-hbsp. Values: sdwan-hbsp.</param>
public record BasicSDWanConfig(Guid Id, string Name, string Type);

/// <summary>
/// Detailed configuration for SD-WAN.
/// </summary>
/// <param name="Id">Unique identifier of the SD-WAN config.</param>
/// <param name="Name">Name of the SD-WAN config.</param>
/// <param name="Type">Type of SD-WAN config - Currently only supports sdwan-hbsp. Values: sdwan-hbsp.</param>
/// <param name="Variant">Variant of SD-WAN configuration. Values: distributed, failover, single.</param>
/// <param name="Settings">Advanced settings.</param>
/// <param name="Hubs">List of SD-WAN hubs.</param>
/// <param name="Spokes">List of SD-WAN spokes.</param>
public record SDWanConfig(
    Guid Id,
    string Name,
    string Type,
    SDWanVariant Variant,
    SDWanSettings Settings,
    IReadOnlyList<SDWanHub> Hubs,
    IReadOnlyList<SDWanSpoke> Spokes
) : BasicSDWanConfig(Id, Name, Type);

/// <summary>
/// Advanced settings for SD-WAN.
/// </summary>
/// <param name="HubsInterconnect">Indicates if hubs are interconnected.</param>
/// <param name="SpokeToHubTunnelsMode">Mode for spoke-to-hub tunnels. Values: maxResiliency, redundant, scalable.</param>
/// <param name="SpokesAutoScaleAndNatEnabled">Auto-assigns subnet and routes; otherwise, users enter them manually.</param>
/// <param name="SpokesAutoScaleAndNatRange">Subnet in CIDR format, Example: 172.16.0.0/12.</param>
/// <param name="SpokesIsolate">Setting for NET: Spokes can reach hubs but not other spokes.</param>
/// <param name="SpokeStandardSettingsEnabled">Enable spoke standard settings.</param>
/// <param name="SpokeStandardSettingsValues">Spoke standard settings.</param>
/// <param name="SpokeToHubRouting">Routing for spoke-to-hub. Values: custom, geo.</param>
public record SDWanSettings(
    bool? HubsInterconnect,
    SpokeToHubTunnelsMode SpokeToHubTunnelsMode,
    bool SpokesAutoScaleAndNatEnabled,
    string SpokesAutoScaleAndNatRange,
    bool SpokesIsolate,
    bool SpokeStandardSettingsEnabled,
    IReadOnlyDictionary<string, object>? SpokeStandardSettingsValues,
    SpokeToHubRouting SpokeToHubRouting
);

/// <summary>
/// Configuration for an SD-WAN hub.
/// </summary>
/// <param name="Id">Unique identifier of the hub.</param>
/// <param name="HostId">Host identifier for the hub.</param>
/// <param name="SiteId">Site identifier for the hub.</param>
/// <param name="NetworkIds">List of network identifiers associated with the hub.</param>
/// <param name="Routes">List of routes associated with the hub.</param>
/// <param name="PrimaryWan">Primary WAN for the hub. Example: 'WAN'.</param>
/// <param name="WanFailover">Indicates if WAN failover is enabled.</param>
public record SDWanHub(
    string Id,
    string HostId,
    string SiteId,
    IReadOnlyList<string> NetworkIds,
    IReadOnlyList<string> Routes,
    string PrimaryWan,
    bool WanFailover
);

/// <summary>
/// Configuration for an SD-WAN spoke.
/// </summary>
/// <param name="Id">Unique identifier of the spoke.</param>
/// <param name="HostId">Host identifier for the spoke.</param>
/// <param name="SiteId">Site identifier for the spoke.</param>
/// <param name="NetworkIds">List of network identifiers associated with the spoke.</param>
/// <param name="Routes">List of routes associated with the spoke.</param>
/// <param name="PrimaryWan">Primary WAN for the spoke. Example: 'WAN'.</param>
/// <param name="WanFailover">Indicates if WAN failover is enabled.</param>
/// <param name="HubsPriority">Priority list of hubs for the spoke.</param>
public record SDWanSpoke(
    string Id,
    string HostId,
    string SiteId,
    IReadOnlyList<string> NetworkIds,
    IReadOnlyList<string> Routes,
    string PrimaryWan,
    bool WanFailover,
    IReadOnlyList<string>? HubsPriority
);

/// <summary>
/// Variant of SD-WAN configuration. Values: distributed, failover, single.
/// </summary>
public enum SDWanVariant
{
    /// <summary>
    /// Distributed.
    /// </summary>
    Distributed,
    /// <summary>
    /// Failover.
    /// </summary>
    Failover,
    /// <summary>
    /// Single.
    /// </summary>
    Single,
}

/// <summary>
/// Specifies the mode of operation for spoke-to-hub tunnels in a network topology.
/// </summary>
/// <remarks>This enumeration defines the available modes for configuring spoke-to-hub tunnels,
/// each offering different trade-offs between resiliency, redundancy, and scalability.</remarks>
public enum  SpokeToHubTunnelsMode
{
    /// <summary>
    /// Maximum resiliency.
    /// </summary>
    MaxResiliency,
    /// <summary>
    /// Redundant.
    /// </summary>
    Redundant,
    /// <summary>
    /// Scalable.
    /// </summary>
    Scalable,
}

/// <summary>
/// Spoke-to-hub routing. Values: custom, scalable.
/// </summary>
public enum SpokeToHubRouting
{
    /// <summary>
    /// Custom routing.
    /// </summary>
    Custom,
    /// <summary>
    /// Geo routing.
    /// </summary>
    Geo,
}