using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    // Firewall Zones

    /// <inheritdoc />
    public Task<PagedResponse<FirewallZone>> ListFirewallZones(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/zones";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<FirewallZone>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallZone> GetFirewallZone(Guid siteId, Guid zoneId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/zones/{zoneId}";

        return GetFromJsonAsync<FirewallZone>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallZone> CreateFirewallZone(Guid siteId, CreateFirewallZoneRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/zones";

        return PostAsJsonAsync<FirewallZone>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallZone> UpdateFirewallZone(Guid siteId, Guid zoneId, UpdateFirewallZoneRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/zones/{zoneId}";

        return PutAsJsonAsync<FirewallZone>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteFirewallZone(Guid siteId, Guid zoneId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/zones/{zoneId}";

        return DeleteAsync(path, cancellationToken);
    }

    // Firewall Policies

    /// <inheritdoc />
    public Task<PagedResponse<FirewallPolicy>> ListFirewallPolicies(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<FirewallPolicy>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallPolicy> GetFirewallPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies/{policyId}";

        return GetFromJsonAsync<FirewallPolicy>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallPolicy> CreateFirewallPolicy(Guid siteId, CreateFirewallPolicyRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies";

        return PostAsJsonAsync<FirewallPolicy>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallPolicy> UpdateFirewallPolicy(Guid siteId, Guid policyId, UpdateFirewallPolicyRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies/{policyId}";

        return PutAsJsonAsync<FirewallPolicy>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallPolicy> PatchFirewallPolicy(Guid siteId, Guid policyId, PatchFirewallPolicyRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies/{policyId}";

        return PatchAsJsonAsync<FirewallPolicy>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteFirewallPolicy(Guid siteId, Guid policyId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies/{policyId}";

        return DeleteAsync(path, cancellationToken);
    }

    // Firewall Policy Ordering

    /// <inheritdoc />
    public Task<FirewallPolicyOrdering> GetFirewallPolicyOrdering(Guid siteId, Guid sourceFirewallZoneId, Guid destinationFirewallZoneId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies/ordering?sourceFirewallZoneId={sourceFirewallZoneId}&destinationFirewallZoneId={destinationFirewallZoneId}";

        return GetFromJsonAsync<FirewallPolicyOrdering>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<FirewallPolicyOrdering> UpdateFirewallPolicyOrdering(Guid siteId, UpdateFirewallPolicyOrderingRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/firewall/policies/ordering";

        return PutAsJsonAsync<FirewallPolicyOrdering>(path, request, cancellationToken);
    }
}
