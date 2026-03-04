using System.Net.Http.Json;
using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<Client>> ListClients(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/clients";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Client>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public Task<Client> GetClient(Guid siteId, Guid clientId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/clients/{clientId}";

        return GetFromJsonAsync<Client>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AuthorizeClientGuestAccessResponse> AuthorizeClientGuestAccess(Guid siteId, Guid clientId, long? timeLimitMinutes = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/clients/{clientId}/actions";
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
    public Task UnauthorizeClientGuestAccess(Guid siteId, Guid clientId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/clients/{clientId}/actions";
        var requestBody = new
        {
            action = ClientAction.UnauthorizeGuestAccess
        };

        return PostAsJsonAsync(path, requestBody, cancellationToken);
    }
}
