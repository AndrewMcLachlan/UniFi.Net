using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<AclRule>> ListAclRules(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<AclRule>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AclRule> GetAclRule(Guid siteId, Guid ruleId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules/{ruleId}";

        return GetFromJsonAsync<AclRule>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AclRule> CreateAclRule(Guid siteId, CreateAclRuleRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules";

        return PostAsJsonAsync<AclRule>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AclRule> UpdateAclRule(Guid siteId, Guid ruleId, UpdateAclRuleRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules/{ruleId}";

        return PutAsJsonAsync<AclRule>(path, request, cancellationToken);
    }

    /// <inheritdoc />
    public Task DeleteAclRule(Guid siteId, Guid ruleId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules/{ruleId}";

        return DeleteAsync(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AclRuleOrdering> GetAclRuleOrdering(Guid siteId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules/ordering";

        return GetFromJsonAsync<AclRuleOrdering>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AclRuleOrdering> UpdateAclRuleOrdering(Guid siteId, UpdateAclRuleOrderingRequest request, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/acl-rules/ordering";

        return PutAsJsonAsync<AclRuleOrdering>(path, request, cancellationToken);
    }
}
