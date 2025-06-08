using UniFi.Net.Access.Devices;

namespace UniFi.Net.Access;

public interface IDeviceClient
{
    // Device Management
    /// <summary>
    /// Fetches all devices.
    /// </summary>
    /// <returns>Task representing the asynchronous operation, returning a list of devices.</returns>
    Task<List<DeviceSummary>> FetchAllDevicesAsync();
}
