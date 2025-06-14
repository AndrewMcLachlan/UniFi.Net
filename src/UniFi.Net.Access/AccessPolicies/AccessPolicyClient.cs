using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.AccessPolicies;

/// <inheritdoc/>
public class AccessPolicyClient : ClientBase, IAccessPolicyClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccessPolicyClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public AccessPolicyClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AccessPolicyClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public AccessPolicyClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc/>
    public async Task<List<AccessPolicySummary>> FetchAllAccessPoliciesAsync(CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync("/api/v1/developer/access_policies", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<AccessPolicySummary>>(json) ?? [];
    }

    /// <inheritdoc/>
    public async Task<AccessPolicyDetails> FetchAccessPolicyAsync(string policyId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/access_policies/{policyId}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<AccessPolicyDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize access policy details.");
    }

    /// <inheritdoc/>
    public async Task CreateAccessPolicyAsync(string name, List<string> resourceIds, string scheduleId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var payload = new
        {
            name,
            resources = resourceIds,
            schedule_id = scheduleId,
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/access_policies", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc/>
    public async Task DeleteAccessPolicyAsync(string policyId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.DeleteAsync($"/api/v1/developer/access_policies/{policyId}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
