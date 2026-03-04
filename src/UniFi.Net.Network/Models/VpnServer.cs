using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of VPN server.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum VpnServerType
{
    /// <summary>
    /// WireGuard VPN server.
    /// </summary>
    WireGuard = 0,

    /// <summary>
    /// L2TP VPN server.
    /// </summary>
    L2tp = 1,
}

/// <summary>
/// Represents a VPN server.
/// </summary>
/// <param name="Id">The unique identifier of the VPN server.</param>
/// <param name="Name">The name of the VPN server.</param>
/// <param name="Type">The type of VPN server.</param>
/// <param name="Enabled">Indicates whether the VPN server is enabled.</param>
/// <param name="Metadata">The metadata for the VPN server.</param>
public record VpnServer(
    Guid Id,
    string Name,
    VpnServerType Type,
    bool Enabled,
    ResourceMetadata Metadata
);
