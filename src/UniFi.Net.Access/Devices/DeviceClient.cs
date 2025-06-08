using System.Text.Json;

namespace UniFi.Net.Access.Devices;

/// <inheritdoc/>
public class DeviceClient : ClientBase, IDeviceClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public DeviceClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeviceClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public DeviceClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    ///<inheritdoc />
    public async Task<List<DeviceSummary>> FetchAllDevicesAsync(CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.GetAsync("/api/v1/developer/devices", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<DeviceSummary>>(json) ?? [];
    }
}
