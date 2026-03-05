using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<DnsPolicy>> ListDnsPolicies(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/dns/policies";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<DnsPolicy>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DnsPolicy> GetDnsPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/dns/policies/{policyId}";

        return GetFromJsonAsync<DnsPolicy>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DnsPolicy> CreateDnsPolicy(Guid siteId, CreateDnsPolicyRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/dns/policies";

        return PostAsJsonAsync<DnsPolicy>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<DnsPolicy> UpdateDnsPolicy(Guid siteId, Guid policyId, UpdateDnsPolicyRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/dns/policies/{policyId}";

        return PutAsJsonAsync<DnsPolicy>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteDnsPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/dns/policies/{policyId}";

        return DeleteAsync(path, cancellationToken);
    }
}
