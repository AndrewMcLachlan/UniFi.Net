namespace UniFi.Net.SiteManager.Models;

public record SDWanConfigStatus(
    string Id,
    string Fingerprint,
    long UpdatedAt,
    IReadOnlyList<SDWanConfigStatusHub> Hubs,
    IReadOnlyList<SDWanConfigStatusSpoke> Spokes,
    long LastGeneratedAt,
    string GenerateStatus,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
);

public record SDWanConfigStatusHub(
    string Id,
    string HostId,
    string SiteId,
    string Name,
    WanStatus PrimaryWanStatus,
    WanStatus SecondaryWanStatus,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings,
    int NumberOfTunnelsUsedByOtherFeatures,
    IReadOnlyList<SDWanConfigStatusNetwork> Networks,
    IReadOnlyList<object> Routes,
    string ApplyStatus
);

public record SDWanConfigStatusSpoke(
    string Id,
    string HostId,
    string SiteId,
    string Name,
    WanStatus PrimaryWanStatus,
    WanStatus SecondaryWanStatus,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings,
    int NumberOfTunnelsUsedByOtherFeatures,
    IReadOnlyList<SDWanConfigStatusNetwork> Networks,
    IReadOnlyList<SDWanConfigStatusRoute> Routes,
    IReadOnlyList<SDWanConfigStatusConnection> Connections,
    string ApplyStatus
);

public record WanStatus(
    string Ip,
    int? Latency,
    IReadOnlyList<object>? InternetIssues,
    string WanId
);

public record SDWanConfigStatusNetwork(
    string NetworkId,
    string Name,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
);

public record SDWanConfigStatusRoute(
    string RouteValue,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings
);

public record SDWanConfigStatusConnection(
    string HubId,
    IReadOnlyList<SDWanConfigStatusTunnel> Tunnels
);

public record SDWanConfigStatusTunnel(
    string SpokeWanId,
    string HubWanId,
    string Status
);
