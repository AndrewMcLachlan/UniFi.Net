namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    public async Task DoDevices(CancellationToken cancellationToken)
    {
        var devices = await client.ListDevicesAsync( cancellationToken: cancellationToken);

        foreach (var host in devices.Data)
        {
            WriteLine($"Host ID: {host.HostId}");
            foreach (var device in host.Devices)
            {
                WriteLine($"Device ID: {device.Ip}, Device Name: {device.Name}");
                WriteLine(device);
            }
        }

        PressAnyKeyToContinue();
    }
}
