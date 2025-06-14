using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace UniFi.Net.Access.Users;

/// <inheritdoc/>
public class UserClient : ClientBase, IUserClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserClient"/> class using an <see cref="IHttpClientFactory"/>.
    /// </summary>
    /// <param name="httpClientFactory">The  <see cref="IHttpClientFactory"/> </param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="httpClientFactory"/>is <see langword="null"/>.</exception>
    public UserClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserClient"/> class using a specific host and API key.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="apiKey"></param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="host"/> or <paramref name="apiKey"/> is <see langword="null"/>.</exception>
    public UserClient(Uri host, string apiKey) : base(host, apiKey)
    {
    }

    /// <inheritdoc />
    public async Task RegisterUserAsync(string firstName, string lastName, string? email = null, string? employeeNumber = null, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var payload = new
        {
            first_name = firstName,
            last_name = lastName,
            user_email = email,
            employee_number = employeeNumber
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/api/v1/developer/users", content, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <inheritdoc />
    public async Task<UserDetails> FetchUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.GetAsync($"/api/v1/developer/users/{userId}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<UserDetails>(json) ?? throw new InvalidOperationException("Failed to deserialize user details.");
    }

    /// <inheritdoc />
    public async Task<List<UserSummary>> FetchAllUsersAsync(int? pageNum = null, int? pageSize = null, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var query = new List<string>();
        if (pageNum.HasValue) query.Add($"page_num={pageNum.Value}");
        if (pageSize.HasValue) query.Add($"page_size={pageSize.Value}");

        var queryString = query.Count > 0 ? "?" + string.Join("&", query) : string.Empty;
        var response = await httpClient.GetAsync($"/api/v1/developer/users{queryString}", cancellationToken);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<UserSummary>>(json) ?? [];
    }

    /// <inheritdoc />
    public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var httpClient = GetClient();

        var response = await httpClient.DeleteAsync($"/api/v1/developer/users/{userId}", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}
