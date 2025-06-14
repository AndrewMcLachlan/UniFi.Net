using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// Represents the available methods of authorization.
/// </summary>
/// <remarks>This enumeration defines the different ways an entity can be authorized, such as using a voucher,  an
/// API, or other unspecified methods. The values are serialized using snake_case formatting  when converted to
/// JSON.</remarks>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum AuthorizationMethod
{
    /// <summary>
    /// Voucher-based authorization method.
    /// </summary>
    Voucher = 0,

    /// <summary>
    /// Authentication via API.
    /// </summary>
    Api = 1,

    /// <summary>
    /// Other authorization methods not specified by the API.
    /// </summary>
    Other = 2,
}
