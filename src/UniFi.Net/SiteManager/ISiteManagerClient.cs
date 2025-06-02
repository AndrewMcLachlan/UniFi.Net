
using System;
using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager;

public interface ISiteManagerClient
{
    /// <summary>
    /// Gets a paged list of hosts for a site.
    /// </summary>
    Task<PagedResponse<Host>> ListHosts(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    Task<DataResponse<Host>> GetHost(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paged list of sites.
    /// </summary>
    Task<PagedResponse<Site>> ListSites(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    Task<PagedResponse<Device>> ListDevices(IEnumerable<string>? hostIds = null, DateTimeOffset? time = null, int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetrics(MetricInterval type, DateTimeOffset? beginTimestamp = null, DateTimeOffset? endTimestamp = null, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetrics(MetricInterval type, string? duration = null, CancellationToken cancellationToken = default);

    Task<DataResponse<IReadOnlyList<IspMetric>>> QueryIspMetrics(MetricInterval type, IEnumerable<SiteQuery> sites, CancellationToken cancellationToken = default);
}
