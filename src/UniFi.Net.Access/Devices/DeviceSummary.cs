namespace UniFi.Net.Access.Devices;

/// <summary>
/// Summary information about a device in the UniFi Access system.
/// </summary>
/// <param name="Id">The unique identifier for the device.</param>
/// <param name="Name">The name of the device.</param>
/// <param name="Type">The type of the device.</param>
/// <param name="Alias">An optional alias for the device.</param>
public record DeviceSummary(string Id, string Name, string Type, string Alias);