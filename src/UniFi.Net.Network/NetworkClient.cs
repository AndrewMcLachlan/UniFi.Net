using System.Net.Http.Json;
using System.Text.Json;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <inheritdoc />
public class NetworkClient : INetworkClient
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly Uri? _host;
    private readonly string? _apiKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public NetworkClient(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public NetworkClient(Uri host, string apiKey)
    {
        ArgumentNullException.ThrowIfNull(host);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        _host = host;
        _apiKey = apiKey;
    }

    /// <inheritdoc />
    public Task<ApplicationInfo> GetApplicationInfo(CancellationToken cancellationToken = default) =>
        GetFromJsonAsync<ApplicationInfo>("proxy/network/integration/v1/info", cancellationToken);

    /// <inheritdoc />
    public Task<PagedResponse<Site>> ListSites(string? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        const string path = "proxy/network/integration/v1/sites";

        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Site>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<DeviceSummary>> ListDevices(Guid siteId, string? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/devices";

        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<DeviceSummary>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Device> GetDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/devices/{deviceId}";

        return GetFromJsonAsync<Device>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task ExecutePortAction(int portIdx, Guid siteId, Guid deviceId, PortAction action, CancellationToken cancellationToken = default)
    {
        string path = $"v1/sites/{siteId}/devices/{deviceId}/ports/{portIdx}/actions";
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(new { action })
        };

        return SendAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task ExecuteDeviceAction(Guid siteId, Guid deviceId, DeviceAction action, CancellationToken cancellationToken = default)
    {
        string path = $"v1/sites/{siteId}/devices/{deviceId}/actions";
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(new { action })
        };

        return SendAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<Client>> ListClients(Guid siteId, string? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/clients";

        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Client>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Client> GetClient(Guid siteId, Guid clientId, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/clients/{clientId}";

        return GetFromJsonAsync<Client>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<ExecuteClientActionResponse> ExecuteClientAction(Guid siteId, Guid clientId, ClientAction action, long? timeLimitMinutes = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/clients/{clientId}/actions";
        var requestBody = new
        {
            action,
            timeLimitMinutes,
            dataUsageLimitMBytes,
            rxRateLimitKbps,
            txRateLimitKbps
        };
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(requestBody)
        };

        return SendAsync<ExecuteClientActionResponse>(request, cancellationToken);
    }

    private async Task<T> GetFromJsonAsync<T>(string requestUri, CancellationToken cancellationToken = default)
    {
        using var client = GetClient();

        try
        {
            var responseMessage = await client.GetAsync(requestUri, cancellationToken) ??
                   throw new InvalidOperationException($"Failed to deserialize response from {requestUri}.");

            responseMessage.EnsureSuccessStatusCode();

            var result = await responseMessage.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);

            return result ?? throw new InvalidOperationException($"Failed to deserialize response from {requestUri}.");
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Error fetching data from {requestUri}: {ex.Message}", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException($"The content type is not supported for {requestUri}: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Error deserializing response from {requestUri}: {ex.Message}", ex);
        }
    }

    private async Task<T> SendAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using var client = GetClient();
        try
        {
            var responseMessage = await client.SendAsync(request, cancellationToken) ??
                   throw new InvalidOperationException($"Failed to deserialize response from {request.RequestUri}.");
            responseMessage.EnsureSuccessStatusCode();
            var result = await responseMessage.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
            return result ?? throw new InvalidOperationException($"Failed to deserialize response from {request.RequestUri}.");
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Error fetching data from {request.RequestUri}: {ex.Message}", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException($"The content type is not supported for {request.RequestUri}: {ex.Message}", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Error deserializing response from {request.RequestUri}: {ex.Message}", ex);
        }
    }

    private async Task SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        using var client = GetClient();
        try
        {
            var responseMessage = await client.SendAsync(request, cancellationToken) ??
                   throw new InvalidOperationException($"Failed to deserialize response from {request.RequestUri}.");
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Error fetching data from {request.RequestUri}: {ex.Message}", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException($"The content type is not supported for {request.RequestUri}: {ex.Message}", ex);
        }
    }

    private HttpClient GetClient()
    {
        HttpClient client;

        if (_httpClientFactory == null)
        {
            client = new HttpClient();
            HttpClientConfigurator.ConfigureHttpClient(client, _host!, _apiKey!);
            return client;
        }

        client = _httpClientFactory.CreateClient("NetworkClient");
        if (client.BaseAddress == null)
        {
            throw new InvalidOperationException("Base address is not set for NetworkClient.");
        }
        return client;
    }

    private static string BuildPagingQuery(string? filter, int? offset, int? limit)
    {
        string filterQuery = string.IsNullOrWhiteSpace(filter) ? string.Empty : $"&filter={Uri.EscapeDataString(filter)}";
        string offsetQuery = offset.HasValue ? $"&offset={offset.Value}" : string.Empty;
        string limitQuery = limit.HasValue ? $"&limit={limit.Value}" : string.Empty;
        return $"?{filterQuery}{offsetQuery}{limitQuery}".TrimStart('&').TrimEnd('?');
    }
}
