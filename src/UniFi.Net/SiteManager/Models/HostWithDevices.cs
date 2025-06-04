namespace UniFi.Net.SiteManager.Models;

public record HostWithDevices(
    string HostId,
    string HostName,
    IReadOnlyList<Device> Devices,
    DateTimeOffset UpdatedAt
);