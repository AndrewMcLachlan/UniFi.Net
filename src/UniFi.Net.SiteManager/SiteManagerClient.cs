using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using UniFi.Net.SiteManager.Exceptions;
using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.SiteManager;

/// <summary>
/// Client for Site Manager API.
/// </summary>
public class SiteManagerClient : ISiteManagerClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    private const string Version = "v1";
    private const string EarlyAccess = "ea";

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteManagerClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public SiteManagerClient(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteManagerClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public SiteManagerClient(string apiKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        _httpClientFactory = CreateFactory(new Uri("https://api.ui.com"), apiKey);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SiteManagerClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public SiteManagerClient(Uri host, string apiKey)
    {
        ArgumentNullException.ThrowIfNull(host);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        _httpClientFactory = CreateFactory(host, apiKey);
    }

    /// <inheritdoc/>
    public Task<PagedResponse<Host>> ListHostsAsync(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default)
    {
        const string url = $"{Version}/hosts";

        Dictionary<string, StringValues> queryParams = new()
        {
            ["page"] = pageSize.ToString(),
            ["nextToken"] = nextToken ?? String.Empty
        };

        return GetFromJsonAsync<PagedResponse<Host>>(url + BuildQueryString(queryParams), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<Host>> GetHostAsync(string id, CancellationToken cancellationToken = default)
    {
        string url = $"{Version}/hosts/{id}";

        return GetFromJsonAsync<DataResponse<Host>>(url, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PagedResponse<Site>> ListSitesAsync(int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default)
    {
        const string url = $"{Version}/sites";

        Dictionary<string, StringValues> queryParams = new()
        {
            ["page"] = pageSize.ToString(),
            ["nextToken"] = nextToken ?? String.Empty
        };

        return GetFromJsonAsync<PagedResponse<Site>>(url + BuildQueryString(queryParams), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<PagedResponse<HostWithDevices>> ListDevicesAsync(IEnumerable<string>? hostIds = null, DateTimeOffset? time = null, int? pageSize = null, string? nextToken = null, CancellationToken cancellationToken = default)
    {
        const string url = $"{Version}/devices";

        Dictionary<string, StringValues> queryParams = new()
        {
            ["hostIds[]"] = hostIds != null ? new StringValues([.. hostIds]) : String.Empty,
            ["time"] = time?.ToString("o") ?? String.Empty,
            ["pageSize"] = pageSize?.ToString() ?? String.Empty,
            ["nextToken"] = nextToken ?? String.Empty,
        };

        return GetFromJsonAsync<PagedResponse<HostWithDevices>>(url + BuildQueryString(queryParams), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetricsAsync(MetricInterval type, DateTimeOffset? beginTimestamp = null, DateTimeOffset? endTimestamp = null, CancellationToken cancellationToken = default)
    {
        string url = $"{EarlyAccess}/isp-metrics/{type}";

        Dictionary<string, StringValues> queryParams = new()
        {
            ["beginTimestamp"] = beginTimestamp?.ToString("o") ?? String.Empty,
            ["endTimestamp"] = endTimestamp?.ToString("o") ?? String.Empty
        };

        return GetFromJsonAsync<DataResponse<IReadOnlyList<IspMetric>>>(url + BuildQueryString(queryParams), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<IReadOnlyList<IspMetric>>> GetIspMetricsAsync(MetricInterval type, string? duration = null, CancellationToken cancellationToken = default)
    {
        string url = $"{EarlyAccess}/isp-metrics/{type}";

        Dictionary<string, StringValues> queryParams = new()
        {
            ["duration"] = duration ?? String.Empty,
        };

        return GetFromJsonAsync<DataResponse<IReadOnlyList<IspMetric>>>(url + BuildQueryString(queryParams), cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<IReadOnlyList<IspMetric>>> QueryIspMetricsAsync(MetricInterval type, IEnumerable<IspMetricsQuery> sites, CancellationToken cancellationToken = default)
    {
        string url = $"{EarlyAccess}/isp-metrics/{type}/query";

        return PostJsonAsync<IEnumerable<IspMetricsQuery>, DataResponse<IReadOnlyList<IspMetric>>>(url, sites, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<IReadOnlyList<BasicSDWanConfig>>> ListSDWanConfigsAsync(CancellationToken cancellationToken = default)
    {
        const string url = $"{EarlyAccess}/sd-wan-configs";

        return GetFromJsonAsync<DataResponse<IReadOnlyList<BasicSDWanConfig>>>(url, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<SDWanConfig>> GetSDWanConfigAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string url = $"{EarlyAccess}/sd-wan-configs/{id}";

        return GetFromJsonAsync<DataResponse<SDWanConfig>>(url, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<DataResponse<SDWanConfigStatus>> GetSDWanConfigStatusAsync(Guid id, CancellationToken cancellationToken = default)
    {
        string url = $"{EarlyAccess}/sd-wan-configs/{id}/status";

        return GetFromJsonAsync<DataResponse<SDWanConfigStatus>>(url, cancellationToken);
    }

    private async Task<T> GetFromJsonAsync<T>(string requestUri, CancellationToken cancellationToken = default)
    {
        var client = GetClient();

        try
        {
            var responseMessage = await client.GetAsync(requestUri, cancellationToken) ??
                throw new InvalidOperationException($"Failed to deserialize response from {requestUri}.");

            if (!responseMessage.IsSuccessStatusCode)
            {
                await ProcessErrorResponse(requestUri, responseMessage, cancellationToken);
            }

            var content = await responseMessage.Content.ReadAsStringAsync(cancellationToken);

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

    private async Task<TResponse> PostJsonAsync<TRequest, TResponse>(string requestUri, TRequest body, CancellationToken cancellationToken = default)
    {
        var client = GetClient();

        try
        {
            var responseMessage = await client.PostAsJsonAsync<TRequest>(requestUri, body, cancellationToken) ??
                   throw new InvalidOperationException($"Failed to deserialize response from {requestUri}.");

            if (!responseMessage.IsSuccessStatusCode)
            {
                await ProcessErrorResponse(new HttpRequestMessage(HttpMethod.Post, requestUri), responseMessage, cancellationToken);
            }

            var result = await responseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);

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

    private static Task ProcessErrorResponse(HttpRequestMessage request, HttpResponseMessage response, CancellationToken cancellationToken) =>
        ProcessErrorResponse(request.RequestUri, response, cancellationToken);

    private static Task ProcessErrorResponse(Uri? requestUri, HttpResponseMessage response, CancellationToken cancellationToken) =>
        ProcessErrorResponse(requestUri?.ToString(), response, cancellationToken);

    private static async Task ProcessErrorResponse(string? requestUri, HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            var result = await response.Content.ReadFromJsonAsync<ErrorResponse>(cancellationToken: cancellationToken) ?? throw new InvalidOperationException($"Error from {requestUri ?? "unknown URI"}: {response.ReasonPhrase}");

            throw result.HttpStatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException(result),
                HttpStatusCode.Unauthorized => new UnauthorizedException(result),
                _ => new UniFiSiteManagerException(result)
            };
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Error from {requestUri}: {response.ReasonPhrase}", ex);
        }
    }

    private HttpClient GetClient()
    {
        HttpClient client;

        client = _httpClientFactory.CreateClient("SiteManagerClient");
        if (client.BaseAddress == null)
        {
            throw new InvalidOperationException("Base address is not set for SiteManagerClient.");
        }
        return client;
    }

    private static string BuildQueryString(Dictionary<string, StringValues> queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
        {
            return String.Empty;
        }
        var query = String.Join("&", queryParams.Where(kvp => !String.IsNullOrWhiteSpace(kvp.Key) && !String.IsNullOrWhiteSpace(queryParams[kvp.Key]))
            .SelectMany(kvp => queryParams[kvp.Key].Where(v => !String.IsNullOrWhiteSpace(v)).Select(v => $"{kvp.Key}={Uri.EscapeDataString(v!)}")));
        return "?" + query;
    }

    private static IHttpClientFactory CreateFactory(Uri host, string apiKey)
    {
        // Manually create an IHttpClientFactory for on-demand HttpClient creation
        var services = new ServiceCollection();
        services.AddHttpClient<SiteManagerClient>("SiteManagerClient", (provider, client) =>
        {
            HttpClientConfigurator.ConfigureHttpClient(client, host, apiKey);
        });
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IHttpClientFactory>();
    }
}
