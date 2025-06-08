namespace UniFi.Net.Access.Space;
// Models for Space Management
public record DoorGroupSummary(string Id, string Name);
public record DoorGroupDetails(string Id, string Name, List<string> ResourceIds);
public record DoorSummary(string Id, string Name, string Status);
public record DoorDetails(string Id, string Name, string Status, string Type);