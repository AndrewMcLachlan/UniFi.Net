namespace UniFi.Net.SiteManager.Models;

public record BasicSDWanConfig(Guid Id, string Name, string Type);

public record SDWanConfig(
    Guid Id,
    string Name,
    string Type,
    string Variant,
    SDWanSettings Settings,
    IReadOnlyList<SDWanHub> Hubs,
    IReadOnlyList<SDWanSpoke> Spokes
) : BasicSDWanConfig(Id, Name, Type);

public record SDWanSettings(
    object? HubsInterconnect,
    string SpokeToHubTunnelsMode,
    bool SpokesAutoScaleAndNatEnabled,
    string SpokesAutoScaleAndNatRange,
    bool SpokesIsolate,
    bool SpokeStandardSettingsEnabled,
    object? SpokeStandardSettingsValues,
    string SpokeToHubRouting
);

public record SDWanHub(
    string Id,
    string HostId,
    string SiteId,
    IReadOnlyList<string> NetworkIds,
    IReadOnlyList<string> Routes,
    string PrimaryWan,
    bool WanFailover
);

public record SDWanSpoke(
    string Id,
    string HostId,
    string SiteId,
    IReadOnlyList<string> NetworkIds,
    IReadOnlyList<string> Routes,
    string PrimaryWan,
    bool WanFailover,
    IReadOnlyList<string>? HubsPriority
);
