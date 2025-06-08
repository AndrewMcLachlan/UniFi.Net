using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.UniFiIdentities;

/// <inheritdoc />
public class UniFiIdentityClient : ClientBase, IUniFiIdentityClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UniFiIdentityClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public UniFiIdentityClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UniFiIdentityClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public UniFiIdentityClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc />
    public async Task SendIdentityInvitationsAsync(List<IdentityInvitation> invitations, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = invitations.Select(invitation => new
        {
            user_id = invitation.UserId,
            email = invitation.Email
        });

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/users/identity/invitations", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task<List<IdentityResource>> FetchAvailableResourcesAsync(string? resourceType = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var query = string.IsNullOrEmpty(resourceType) ? string.Empty : $"?resource_type={resourceType}";
        var response = await httpClient.GetAsync($"/api/v1/developer/users/identity/assignments{query}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<IdentityResource>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task AssignResourcesToUserAsync(string userId, string resourceType, List<string> resourceIds, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = new
        {
            resource_type = resourceType,
            resource_ids = resourceIds
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"/api/v1/developer/users/{userId}/identity/assignments", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task<List<IdentityResource>> FetchResourcesAssignedToUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/users/{userId}/identity/assignments", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<IdentityResource>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task AssignResourcesToUserGroupAsync(string groupId, string resourceType, List<string> resourceIds, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = new
        {
            resource_type = resourceType,
            resource_ids = resourceIds
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"/api/v1/developer/user_groups/{groupId}/identity/assignments", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task<List<IdentityResource>> FetchResourcesAssignedToUserGroupAsync(string groupId, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/user_groups/{groupId}/identity/assignments", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<IdentityResource>>(json) ?? [];
    }
}
