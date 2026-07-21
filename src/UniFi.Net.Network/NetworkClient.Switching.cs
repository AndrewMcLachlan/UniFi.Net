using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<Lag>> ListLags(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/switching/lags";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Lag>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Lag> GetLag(Guid siteId, Guid lagId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/switching/lags/{lagId}";

        return GetFromJsonAsync<Lag>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<McLagDomain>> ListMcLagDomains(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/switching/mc-lag-domains";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<McLagDomain>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<McLagDomain> GetMcLagDomain(Guid siteId, Guid mcLagDomainId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/switching/mc-lag-domains/{mcLagDomainId}";

        return GetFromJsonAsync<McLagDomain>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<SwitchStack>> ListSwitchStacks(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/switching/switch-stacks";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<SwitchStack>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<SwitchStack> GetSwitchStack(Guid siteId, Guid switchStackId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/switching/switch-stacks/{switchStackId}";

        return GetFromJsonAsync<SwitchStack>(path, cancellationToken);
    }
}
