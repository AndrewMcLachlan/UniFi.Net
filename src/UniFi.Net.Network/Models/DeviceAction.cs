using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// Possible actions that can be performed on a device.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum DeviceAction
{
    /// <summary>
    /// Restart the device.
    /// </summary>
    Restart = 0,
}
