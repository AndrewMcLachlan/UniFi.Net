namespace UniFi.Net.SiteManager.Models;

public abstract record HostWithDevices(
    string HostId,
    string HostName,
    IReadOnlyList<Device> Devices,
    DateTimeOffset UpdatedAt
);