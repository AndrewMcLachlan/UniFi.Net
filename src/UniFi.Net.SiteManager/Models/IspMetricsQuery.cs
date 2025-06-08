namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// A query for ISP metric data within a specific time range.
/// </summary>
public record IspMetricsQuery
{
    /// <summary>
    /// The start date time.
    /// </summary>
    public required DateTimeOffset BeginTimestamp { get; init; }

    /// <summary>
    /// The end date time.
    /// </summary>
    public required DateTimeOffset EndTimestamp { get; init; }


    public required string HostId { get; init; }

    public required string SiteId { get; init; }
}
