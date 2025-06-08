using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.Visitors;

/// <inheritdoc />
public class VisitorClient : ClientBase, IVisitorClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VisitorClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public VisitorClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="VisitorClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public VisitorClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc />
    public async Task RegisterVisitorAsync(string firstName, string lastName, string? email = null, string? phone = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = new
        {
            first_name = firstName,
            last_name = lastName,
            email,
            mobile_phone = phone
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/visitors", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }


    /// <inheritdoc />
    public async Task<VisitorDetails> FetchVisitorAsync(string visitorId, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/visitors/{visitorId}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<VisitorDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize visitor details.");
    }

    /// <inheritdoc />
    public async Task<List<VisitorSummary>> FetchAllVisitorsAsync(int? pageNum = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var query = new List<string>();
        if (pageNum.HasValue) query.Add($"page_num={pageNum.Value}");
        if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");

        var queryString = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
        var response = await httpClient.GetAsync($"/api/v1/developer/visitors{queryString}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<VisitorSummary>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task DeleteVisitorAsync(string visitorId, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.DeleteAsync($"/api/v1/developer/visitors/{visitorId}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
