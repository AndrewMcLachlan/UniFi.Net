using UniFi.Net.SiteManager;
using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient(ISiteManagerClient client)
{
    private Host? SelectedHost { get; set; }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var hosts = await client.ListHostsAsync(cancellationToken: cancellationToken);

        SelectedHost = hosts.Data.Count > 0 ? hosts.Data[0] : null;

        (int Primary, int? Secondary) action = (0, null);
        while (action.Primary != 5 && !cancellationToken.IsCancellationRequested)
        {
            action = PrintMenu();

            await DoAction(action, cancellationToken);
        }
    }

    private Task DoAction((int Primary, int? Secondary) action, CancellationToken cancellationToken) =>
        action.Primary switch
        {
            1 => DoHosts(action.Secondary, cancellationToken),
            2 => DoSites(cancellationToken),
            3 => DoDevices(cancellationToken),
            _ => Task.CompletedTask,
        };

    private (int Primary, int? Secondary) PrintMenu()
    {
        Clear();
        WriteLine("Unifi Site Manager Client Test Harness");
        WriteLine("-------------------------");
        WriteLine("1. Hosts");
        WriteLine("2. Sites");
        WriteLine("3. Devices");
        WriteLine("4. ISP Metrics");
        WriteLine("5. SD-WAN Configs");
        WriteLine("6. Main menu");
        Write("Select an option: ");

        var input = ReadKey();
        WriteLine();

        return input.KeyChar switch
        {
            '1' => (1, PrintHostsMenu()),
            '2' => (2, null), // List Devices
            '3' => (3, null), // Get Device Details
            '4' => (4, PrintIspMetricsMenu()), // List Clients
            '5' => (5, PrintSDWanConfigsMenu()), // Exit
            '6' => (6, null), // Exit
            _ => (-1, null)
        };
    }
}

