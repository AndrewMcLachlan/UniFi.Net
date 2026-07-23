namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    private async Task DoDevices(CancellationToken cancellationToken)
    {
        Clear();
        WriteHeading("Devices");

        var devices = await client.ListDevicesAsync(cancellationToken: cancellationToken);

        foreach (var host in devices.Data)
        {
            WriteLine($"Host: {host.HostName} ({host.HostId})");
            WriteLine($"  Updated at: {host.UpdatedAt:yyyy-MM-dd HH:mm:ss}");
            foreach (var device in host.Devices)
            {
                WriteLine($"  {device.Name} [{device.ShortName}]");
                WriteLine($"    Id:      {device.Id}");
                WriteLine($"    MAC:     {device.Mac}, IP: {device.Ip}");
                WriteLine($"    Product: {device.ProductLine}, Model: {device.Model}");
                WriteLine($"    Status:  {device.Status}, Version: {device.Version}, Firmware: {device.FirmwareStatus}");
                WriteLine($"    Console: {device.IsConsole}, Managed: {device.IsManaged}");
                WriteLine($"    Started: {device.StartupTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "(unknown)"}, Adopted: {device.AdoptionTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "(unknown)"}");
            }
            WriteLine();
        }

        WriteLine($"{devices.Data.Count} host(s) returned. Next token: {devices.NextToken ?? "(none)"}");

        PressAnyKeyToContinue();
    }
}
