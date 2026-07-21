using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The type of a link aggregation group (LAG).
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum LagType
{
    /// <summary>
    /// A LAG local to a single device.
    /// </summary>
    Local = 0,

    /// <summary>
    /// A multi-chassis LAG spanning the devices of an MC-LAG domain.
    /// </summary>
    MultiChassis = 1,

    /// <summary>
    /// A LAG spanning the members of a switch stack.
    /// </summary>
    SwitchStack = 2,
}

/// <summary>
/// Represents a link aggregation group (LAG).
/// </summary>
/// <param name="Id">The unique identifier of the LAG.</param>
/// <param name="Type">The type of the LAG.</param>
/// <param name="Members">The member ports that make up the LAG.</param>
/// <param name="Metadata">The metadata for the LAG.</param>
/// <param name="McLagDomainId">The identifier of the owning MC-LAG domain. Set only when <see cref="Type"/> is <see cref="LagType.MultiChassis"/>.</param>
/// <param name="SwitchStackId">The identifier of the owning switch stack. Set only when <see cref="Type"/> is <see cref="LagType.SwitchStack"/>.</param>
public record Lag(
    Guid Id,
    LagType Type,
    IReadOnlyList<LagMember> Members,
    ResourceMetadata Metadata,
    Guid? McLagDomainId,
    Guid? SwitchStackId
);

/// <summary>
/// Represents a member of a link aggregation group.
/// </summary>
/// <param name="DeviceId">The unique identifier of the device contributing the ports.</param>
/// <param name="PortIdxs">The indices of the ports that participate in the LAG.</param>
public record LagMember(
    Guid DeviceId,
    IReadOnlyList<int> PortIdxs
);

/// <summary>
/// Represents a link aggregation group as nested within an MC-LAG domain or switch stack.
/// </summary>
/// <param name="Id">The unique identifier of the LAG.</param>
/// <param name="Members">The member ports that make up the LAG.</param>
/// <param name="Metadata">The metadata for the LAG.</param>
public record LagSummary(
    Guid Id,
    IReadOnlyList<LagMember> Members,
    ResourceMetadata Metadata
);
