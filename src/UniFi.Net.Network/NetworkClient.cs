using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UniFi.Net.Network.Exceptions;
using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;
using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network;

/// <inheritdoc />
public class NetworkClient : INetworkClient
{
    internal static readonly string ClientName = typeof(NetworkClient).FullName!;

    private readonly IHttpClientFactory _httpClientFactory;


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

        // Manually create an IHttpClientFactory for on-demand HttpClient creation
        var services = new ServiceCollection();
        services.AddHttpClient<NetworkClient>("NetworkClient", (provider, client) =>
        {
            HttpClientConfigurator.ConfigureHttpClient(client, host, apiKey);
        });
        var serviceProvider = services.BuildServiceProvider();
        _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    }

    /// <inheritdoc />
    public Task<ApplicationInfo> GetApplicationInfo(CancellationToken cancellationToken = default) =>
        GetFromJsonAsync<ApplicationInfo>("proxy/network/integration/v1/info", cancellationToken);

    /// <inheritdoc />
    public Task<PagedResponse<Site>> ListSites(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        const string path = "proxy/network/integration/v1/sites";

        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Site>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<DeviceSummary>> ListDevices(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
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
    public Task PowerCyclePort(int portIdx, Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/devices/{deviceId}/interfaces/ports/{portIdx}/actions";
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(new { action = PortAction.PowerCycle })
        };

        return SendAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task RestartDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/devices/{deviceId}/actions";
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(new { action = DeviceAction.Restart })
        };

        return SendAsync(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<Client>> ListClients(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
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
    public Task<AuthorizeClientGuestAccessResponse> AuthorizeClientGuestAccess(Guid siteId, Guid clientId, long? timeLimitMinutes = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/clients/{clientId}/actions";
        var requestBody = new
        {
            action = ClientAction.AuthorizeGuestAccess,
            timeLimitMinutes,
            dataUsageLimitMBytes,
            rxRateLimitKbps,
            txRateLimitKbps
        };
        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(requestBody)
        };

        return SendAsync<AuthorizeClientGuestAccessResponse>(request, cancellationToken);
    }

    /// <inheritdoc />
    public Task<PagedResponse<Voucher>> ListVouchers(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/hotspot/vouchers";

        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Voucher>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Voucher>> GenerateVouchers(Guid siteId, string name, long authorizedGuestLimit, long timeLimitMinutes, int? count = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/hotspot/vouchers";

        var requestBody = new
        {
            name,
            authorizedGuestLimit,
            timeLimitMinutes,
            dataUsageLimitMBytes,
            rxRateLimitKbps,
            txRateLimitKbps,
            count
        };

        var request = new HttpRequestMessage(HttpMethod.Post, path)
        {
            Content = JsonContent.Create(requestBody)
        };

        var result = await SendAsync<GenerateVouchersResponse>(request, cancellationToken);

        return result.Vouchers;
    }

    /// <inheritdoc />
    public async Task<long> DeleteVouchers(Guid siteId, IFilter filter, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/hotspot/vouchers";
        string query = $"?filter={filter}";

        var request = new HttpRequestMessage(HttpMethod.Delete, path + query);

        var result = await SendAsync<DeleteVouchersResponse>(request, cancellationToken);

        return result.VouchersDeleted;
    }

    /// <inheritdoc />
    public Task<Voucher> GetVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/hotspot/vouchers/{voucherId}";
        return GetFromJsonAsync<Voucher>(path, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<long> DeleteVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default)
    {
        string path = $"proxy/network/integration/v1/sites/{siteId}/hotspot/vouchers/{voucherId}";

        var request = new HttpRequestMessage(HttpMethod.Delete, path);

        var result = await SendAsync<DeleteVouchersResponse>(request, cancellationToken);

        return result.VouchersDeleted;
    }

    private async Task<T> GetFromJsonAsync<T>(string requestUri, CancellationToken cancellationToken = default)
    {
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var responseMessage = await SendAsync(requestMessage, cancellationToken) ??
                   throw new InvalidOperationException($"Failed to deserialize response from {requestUri}.");

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
        var client = GetClient();
        try
        {
            var responseMessage = await client.SendAsync(request, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                await ProcessErrorResponse(request, responseMessage, cancellationToken);
            }

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

    private async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var client = GetClient();
        var responseMessage = await client.SendAsync(request, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            await ProcessErrorResponse(request, responseMessage, cancellationToken);
        }

        return responseMessage;
    }

    private static async Task ProcessErrorResponse(HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException($"Error from {request.RequestUri}: {response.ReasonPhrase}");

            throw result.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException(result),
                HttpStatusCode.Unauthorized => new UnauthorizedException(result),
                _ => new UniFiNetworkException(result)
            };
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Error deserializing response from {request.RequestUri}: {ex.Message}", ex);
        }
    }

    private HttpClient GetClient()
    {
        HttpClient client;

        client = _httpClientFactory.CreateClient(ClientName);
        if (client.BaseAddress == null)
        {
            throw new InvalidOperationException("Base address is not set for NetworkClient.");
        }
        return client;
    }

    private static string BuildPagingQuery(IFilter? filter, int? offset, int? limit)
    {
        string filterQuery = filter is not null ? $"&filter={filter}" : string.Empty;
        string offsetQuery = offset.HasValue ? $"&offset={offset.Value}" : string.Empty;
        string limitQuery = limit.HasValue ? $"&limit={limit.Value}" : string.Empty;
        return $"?{filterQuery}{offsetQuery}{limitQuery}".TrimStart('&').TrimEnd('?');
    }


}
