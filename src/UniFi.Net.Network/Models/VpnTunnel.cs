using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of VPN tunnel.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum VpnTunnelType
{
    /// <summary>
    /// IPsec VPN tunnel.
    /// </summary>
    Ipsec = 0,

    /// <summary>
    /// OpenVPN tunnel.
    /// </summary>
    OpenVpn = 1,

    /// <summary>
    /// WireGuard VPN tunnel.
    /// </summary>
    WireGuard = 2,
}

/// <summary>
/// Represents a site-to-site VPN tunnel.
/// </summary>
/// <param name="Id">The unique identifier of the VPN tunnel.</param>
/// <param name="Name">The name of the VPN tunnel.</param>
/// <param name="Type">The type of VPN tunnel.</param>
/// <param name="Metadata">The metadata for the VPN tunnel.</param>
public record VpnTunnel(
    Guid Id,
    string Name,
    VpnTunnelType Type,
    ResourceMetadata Metadata
);
