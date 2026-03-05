using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<WanInterface>> ListWanInterfaces(Guid siteId, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/wans";
        string query = BuildPagingQuery(null, offset, limit);

        return GetFromJsonAsync<PagedResponse<WanInterface>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<VpnTunnel>> ListSiteToSiteVpnTunnels(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/vpn/site-to-site-tunnels";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<VpnTunnel>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<VpnServer>> ListVpnServers(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/vpn/servers";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<VpnServer>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<RadiusProfile>> ListRadiusProfiles(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/radius/profiles";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<RadiusProfile>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<DeviceTag>> ListDeviceTags(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/device-tags";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<DeviceTag>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<DpiCategory>> ListDpiCategories(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}dpi/categories";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<DpiCategory>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<DpiApplication>> ListDpiApplications(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}dpi/applications";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<DpiApplication>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<Country>> ListCountries(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}countries";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Country>>(path + query, cancellationToken);
    }
}
