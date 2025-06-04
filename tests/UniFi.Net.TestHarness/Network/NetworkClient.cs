using static System.Console;

namespace UniFi.Net.TestHarness.Network;

public partial class NetworkClient(INetworkClient uniFiClient)
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var sites = await uniFiClient.ListSites(cancellationToken: cancellationToken);

        SelectedSite = sites.Data.Count > 0 ? sites.Data[0] : null;

        (int Primary, int? Secondary) action = (0, null);
        while (action.Primary != 5 && !cancellationToken.IsCancellationRequested)
        {
            action = PrintMenu();

            await DoAction(action, cancellationToken);
        }
    }

    private async Task DoAction((int Primary, int? Secondary) action, CancellationToken cancellationToken)
    {
        switch (action.Primary)
        {
            case 1:
                await DoInfo(cancellationToken);
                break;
            case 2:
                await DoSites(action.Secondary!.Value, cancellationToken);
                break;
            case 3:
                await DoDevices(action.Secondary!.Value, cancellationToken);
                break;
            case 4:
                await DoClients(action.Secondary!.Value, cancellationToken);
                break;
            case 5:
                break;
            default:
                WriteLine("Invalid option, please try again.");
                break;
        }
    }

    private async Task DoInfo(CancellationToken cancellationToken)
    {
        var info = await uniFiClient.GetApplicationInfo(cancellationToken);
        WriteLine(info);
        WriteLine("Press any key to return to the main menu...");
        ReadKey();
    }

    private (int, int?) PrintMenu()
    {
        Clear();
        WriteLine("Unifi Network Client Test Harness");
        WriteLine("-------------------------");
        if (SelectedSite is not null)
        {
            WriteLine($"Current site is ${SelectedSite.Name}");
            WriteLine("-------------------------");
        }
        if (SelectedDevice is not null)
        {
            WriteLine($"Current device is ${SelectedDevice.Name}");
            WriteLine("-------------------------");
        }
        if (SelectedClient is not null)
        {
            WriteLine($"Current client is ${SelectedClient.Name}");
            WriteLine("-------------------------");
        }
        WriteLine("1. Info");
        WriteLine("2. Sites");
        WriteLine("3. Devices");
        WriteLine("4. Clients");
        WriteLine("5. Main menu");
        Write("Select an option: ");

        var input = ReadKey();
        WriteLine();

        return input.KeyChar switch
        {
            '1' => (1, null),
            '2' => (2, PrintSitesMenu()), // List Devices
            '3' => (3, PrintDevicesMenu()), // Get Device Details
            '4' => (4, PrintClientsMenu()), // List Clients
            '5' => (5, null), // Exit
            _ => (-1, null)
        };
    }

    private bool SelectedSiteCheck()
    {
        if (SelectedSite == null)
        {
            WriteLine("No site selected. Please select a site first.");
            return false;
        }
        return true;
    }
}
