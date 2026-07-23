using UniFi.Net.SiteManager;
using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient(ISiteManagerClient client)
{
    private Host? SelectedHost { get; set; }

    private BasicSDWanConfig? SelectedSDWanConfig { get; set; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var hosts = await client.ListHostsAsync(cancellationToken: cancellationToken);
            SelectedHost = hosts.Data.Count > 0 ? hosts.Data[0] : null;
        }
        catch (Exception ex)
        {
            WriteError($"Could not list hosts: {ex.Message}");
            PressAnyKeyToContinue();
        }

        while (!cancellationToken.IsCancellationRequested)
        {
            var action = PromptOption(Title(), ["Hosts", "Sites", "Devices", "ISP Metrics", "SD-WAN Configs"], "Main menu");

            if (action == 0)
            {
                return;
            }

            try
            {
                await (action switch
                {
                    1 => DoHosts(cancellationToken),
                    2 => DoSites(cancellationToken),
                    3 => DoDevices(cancellationToken),
                    4 => DoIspMetrics(cancellationToken),
                    5 => DoSDWanConfigs(cancellationToken),
                    _ => Task.CompletedTask,
                });
            }
            catch (Exception ex)
            {
                WriteError($"Error: {ex.Message}");
                PressAnyKeyToContinue();
            }
        }
    }

    private string Title()
    {
        var title = "UniFi Site Manager Test Harness";
        if (SelectedHost is not null)
        {
            title += $"\nCurrent host is {SelectedHost.IpAddress} ({SelectedHost.Type})";
        }
        if (SelectedSDWanConfig is not null)
        {
            title += $"\nCurrent SD-WAN config is {SelectedSDWanConfig.Name}";
        }
        return title;
    }

    private bool SelectedHostCheck()
    {
        if (SelectedHost == null)
        {
            WriteError("No host selected. Please select a host first (Hosts > List hosts).");
            PressAnyKeyToContinue();
            return false;
        }
        return true;
    }
}
