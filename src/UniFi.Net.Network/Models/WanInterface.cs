namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a WAN interface.
/// </summary>
/// <param name="Id">The unique identifier of the WAN interface.</param>
/// <param name="Name">The name of the WAN interface.</param>
public record WanInterface(
    Guid Id,
    string Name
);
