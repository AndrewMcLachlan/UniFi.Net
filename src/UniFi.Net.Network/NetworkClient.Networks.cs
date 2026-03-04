using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<SiteNetwork>> ListNetworks(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/networks";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<SiteNetwork>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SiteNetwork> GetNetwork(Guid siteId, Guid networkId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/networks/{networkId}";

        return GetFromJsonAsync<SiteNetwork>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SiteNetwork> CreateNetwork(Guid siteId, CreateNetworkRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/networks";

        return PostAsJsonAsync<SiteNetwork>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SiteNetwork> UpdateNetwork(Guid siteId, Guid networkId, UpdateNetworkRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/networks/{networkId}";

        return PutAsJsonAsync<SiteNetwork>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteNetwork(Guid siteId, Guid networkId, bool? force = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/networks/{networkId}";
        if (force == true)
        {
            path += "?force=true";
        }

        return DeleteAsync(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<NetworkReferences> GetNetworkReferences(Guid siteId, Guid networkId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/networks/{networkId}/references";

        return GetFromJsonAsync<NetworkReferences>(path, cancellationToken);
    }
}
