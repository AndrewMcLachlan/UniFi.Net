namespace UniFi.Net.Access.Devices;

/// <summary>
/// Device management client  for UniFi Access.
/// </summary>
public interface IDeviceClient
{
    // Device Management
    /// <summary>
    /// Fetches all devices.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
    /// <returns>Task representing the asynchronous operation, returning a list of devices.</returns>
    Task<List<DeviceSummary>> FetchAllDevicesAsync(CancellationToken cancellationToken = default);
}
