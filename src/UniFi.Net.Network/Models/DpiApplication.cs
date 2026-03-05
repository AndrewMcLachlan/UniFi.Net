namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a DPI (Deep Packet Inspection) application.
/// </summary>
/// <param name="Id">The unique identifier of the DPI application.</param>
/// <param name="Name">The name of the DPI application.</param>
public record DpiApplication(
    int Id,
    string Name
);
