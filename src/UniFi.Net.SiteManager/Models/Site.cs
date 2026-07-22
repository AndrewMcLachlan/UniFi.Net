using System.Text.Json.Serialization;

namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Represents a site managed by a host.
/// </summary>
/// <param name="SiteId">Unique identifier of the site.</param>
/// <param name="HostId">Unique identifier of the host device managing this site.</param>
/// <param name="Meta">Site metadata including name, description, time zone, and gateway MAC address. Structure may vary depending on the UniFi Network version.</param>
/// <param name="Statistics">Site statistics including device counts, client counts, and network performance metrics. Structure may vary depending on the UniFi Network version.</param>
/// <param name="Permission">Permission level of the current user for this site (admin, readonly, etc.).</param>
/// <param name="IsOwner">Indicates if the current user is the owner of this site.</param>
public record Site(
    string SiteId,
    string HostId,
    SiteMeta Meta,
    SiteStatistics Statistics,
    string Permission,
    bool IsOwner
);

/// <summary>
/// Site metadata.
/// </summary>
/// <param name="Desc">Site description</param>
/// <param name="GatewayMac">Gateway MAC address.</param>
/// <param name="Name">Site name.</param>
/// <param name="TimeZone">Site time zone.</param>
public record SiteMeta(
    string Desc,
    string GatewayMac,
    string Name,
    [property:JsonPropertyName("timezone")]string TimeZone
);

/// <summary>
/// Site statistics.
/// </summary>
/// <param name="Counts">Device and client counts, keyed by count name.</param>
/// <param name="Gateway">Gateway information.</param>
/// <param name="InternetIssues">Internet issues reported for the site.</param>
/// <param name="IspInfo">ISP information.</param>
/// <param name="Percentages">Percentage metrics, keyed by metric name.</param>
public record SiteStatistics(
    IReadOnlyDictionary<string, int> Counts,
    SiteGateway Gateway,
    IReadOnlyList<object> InternetIssues,
    SiteIspInfo IspInfo,
    IReadOnlyDictionary<string, int> Percentages
);

/// <summary>
/// Gateway information for a site.
/// </summary>
/// <param name="HardwareId">Hardware identifier of the gateway.</param>
/// <param name="InspectionState">The traffic inspection state.</param>
/// <param name="IpsMode">The intrusion prevention system mode.</param>
/// <param name="IpsSignature">The intrusion prevention system signature information.</param>
/// <param name="Shortname">Short identifier of the gateway model.</param>
public record SiteGateway(
    string HardwareId,
    string InspectionState,
    string IpsMode,
    SiteIpsSignature IpsSignature,
    string Shortname
);

/// <summary>
/// Intrusion prevention system signature information.
/// </summary>
/// <param name="RulesCount">The number of rules in the signature set.</param>
/// <param name="Type">The signature set type.</param>
public record SiteIpsSignature(
    int RulesCount,
    string Type
);

/// <summary>
/// ISP information.
/// </summary>
/// <param name="Name">The ISP name.</param>
/// <param name="Organization">The ISP organization name.</param>
public record SiteIspInfo(
    string Name,
    string Organization
);
