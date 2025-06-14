using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// Possible actions that can be performed on a device.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum PortAction
{
    /// <summary>
    /// Power cycle the port.
    /// </summary>
    PowerCycle = 0,
}
