using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager;

public interface ISiteManagerClient
{
    /// <summary>
    /// Gets a paged list of hosts.
    /// </summary>
    Task<PagedResponse<Host>> ListHostsAsync(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a host.
    /// </summary>
    /// <param name="id">The host ID.</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<DataResponse<Host>> GetHostAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paged list of sites.
    /// </summary>
    Task<PagedResponse<Site>> ListSitesAsync(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    Task<PagedResponse<HostWithDevices>> ListDevicesAsync(IEnumerable<string>? hostIds = null, DateTimeOffset? time = null, int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetricsAsync(MetricInterval type, DateTimeOffset? beginTimestamp = null, DateTimeOffset? endTimestamp = null, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetricsAsync(MetricInterval type, string? duration = null, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<IspMetric>>> QueryIspMetricsAsync(MetricInterval type, IEnumerable<SiteQuery> sites, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<BasicSDWanConfig>>> ListSDWanConfigsAsync(CancellationToken cancellationToken = default);

    Task<DataResponse<SDWanConfig>> GetSDWanConfigAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DataResponse<SDWanConfigStatus>> GetSDWanConfigStatusAsync(Guid id, CancellationToken cancellationToken = default);

}
