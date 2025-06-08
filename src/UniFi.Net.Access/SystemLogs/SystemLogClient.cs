using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.SystemLogs;

/// <inheritdoc />
public class SystemLogClient : ClientBase, ISystemLogClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SystemLogClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public SystemLogClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemLogClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public SystemLogClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc />
    public async Task<List<SystemLogEntry>> FetchSystemLogsAsync(string topic, long? since = null, long? until = null, string? actorId = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = new
        {
            topic,
            since,
            until,
            actor_id = actorId
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/system/logs", content, cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<SystemLogEntry>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task<byte[]> ExportSystemLogsAsync(string topic, long since, long until, string timeZone, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = new
        {
            topic,
            since,
            until,
            timezone = timeZone
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/system/logs/export", content, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync(cancellationToken);
    }
}
