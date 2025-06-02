namespace UniFi.Net.SiteManager.Models;

public record Device(
    string Id,
    string Mac,
    string Name,
    string Model,
    string Shortname,
    string Ip,
    string ProductLine,
    string Status,
    string Version,
    string FirmwareStatus,
    string? UpdateAvailable,
    bool IsConsole,
    bool IsManaged,
    string StartupTime,
    string? AdoptionTime,
    string? Note,
    Uidb Uidb
);

public record Uidb(
    string Guid,
    string Id,
    UidbImages Images
);

public record UidbImages(
    string Default,
    string Nopadding,
    string Topology
);
