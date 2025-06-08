using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.Credentials;

/// <inheritdoc/>
public class CredentialClient : ClientBase, ICredentialClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CredentialClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public CredentialClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CredentialClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public CredentialClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc/>
    public async Task<string> GeneratePinCodeAsync(CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.PostAsync("/api/v1/developer/credentials/pin_codes", null, cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<string>(json) ?? throw new InvalidOperationException("Failed to deserialize PIN code.");
    }

    /// <inheritdoc/>
    public async Task EnrolNfcCardAsync(string deviceId, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var payload = new { device_id = deviceId };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/credentials/nfc_cards/sessions", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc/>
    public async Task<NfcCardDetails> FetchNfcCardAsync(string cardToken, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/credentials/nfc_cards/{cardToken}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<NfcCardDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize NFC card details.");
    }

    /// <inheritdoc/>
    public async Task<List<NfcCardSummary>> FetchAllNfcCardsAsync(int? pageNum = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var query = new List<string>();
        if (pageNum.HasValue) query.Add($"page_num={pageNum.Value}");
        if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");

        var queryString = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
        var response = await httpClient.GetAsync($"/api/v1/developer/credentials/nfc_cards{queryString}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<NfcCardSummary>>(json) ?? [];
    }

    /// <inheritdoc/>
    public async Task DeleteNfcCardAsync(string cardToken, CancellationToken cancellationToken = default)
    {
        using var httpClient = GetClient();

        var response = await httpClient.DeleteAsync($"/api/v1/developer/credentials/nfc_cards/{cardToken}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
