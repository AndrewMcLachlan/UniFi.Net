namespace UniFi.Net.Access;

/// <summary>
/// Base class for UniFi Access clients.
/// </summary>
public abstract class ClientBase
{
    private readonly IHttpClientFactory? _httpClientFactory;
    private readonly Uri? _host;
    private readonly string? _apiKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientBase"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    protected ClientBase(IHttpClientFactory httpClientFactory)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientBase"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="host">The base URI for the UniFi API.</param>
    /// <param name="apiKey">The API key for authentication.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    protected ClientBase(Uri host, string apiKey)
    {
        ArgumentNullException.ThrowIfNull(host);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        _host = host;
        _apiKey = apiKey;
    }

    /// <summary>
    /// Gets an <see cref="HttpClient"/> instance configured for UniFi Access API requests.
    /// </summary>
    /// <returns>An instance of <see cref="HttpClient"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the base address is not set.</exception>
    protected HttpClient GetClient()
    {
        HttpClient client;

        if (_httpClientFactory == null)
        {
            client = new HttpClient();
            HttpClientConfigurator.ConfigureHttpClient(client, _host!, _apiKey!);
            return client;
        }

        client = _httpClientFactory.CreateClient("AccessClient");
        if (client.BaseAddress == null)
        {
            throw new InvalidOperationException("Base address is not set.");
        }
        return client;
    }
}
