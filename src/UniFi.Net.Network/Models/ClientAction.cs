using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// Possible actions that can be performed on a client.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum ClientAction
{
    /// <summary>
    /// Action to authorize guest access for a client.
    /// </summary>
    AuthorizeGuestAccess = 0,

    /// <summary>
    /// Action to unauthorize guest access for a client.
    /// </summary>
    UnauthorizeGuestAccess = 1,
}
