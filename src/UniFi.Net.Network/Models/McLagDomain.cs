using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The role of a peer within a multi-chassis LAG (MC-LAG) domain.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum McLagPeerRole
{
    /// <summary>
    /// The top peer of the MC-LAG domain.
    /// </summary>
    Top = 0,

    /// <summary>
    /// The bottom peer of the MC-LAG domain.
    /// </summary>
    Bottom = 1,
}

/// <summary>
/// Represents a multi-chassis LAG (MC-LAG) domain.
/// </summary>
/// <param name="Id">The unique identifier of the MC-LAG domain.</param>
/// <param name="Name">The name of the MC-LAG domain.</param>
/// <param name="Metadata">The metadata for the MC-LAG domain.</param>
/// <param name="Lags">The LAGs that belong to the MC-LAG domain.</param>
/// <param name="Peers">The peer devices that form the MC-LAG domain.</param>
public record McLagDomain(
    Guid Id,
    string Name,
    ResourceMetadata Metadata,
    IReadOnlyList<LagSummary> Lags,
    IReadOnlyList<McLagPeer> Peers
);

/// <summary>
/// Represents a peer device within an MC-LAG domain.
/// </summary>
/// <param name="DeviceId">The unique identifier of the peer device.</param>
/// <param name="LinkPortIdxs">The indices of the ports forming the peer link.</param>
/// <param name="Role">The role of the peer within the MC-LAG domain.</param>
public record McLagPeer(
    Guid DeviceId,
    IReadOnlyList<int> LinkPortIdxs,
    McLagPeerRole Role
);
