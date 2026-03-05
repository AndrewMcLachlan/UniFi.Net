using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using UniFi.Net.Network.Exceptions;
using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network;

/// <inheritdoc />
public partial class NetworkClient : INetworkClient
{
    internal static readonly string ClientName = typeof(NetworkClient).FullName!;

    private const string PathPrefix = "proxy/network/integration/v1/";

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
        services.AddHttpClient(ClientName, client =>
        {
            HttpClientConfigurator.ConfigureHttpClient(client, host, apiKey);
        });
        var serviceProvider = services.BuildServiceProvider();
        _httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    }

    private async Task<T> PostAsJsonAsync<T>(string uri, object body, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = JsonContent.Create(body)
        };
        return await SendAsync<T>(request, ct);
    }

    private async Task PostAsJsonAsync(string uri, object body, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = JsonContent.Create(body)
        };
        await SendAsync(request, ct);
    }

    private async Task<T> PutAsJsonAsync<T>(string uri, object body, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = JsonContent.Create(body)
        };
        return await SendAsync<T>(request, ct);
    }

    private async Task<T> PatchAsJsonAsync<T>(string uri, object body, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, uri)
        {
            Content = JsonContent.Create(body)
        };
        return await SendAsync<T>(request, ct);
    }

    private async Task DeleteAsync(string uri, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        await SendAsync(request, ct);
    }

    private async Task<T> DeleteAsync<T>(string uri, CancellationToken ct)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);
        return await SendAsync<T>(request, ct);
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
                HttpStatusCode.BadRequest => new BadRequestException(result),
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
        string filterQuery = filter is not null ? $"&filter={filter}" : String.Empty;
        string offsetQuery = offset.HasValue ? $"&offset={offset.Value}" : String.Empty;
        string limitQuery = limit.HasValue ? $"&limit={limit.Value}" : String.Empty;
        return $"?{filterQuery}{offsetQuery}{limitQuery}".TrimStart('&').TrimEnd('?');
    }


}
