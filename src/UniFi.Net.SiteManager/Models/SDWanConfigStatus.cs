using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Represents the status of an SD-WAN configuration.
/// </summary>
/// <param name="Id">Unique identifier of the SD-WAN configuration.</param>
/// <param name="Fingerprint">A unique identifier representing the current state of the configuration.</param>
/// <param name="UpdatedAt">The timestamp of the last update to the SD-WAN configuration.</param>
/// <param name="Hubs">List of hubs in SD-WAN configuration.</param>
/// <param name="Spokes">A list of spokes associated with the SD-WAN config.</param>
/// <param name="LastGeneratedAt">The timestamp of the last generation of the SD-WAN configuration.</param>
/// <param name="GenerateStatus">The status of the configuration generation process. Values: OK, GENERATING, GENERATE_FAILED.</param>
/// <param name="Errors">A list of error messages related to the configuration, if any.</param>
/// <param name="Warnings">A list of warning messages related to the configuration, if any.</param>
public record SDWanConfigStatus(
    string Id,
    string Fingerprint,
    long UpdatedAt,
    IReadOnlyList<SDWanConfigStatusHub> Hubs,
    IReadOnlyList<SDWanConfigStatusSpoke> Spokes,
    long LastGeneratedAt,
    GenerateStatus GenerateStatus,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
);

/// <summary>
/// Represents the status of an SD-WAN hub.
/// </summary>
/// <param name="Id">Unique identifier of the hub.</param>
/// <param name="HostId">Host identifier for the hub.</param>
/// <param name="SiteId">Site identifier for the hub.</param>
/// <param name="Name">Name of the hub.</param>
/// <param name="PrimaryWanStatus">Status of the primary WAN connection.</param>
/// <param name="SecondaryWanStatus">Status of the secondary WAN connection.</param>
/// <param name="Errors">A list of error messages related to the hub, if any.</param>
/// <param name="Warnings">A list of warning messages related to the hub, if any.</param>
/// <param name="NumberOfTunnelsUsedByOtherFeatures">Number of tunnels used by other features.</param>
/// <param name="Networks">List of networks associated with the hub.</param>
/// <param name="Routes">List of routes associated with the hub.</param>
/// <param name="ApplyStatus">The current status of the hub configuration application. Values: ok, creating, updating, removing, createFailed, updateFailed, removeFailed.</param>
public record SDWanConfigStatusHub(
    string Id,
    string HostId,
    string SiteId,
    string Name,
    WanStatus PrimaryWanStatus,
    WanStatus SecondaryWanStatus,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings,
    int NumberOfTunnelsUsedByOtherFeatures,
    IReadOnlyList<SDWanConfigStatusNetwork> Networks,
    IReadOnlyList<object> Routes,
    ApplyStatus ApplyStatus
);

/// <summary>
/// Represents the status of an SD-WAN spoke.
/// </summary>
/// <param name="Id">Unique identifier of the spoke.</param>
/// <param name="HostId">Host identifier for the spoke.</param>
/// <param name="SiteId">Site identifier for the spoke.</param>
/// <param name="Name">Name of the spoke.</param>
/// <param name="PrimaryWanStatus">Status of the primary WAN connection.</param>
/// <param name="SecondaryWanStatus">Status of the secondary WAN connection.</param>
/// <param name="Errors">A list of error messages related to the spoke, if any.</param>
/// <param name="Warnings">A list of warning messages related to the spoke, if any.</param>
/// <param name="NumberOfTunnelsUsedByOtherFeatures">Number of tunnels used by other features.</param>
/// <param name="Networks">List of networks associated with the spoke.</param>
/// <param name="Routes">List of routes associated with the spoke.</param>
/// <param name="Connections">List of connections associated with the spoke.</param>
/// <param name="ApplyStatus">The current status of the spoke configuration application. Values: ok, creating, updating, removing, createFailed, updateFailed, removeFailed.</param>
public record SDWanConfigStatusSpoke(
    string Id,
    string HostId,
    string SiteId,
    string Name,
    WanStatus PrimaryWanStatus,
    WanStatus SecondaryWanStatus,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings,
    int NumberOfTunnelsUsedByOtherFeatures,
    IReadOnlyList<SDWanConfigStatusNetwork> Networks,
    IReadOnlyList<SDWanConfigStatusRoute> Routes,
    IReadOnlyList<SDWanConfigStatusConnection> Connections,
    string ApplyStatus
);

