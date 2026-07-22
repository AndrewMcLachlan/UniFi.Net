using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager;

/// <summary>
/// Client for the UniFi Site Manager API.
/// </summary>
public interface ISiteManagerClient
{
    /// <summary>
    /// Gets a paged list of hosts.
    /// </summary>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <param name="nextToken">A token for retrieving the next page of results.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of hosts.</returns>
    Task<PagedResponse<Host>> ListHostsAsync(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a host by ID.
    /// </summary>
    /// <param name="id">The host ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The host.</returns>
    Task<DataResponse<Host>> GetHostAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paged list of sites.
    /// </summary>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <param name="nextToken">A token for retrieving the next page of results.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of sites.</returns>
    Task<PagedResponse<Site>> ListSitesAsync(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paged list of devices, optionally filtered by host IDs.
    /// </summary>
    /// <param name="hostIds">Optional list of host IDs to filter the results.</param>
    /// <param name="time">Optional timestamp to filter devices by last processed time.</param>
    /// <param name="pageSize">The number of items to return per page.</param>
    /// <param name="nextToken">A token for retrieving the next page of results.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of hosts with their devices.</returns>
    Task<PagedResponse<HostWithDevices>> ListDevicesAsync(IEnumerable<string>? hostIds = null, DateTimeOffset? time = null, int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets ISP metrics for the specified interval, optionally filtered by time range.
    /// </summary>
    /// <param name="type">The metric interval (5-minute or 1-hour).</param>
    /// <param name="beginTimestamp">The earliest timestamp to retrieve data from.</param>
    /// <param name="endTimestamp">The latest timestamp to retrieve data up to.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>ISP metrics data.</returns>
    Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetricsAsync(MetricInterval type, DateTimeOffset? beginTimestamp = null, DateTimeOffset? endTimestamp = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets ISP metrics for the specified interval, filtered by duration.
    /// </summary>
    /// <param name="type">The metric interval (5-minute or 1-hour).</param>
    /// <param name="duration">The time range of metrics to retrieve, ending at the current time. Valid values: 24h (5-minute interval), 7d or 30d (1-hour interval).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>ISP metrics data.</returns>
    Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetricsAsync(MetricInterval type, string duration, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries ISP metrics for specific sites.
    /// </summary>
    /// <param name="type">The metric interval (5-minute or 1-hour).</param>
    /// <param name="sites">The site queries specifying host/site IDs and time ranges.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>ISP metrics data for the queried sites.</returns>
    Task<DataResponse<IspMetricsQueryResult>> QueryIspMetricsAsync(MetricInterval type, IEnumerable<IspMetricsQuery> sites, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of SD-WAN configurations.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A list of SD-WAN configurations.</returns>
    Task<DataResponse<IReadOnlyList<BasicSDWanConfig>>> ListSDWanConfigsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets an SD-WAN configuration by ID.
    /// </summary>
    /// <param name="id">The SD-WAN configuration ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The SD-WAN configuration.</returns>
    Task<DataResponse<SDWanConfig>> GetSDWanConfigAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the status of an SD-WAN configuration.
    /// </summary>
    /// <param name="id">The SD-WAN configuration ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The SD-WAN configuration status.</returns>
    Task<DataResponse<SDWanConfigStatus>> GetSDWanConfigStatusAsync(Guid id, CancellationToken cancellationToken = default);
}
