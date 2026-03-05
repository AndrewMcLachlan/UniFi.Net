using Xunit.Abstractions;

namespace UniFi.Net.Network.IntegrationTests;

[Trait("Category", "Integration")]
public class DeviceTests(NetworkClientFixture fixture, ITestOutputHelper output) : IntegrationTestBase(fixture, output)
{
    /// <summary>
    /// Given a site on the controller,
    /// When ListDevices is called with no filters,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListDevices_ReturnsPagedResponse()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListDevices(SiteId);

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} device(s).");
    }

    /// <summary>
    /// Given a site on the controller,
    /// When ListDevices is called with a limit of 1,
    /// Then the response contains at most 1 device.
    /// </summary>
    [Fact]
    public async Task ListDevices_WithLimit_RespectsLimit()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListDevices(SiteId, limit: 1);

        Assert.NotNull(result);
        Assert.True(result.Data.Count <= 1);
    }

    /// <summary>
    /// Given a site with at least one device,
    /// When ListDevices is called,
    /// Then each device has a non-empty ID, MAC address, model, state, features, and interfaces.
    /// </summary>
    [Fact]
    public async Task ListDevices_EachDevice_HasRequiredFields()
    {
        if (SkipIfNoSite()) return;

        var result = await Client.ListDevices(SiteId, limit: 10);

        foreach (var device in result.Data)
        {
            Assert.NotEqual(Guid.Empty, device.Id);
            Assert.False(String.IsNullOrWhiteSpace(device.MacAddress));
            Assert.False(String.IsNullOrWhiteSpace(device.Model));
            Assert.False(String.IsNullOrWhiteSpace(device.State));
            Assert.NotNull(device.Features);
            Assert.NotNull(device.Interfaces);
            Output.WriteLine($"  Device: {device.Name} ({device.Model}) - {device.State}");
        }
    }

    /// <summary>
    /// Given a site with at least one device,
    /// When GetDevice is called with the first device's ID,
    /// Then the full device details are returned including firmware version.
    /// </summary>
    [Fact]
    public async Task GetDevice_WhenDevicesExist_ReturnsFullDevice()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListDevices(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No devices found on site."); return; }

        var device = await Client.GetDevice(SiteId, list.Data[0].Id);

        Assert.NotNull(device);
        Assert.Equal(list.Data[0].Id, device.Id);
        Assert.False(String.IsNullOrWhiteSpace(device.FirmwareVersion));
        Assert.NotNull(device.Features);
        Assert.NotNull(device.Interfaces);
        Output.WriteLine($"  Device: {device.Name}, FW: {device.FirmwareVersion}, Uptime adopted: {device.AdoptedAt}");
    }

    /// <summary>
    /// Given a site with at least one device,
    /// When GetDeviceStatistics is called with the first device's ID,
    /// Then the response contains non-negative uptime, CPU, and memory utilization values.
    /// </summary>
    [Fact]
    public async Task GetDeviceStatistics_WhenDevicesExist_ReturnsStatistics()
    {
        if (SkipIfNoSite()) return;

        var list = await Client.ListDevices(SiteId, limit: 1);
        if (list.Data.Count == 0) { SkipBecause("No devices found on site."); return; }

        var stats = await Client.GetDeviceStatistics(SiteId, list.Data[0].Id);

        Assert.NotNull(stats);
        Assert.True(stats.UptimeSec >= 0);
        Assert.True(stats.CpuUtilizationPct >= 0);
        Assert.True(stats.MemoryUtilizationPct >= 0);
        Output.WriteLine($"  Uptime: {stats.UptimeSec}s, CPU: {stats.CpuUtilizationPct}%, Mem: {stats.MemoryUtilizationPct}%");
    }

    /// <summary>
    /// Given a configured Network client,
    /// When ListPendingDevices is called,
    /// Then a paged response is returned with a non-negative total count.
    /// </summary>
    [Fact]
    public async Task ListPendingDevices_ReturnsPagedResponse()
    {
        if (SkipIfNotConfigured()) return;

        var result = await Client.ListPendingDevices();

        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.TotalCount >= 0);
        Output.WriteLine($"Found {result.TotalCount} pending device(s).");
    }
}
