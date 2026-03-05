using System.Net.Http.Json;
using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;
using UniFi.Net.Network.Responses;

namespace UniFi.Net.Network;

public partial class NetworkClient
{
    /// <inheritdoc />
    public Task<PagedResponse<Voucher>> ListVouchers(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/hotspot/vouchers";
        string query = BuildPagingQuery(filter, offset, limit);

        return GetFromJsonAsync<PagedResponse<Voucher>>(path + query, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Voucher>> GenerateVouchers(Guid siteId, string name, long authorizedGuestLimit, long timeLimitMinutes, int? count = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/hotspot/vouchers";

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
        string path = $"{PathPrefix}sites/{siteId}/hotspot/vouchers";
        string query = $"?filter={filter}";

        var request = new HttpRequestMessage(HttpMethod.Delete, path + query);

        var result = await SendAsync<DeleteVouchersResponse>(request, cancellationToken);

        return result.VouchersDeleted;
    }

    /// <inheritdoc />
    public Task<Voucher> GetVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/hotspot/vouchers/{voucherId}";
        return GetFromJsonAsync<Voucher>(path, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<long> DeleteVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default)
    {
        string path = $"{PathPrefix}sites/{siteId}/hotspot/vouchers/{voucherId}";

        var request = new HttpRequestMessage(HttpMethod.Delete, path);

        var result = await SendAsync<DeleteVouchersResponse>(request, cancellationToken);

        return result.VouchersDeleted;
    }
}
