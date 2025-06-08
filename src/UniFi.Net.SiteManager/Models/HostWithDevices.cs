namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Basic host information and attached devices.
/// </summary>
/// <param name="HostId">Unique identifier of the host device.</param>
/// <param name="HostName">Name of the host device.</param>
/// <param name="Devices">Array of devices managed by this host.+</param>
/// <param name="UpdatedAt">Last update time.</param>
public record HostWithDevices(
    string HostId,
    string HostName,
    IReadOnlyList<Device> Devices,
    DateTimeOffset UpdatedAt
);