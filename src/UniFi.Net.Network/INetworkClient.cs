using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.Network;

/// <summary>
/// The UniFi Client.
/// </summary>
public interface INetworkClient
{
    /// <summary>
    /// Gets the application information.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Application information.</returns>
    Task<ApplicationInfo> GetApplicationInfo(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of sites.
    /// </summary>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of sites to offset.</param>
    /// <param name="limit">The maximum number of sites to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of sites.</returns>
    Task<PagedResponse<Site>> ListSites(IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of devices.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of devices to offset.</param>
    /// <param name="limit">The maximum number of devices to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of devices.</returns>
    Task<PagedResponse<DeviceSummary>> ListDevices(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific device by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="deviceId">The device ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Device details.</returns>
    Task<Device> GetDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action on a port associated with a device at a given site.
    /// </summary>
    /// <remarks>This method performs an action on a specific port of a device. Ensure that the provided
    /// identifiers for the site and device are valid and that the port index corresponds to an existing port on the
    /// device.</remarks>
    /// <param name="portIdx">The index of the port on which the action will be performed. Must be a valid port index.</param>
    /// <param name="siteId">The unique identifier of the site where the device is located.</param>
    /// <param name="deviceId">The unique identifier of the device associated with the port.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task PowerCyclePort(int portIdx, Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action on a device within a site.
    /// </summary>
    /// <remarks>Use this method to perform actions such as restarting, updating, or configuring a device.
    /// Ensure that the <paramref name="siteId"/> and <paramref name="deviceId"/> are valid and correspond to an
    /// existing site and device.</remarks>
    /// <param name="siteId">The unique identifier of the site containing the device.</param>
    /// <param name="deviceId">The unique identifier of the device on which the action will be performed.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task RestartDevice(Guid siteId, Guid deviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a list of clients.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="filter">A filter expression.</param>
    /// <param name="offset">The number of clients to offset.</param>
    /// <param name="limit">The maximum number of clients to return (max 200).</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A paged response of clients.</returns>
    Task<PagedResponse<Client>> ListClients(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific client by its ID.
    /// </summary>
    /// <param name="siteId">The site ID.</param>
    /// <param name="clientId">The client ID.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>Client details.</returns>
    Task<Client> GetClient(Guid siteId, Guid clientId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a specified action on a client within a site, with optional limits on time, data usage, and network
    /// rates.
    /// </summary>
    /// <remarks>Use this method to perform actions such as managing client connectivity or applying
    /// restrictions. Ensure that the provided limits, if specified, are appropriate for the intended action.</remarks>
    /// <param name="siteId">The unique identifier of the site where the client resides.</param>
    /// <param name="clientId">The unique identifier of the client on which the action will be executed.</param>
    /// <param name="timeLimitMinutes">An optional time limit, in minutes, for the action. If null, no time limit is applied.</param>
    /// <param name="dataUsageLimitMBytes">An optional data usage limit, in megabytes, for the action. If null, no data usage limit is applied.</param>
    /// <param name="rxRateLimitKbps">An optional receive rate limit, in kilobits per second, for the client. If null, no receive rate limit is
    /// applied.</param>
    /// <param name="txRateLimitKbps">An optional transmit rate limit, in kilobits per second, for the client. If null, no transmit rate limit is
    /// applied.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see
    /// cref="AuthorizeClientGuestAccessResponse"/> object with details about the outcome of the action.</returns>
    Task<AuthorizeClientGuestAccessResponse> AuthorizeClientGuestAccess(Guid siteId, Guid clientId, long? timeLimitMinutes = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a paginated list of vouchers for the specified site.
    /// </summary>
    /// <remarks>This method supports optional filtering, pagination, and cancellation. Use the <paramref
    /// name="filter"/> parameter to apply specific criteria to the query, and the <paramref name="offset"/> and
    /// <paramref name="limit"/> parameters to control the pagination of results.</remarks>
    /// <param name="siteId">The unique identifier of the site for which vouchers are being retrieved.</param>
    /// <param name="filter">Optional filter criteria to narrow down the results. If <see langword="null"/>, no filtering is applied.</param>
    /// <param name="offset">The zero-based index of the first voucher to retrieve. If <see langword="null"/>, defaults to the beginning of
    /// the list.</param>
    /// <param name="limit">The maximum number of vouchers to retrieve. If <see langword="null"/>, defaults to the system-defined page size.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see
    /// cref="PagedResponse{Voucher}"/> object, which includes the list of vouchers and pagination metadata.</returns>
    Task<PagedResponse<Voucher>> ListVouchers(Guid siteId, IFilter? filter = null, int? offset = null, int? limit = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Generates a collection of vouchers for a specified site with the given configuration parameters.
    /// </summary>
    /// <remarks>This method creates vouchers based on the provided limits and optional parameters. The
    /// generated vouchers can be used to grant access to guests within the specified constraints.  If the <paramref
    /// name="count"/> parameter is not provided, the method generates a default number of vouchers. Optional parameters
    /// such as data usage limits and rate limits allow fine-grained control over voucher restrictions.</remarks>
    /// <param name="siteId">The unique identifier of the site for which the vouchers are being generated.</param>
    /// <param name="name">The name or label associated with the generated vouchers.</param>
    /// <param name="authorizedGuestLimit">The maximum number of guests authorized to use the vouchers.</param>
    /// <param name="timeLimitMinutes">The time limit, in minutes, for which the vouchers are valid.</param>
    /// <param name="count">The number of vouchers to generate. If null, a default number of vouchers will be created.</param>
    /// <param name="dataUsageLimitMBytes">The maximum data usage limit, in megabytes, for each voucher. If null, no data usage limit is applied.</param>
    /// <param name="rxRateLimitKbps">The maximum download rate limit, in kilobits per second, for each voucher. If null, no download rate limit is
    /// applied.</param>
    /// <param name="txRateLimitKbps">The maximum upload rate limit, in kilobits per second, for each voucher. If null, no upload rate limit is
    /// applied.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see
    /// cref="Voucher"/> objects representing the generated vouchers.</returns>
    Task<IEnumerable<Voucher>> GenerateVouchers(Guid siteId, string name, long authorizedGuestLimit, long timeLimitMinutes, int? count = null, long? dataUsageLimitMBytes = null, long? rxRateLimitKbps = null, long? txRateLimitKbps = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes vouchers associated with the specified site that match the given filter criteria.
    /// </summary>
    /// <remarks>This method removes vouchers from the system based on the provided filter. The operation is
    /// asynchronous and supports cancellation.</remarks>
    /// <param name="siteId">The unique identifier of the site whose vouchers are to be deleted.</param>
    /// <param name="filter">The filter criteria used to determine which vouchers to delete. Must not be null.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see langword="default"/> if not provided.</param>
    /// <returns>The total number of vouchers deleted.</returns>
    Task<long> DeleteVouchers(Guid siteId, IFilter filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a voucher associated with the specified site and voucher identifiers.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site to which the voucher belongs.</param>
    /// <param name="voucherId">The unique identifier of the voucher to retrieve.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see langword="default"/> if not provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Voucher"/> object
    /// corresponding to the specified identifiers.</returns>
    Task<Voucher> GetVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a specific voucher associated with the specified site.
    /// </summary>
    /// <param name="siteId">The unique identifier of the site whose vouchers are to be deleted.</param>
    /// <param name="voucherId">The unique identifier of the voucher to be deleted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see langword="default"/> if not provided.</param>
    /// <returns>The total number of vouchers deleted.</returns>
    Task<long> DeleteVoucher(Guid siteId, Guid voucherId, CancellationToken cancellationToken = default);
}
