namespace UniFi.Net.Access.Spaces;

/// <summary>
/// Represents a summary of a door group, including its unique identifier and name.
/// </summary>
/// <param name="Id">The unique identifier of the door group.</param>
/// <param name="Name">The name of the door group.</param>
public record DoorGroupSummary(string Id, string Name);

/// <summary>
/// Represents the details of a door group, including its identifier, name, and associated resource IDs.
/// </summary>
/// <param name="Id">The unique identifier of the door group. This value cannot be null or empty.</param>
/// <param name="Name">The name of the door group. This value cannot be null or empty.</param>
/// <param name="ResourceIds">A list of resource IDs associated with the door group. This list cannot be null but may be empty.</param>
public record DoorGroupDetails(string Id, string Name, List<string> ResourceIds);

/// <summary>
/// Represents a summary of a door, including its identifier, name, and current status.
/// </summary>
/// <param name="Id">The unique identifier of the door.</param>
/// <param name="Name">The name or label of the door.</param>
/// <param name="Status">The current status of the door, such as "Open" or "Closed".</param>
public record DoorSummary(string Id, string Name, string Status);

/// <summary>
/// Represents the details of a door, including its identifier, name, status, and type.
/// </summary>
/// <remarks>This record is typically used to encapsulate information about a door in systems such as access
/// control, inventory, or monitoring.</remarks>
/// <param name="Id">The unique identifier of the door.</param>
/// <param name="Name">The name or label of the door.</param>
/// <param name="Status">The current status of the door, such as "Open" or "Closed".</param>
/// <param name="Type">The type of door.</param>
public record DoorDetails(string Id, string Name, string Status, string Type);