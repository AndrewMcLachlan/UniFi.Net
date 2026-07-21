namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a switch stack.
/// </summary>
/// <param name="Id">The unique identifier of the switch stack.</param>
/// <param name="Name">The name of the switch stack.</param>
/// <param name="Metadata">The metadata for the switch stack.</param>
/// <param name="Lags">The LAGs that belong to the switch stack.</param>
/// <param name="Members">The devices that are members of the switch stack.</param>
public record SwitchStack(
    Guid Id,
    string Name,
    ResourceMetadata Metadata,
    IReadOnlyList<LagSummary> Lags,
    IReadOnlyList<SwitchStackMember> Members
);

/// <summary>
/// Represents a member device of a switch stack.
/// </summary>
/// <param name="DeviceId">The unique identifier of the member device.</param>
public record SwitchStackMember(
    Guid DeviceId
);
