using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.Spaces;

/// <inheritdoc />
public class SpaceClient : ClientBase, ISpaceClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SpaceClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public SpaceClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SpaceClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public SpaceClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc />
    public async Task<List<DoorGroupSummary>> FetchAllDoorGroupsAsync(CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync("/api/v1/developer/door_groups", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<DoorGroupSummary>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task<DoorGroupDetails> FetchDoorGroupAsync(string doorGroupId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/door_groups/{doorGroupId}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<DoorGroupDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize door group details.");
    }

    /// <inheritdoc />
    public async Task CreateDoorGroupAsync(string name, List<string> resourceIds, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var payload = new
        {
            group_name = name,
            resources = resourceIds
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/door_groups", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task DeleteDoorGroupAsync(string doorGroupId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.DeleteAsync($"/api/v1/developer/door_groups/{doorGroupId}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task<List<DoorSummary>> FetchAllDoorsAsync(CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync("/api/v1/developer/doors", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<DoorSummary>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task<DoorDetails> FetchDoorAsync(string doorId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/doors/{doorId}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<DoorDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize door details.");
    }

    /// <inheritdoc />
    public async Task UnlockDoorAsync(string doorId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.PutAsync($"/api/v1/developer/doors/{doorId}/unlock", null, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
