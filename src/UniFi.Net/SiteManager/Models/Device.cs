namespace UniFi.Net.SiteManager.Models;

/// <summary>
/// Represents a UniFi device with detailed information.
/// </summary>
/// <param name="Id">Unique identifier of the device.</param>
/// <param name="Mac">MAC address of the device.</param>
/// <param name="Name">User-defined name of the device.</param>
/// <param name="Model">Model name of the device.</param>
/// <param name="Shortname">Short identifier of the device model (e.g., UDMPROSE).</param>
/// <param name="Ip">IP address of the device.</param>
/// <param name="ProductLine">Product line of the device (network, protect, etc.).</param>
/// <param name="Status">Current connection status of the device (online, offline, etc.).</param>
/// <param name="Version">Current firmware version of the device.</param>
/// <param name="FirmwareStatus">Status of device firmware (upToDate, updateAvailable, etc.).</param>
/// <param name="UpdateAvailable">Version of firmware update available for the device, if any.</param>
/// <param name="IsConsole">Indicates if the device is a console device.</param>
/// <param name="IsManaged">Indicates if the device is managed by the controller.</param>
/// <param name="StartupTime">Time when the device was last started in RFC3339 format.</param>
/// <param name="AdoptionTime">Time when the device was adopted in RFC3339 format.</param>
/// <param name="Note">User-defined notes for the device.</param>
/// <param name="Uidb">UI-specific metadata including images and identifiers.</param>
public record Device(
    string Id,
    string Mac,
    string Name,
    string Model,
    string Shortname,
    string Ip,
    string ProductLine,
    string Status,
    string Version,
    string FirmwareStatus,
    string? UpdateAvailable,
    bool IsConsole,
    bool IsManaged,
    string StartupTime,
    string? AdoptionTime,
    string? Note,
    Uidb Uidb
);

/// <summary>
/// Represents UI-specific metadata for a device.
/// </summary>
/// <param name="Guid">Unique identifier for the UI metadata.</param>
/// <param name="Id">Identifier for the UI metadata.</param>
/// <param name="Images">Images associated with the device.</param>
public record Uidb(
    Guid Guid,
    string Id,
    IDictionary<string, string> Images
);