/// <summary>
/// Represents the status of a WAN connection.
/// </summary>
/// <param name="Ip">IP address of the WAN. Format: 10.0.0.1.</param>
/// <param name="Latency">Latency of the WAN connection in milliseconds.</param>
/// <param name="InternetIssues">List of issues experienced by the WAN, if any.</param>
/// <param name="WanId">Unique identifier of the WAN.</param>
public record WanStatus(
    string Ip,
    int? Latency,
    IReadOnlyList<object>? InternetIssues,
    string WanId
);

/// <summary>
/// Represents the status of a network in the SD-WAN configuration.
/// </summary>
/// <param name="NetworkId">Unique identifier of the network.</param>
/// <param name="Name">Name of the network.</param>
/// <param name="Errors">A list of error messages related to the network, if any.</param>
/// <param name="Warnings">A list of warning messages related to the network, if any.</param>
public record SDWanConfigStatusNetwork(
    string NetworkId,
    string Name,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
);

/// <summary>
/// Represents the status of a route in the SD-WAN configuration.
/// </summary>
/// <param name="RouteValue">Value of the route in CIDR format. Example: 10.0.0.0/24.</param>
/// <param name="Errors">A list of error messages related to the route, if any.</param>
/// <param name="Warnings">A list of warning messages related to the route, if any.</param>
public record SDWanConfigStatusRoute(
    string RouteValue,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
);

/// <summary>
/// Represents a connection in the SD-WAN configuration.
/// </summary>
/// <param name="HubId">Unique identifier of the hub associated with the connection.</param>
/// <param name="Tunnels">List of tunnels associated with the connection.</param>
public record SDWanConfigStatusConnection(
    string HubId,
    IReadOnlyList<SDWanConfigStatusTunnel> Tunnels
);

/// <summary>
/// Represents the status of a tunnel in the SD-WAN configuration.
/// </summary>
/// <param name="SpokeWanId">Unique identifier of the spoke WAN.</param>
/// <param name="HubWanId">Unique identifier of the hub WAN.</param>
/// <param name="Status">The current status of the tunnel connection. Values: connected, disconnected, pending.</param>
public record SDWanConfigStatusTunnel(
    string SpokeWanId,
    string HubWanId,
    TunnelStatus Status
);

/// <summary>
/// Represents the status of an operation applied to a configuration.
/// </summary>
/// <remarks>This enumeration is used to indicate the current state of a configuration operation, such as
/// creation, update, or removal. It provides detailed statuses to help track the progress or identify failures during
/// these operations.</remarks>
public enum ApplyStatus
{
    /// <summary>
    /// The configuration is OK.
    /// </summary>
    Ok,
    /// <summary>
    /// The configuration is being created.
    /// </summary>
    Creating,
    /// <summary>
    /// The configuration is being updated.
    /// </summary>
    Updating,
    /// <summary>
    /// The configuration is being removed.
    /// </summary>
    Removing,
    /// <summary>
    /// The creation of the configuration failed.
    /// </summary>
    CreateFailed,
    /// <summary>
    /// The update of the configuration failed.
    /// </summary>
    UpdateFailed,
    /// <summary>
    /// The removal of the configuration failed.
    /// </summary>
    RemoveFailed,
}

/// <summary>
/// Represents the connection status of a tunnel.
/// </summary>
/// <remarks>This enumeration is used to indicate the current state of a tunnel connection. It can be used to
/// determine whether a tunnel is actively connected, disconnected, or in the process of connecting.</remarks>
public enum TunnelStatus
{
    /// <summary>
    /// The tunnel is connected.
    /// </summary>
    Connected,
    /// <summary>
    /// The tunnel is disconnected.
    /// </summary>
    Disconnected,
    /// <summary>
    /// The tunnel connection is pending.
    /// </summary>
    Pending,
}

/// <summary>
/// Represents the status of a configuration generation process.
/// </summary>
/// <remarks>This enumeration is used to indicate the current state of a configuration generation operation. It
/// provides values to represent successful completion, ongoing generation, or a failure during the process.</remarks>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum GenerateStatus
{
    /// <summary>
    /// The generation of the configuration is OK.
    /// </summary>
    Ok,
    /// <summary>
    /// The configuration is currently being generated.
    /// </summary>
    Generating,
    /// <summary>
    /// The generation of the configuration failed.
    /// </summary>
    GenerateFailed,
}