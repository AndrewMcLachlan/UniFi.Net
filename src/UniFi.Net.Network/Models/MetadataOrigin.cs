using System.Text.Json.Serialization;
using UniFi.Net.Serialization;

namespace UniFi.Net.Network.Models;

/// <summary>
/// The origin of a resource's metadata.
/// </summary>
[JsonConverter(typeof(SnakeCaseEnumConverter))]
public enum MetadataOrigin
{
    /// <summary>
    /// The resource was created by the user.
    /// </summary>
    UserDefined = 0,

    /// <summary>
    /// The resource was created by the system.
    /// </summary>
    SystemDefined = 1,

    /// <summary>
    /// The resource was created through orchestration.
    /// </summary>
    Orchestrated = 2,

    /// <summary>
    /// The resource is a built-in system resource.
    /// </summary>
    BuiltIn = 3,

    /// <summary>
    /// The resource was derived from another resource.
    /// </summary>
    Derived = 4,
}
