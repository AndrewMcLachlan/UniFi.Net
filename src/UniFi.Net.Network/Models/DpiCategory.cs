namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents a DPI (Deep Packet Inspection) category.
/// </summary>
/// <param name="Id">The unique identifier of the DPI category.</param>
/// <param name="Name">The name of the DPI category.</param>
public record DpiCategory(
    int Id,
    string Name
);
