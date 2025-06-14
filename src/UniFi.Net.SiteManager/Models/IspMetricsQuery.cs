namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// A query for ISP metric data within a specific time range.
/// </summary>
public record IspMetricsQuery
{
    /// <summary>
    /// Gets the start date time.
    /// </summary>
    public required DateTimeOffset BeginTimestamp { get; init; }

    /// <summary>
    /// Gets the end date time.
    /// </summary>
    public required DateTimeOffset EndTimestamp { get; init; }

    /// <summary>
    /// Get the host ID to query metrics for.
    /// </summary>
    public required string HostId { get; init; }

    /// <summary>
    /// Gets the unique identifier for the site.
    /// </summary>
    public required string SiteId { get; init; }
}
