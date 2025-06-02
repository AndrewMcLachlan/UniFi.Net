namespace UniFi.Net.SiteManager.Models;

public record Site(
    string SiteId,
    string HostId,
    SiteMeta Meta,
    SiteStatistics Statistics,
    string Permission,
    bool IsOwner
);

public record SiteMeta(
    string Desc,
    string GatewayMac,
    string Name,
    string Timezone
);

public record SiteStatistics(
    SiteCounts Counts,
    SiteGateway Gateway,
    IReadOnlyList<object> InternetIssues, // Use a more specific type if known
    SiteIspInfo IspInfo,
    SitePercentages Percentages
);

public record SiteCounts(
    int CriticalNotification,
    int GatewayDevice,
    int GuestClient,
    int LanConfiguration,
    int OfflineDevice,
    int OfflineGatewayDevice,
    int OfflineWifiDevice,
    int OfflineWiredDevice,
    int PendingUpdateDevice,
    int TotalDevice,
    int WanConfiguration,
    int WifiClient,
    int WifiConfiguration,
    int WifiDevice,
    int WiredClient,
    int WiredDevice
);

public record SiteGateway(
    string HardwareId,
    string InspectionState,
    string IpsMode,
    SiteIpsSignature IpsSignature,
    string Shortname
);

public record SiteIpsSignature(
    int RulesCount,
    string Type
);

public record SiteIspInfo(
    string Name,
    string Organization
);

public record SitePercentages(
    int WanUptime
);