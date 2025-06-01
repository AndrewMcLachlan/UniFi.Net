using System.Net;

namespace UniFi.Net.Models;

/// <summary>
/// Represents a client connected to the system, including its identification, connection details, and access
/// information.
/// </summary>
/// <remarks>This record encapsulates information about a connected client, such as its unique identifier, name,
/// connection timestamp,  IP address, access level, type, MAC address, and the identifier of the uplink device it is
/// associated with.</remarks>
/// <param name="Id">The client's ID.</param>
/// <param name="Name">The name of the client.</param>
/// <param name="ConnectedAt">The date and time the client connected.</param>
/// <param name="IpAddress">The IP address of the client.</param>
/// <param name="Access"></param>
/// <param name="Type">The type of client.</param>
/// <param name="MacAddress">The MAC address of the client.</param>
/// <param name="UplinkDeviceId">The ID device that this client is attached to.</param>
public record Client(
    Guid Id,
    string Name,
    DateTimeOffset ConnectedAt,
    string IpAddress,
    ClientAccess Access,
    string Type,
    string MacAddress,
    Guid UplinkDeviceId
);

/// <summary>
/// Represents the access type for a client.
/// </summary>
/// <param name="Type">The type of access granted to the client.</param>
public record ClientAccess(
    string Type
);