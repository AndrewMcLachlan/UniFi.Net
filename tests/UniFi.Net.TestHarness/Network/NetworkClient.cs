using UniFi.Net.Network;
using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;

public partial class NetworkClient(INetworkClient uniFiClient)
{
    private const byte NetworkExitOption = 9;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var sites = await uniFiClient.ListSites(cancellationToken: cancellationToken);

        SelectedSite = sites.Data.Count > 0 ? sites.Data[0] : null;

        if (SelectedSite != null)
        {
            var devices = await uniFiClient.ListDevices(SelectedSite.Id, null, null, 200, cancellationToken);
            SelectedDevice = devices.Data.Count > 0 ? devices.Data[0] : null;
        }

        (int Primary, object? Action) action = (0, null);
        while (action.Primary != NetworkExitOption && !cancellationToken.IsCancellationRequested)
        {
            action = PrintMenu();

            await DoAction(action, cancellationToken);
        }
    }

    private async Task DoAction((int Primary, object? Action) action, CancellationToken cancellationToken)
    {
        switch (action.Primary)
        {
            case 1:
                await DoInfo(cancellationToken);
                break;
            case 2:
                await DoSites((int)action.Action!, cancellationToken);
                break;
            case 3:
                await DoDevices((int)action.Action!, cancellationToken);
                break;
            case 4:
                await DoClients((int)action.Action!, cancellationToken);
                break;
            case 5:
                await DoPortActions(((PortAction?, int?))action.Action!, cancellationToken);
                break;
            case 6:
                await DoDeviceActions((DeviceAction?)action.Action, cancellationToken);
                break;
            case 7:
                await DoClientActions((ClientAction?)action.Action, cancellationToken);
                break;
            case 8:
                await DoVouchers((int)action.Action!, cancellationToken);
                break;
            case NetworkExitOption:
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

    private (int, object?) PrintMenu()
    {
        Clear();
        WriteLine("Unifi Network Client Test Harness");
        WriteLine("-------------------------");
        if (SelectedSite is not null)
        {
            WriteLine($"Current site is {SelectedSite.Name}");
            WriteLine("-------------------------");
        }
        if (SelectedDevice is not null)
        {
            WriteLine($"Current device is {SelectedDevice.Name}");
            WriteLine("-------------------------");
        }
        if (SelectedClient is not null)
        {
            WriteLine($"Current client is {SelectedClient.Name}");
            WriteLine("-------------------------");
        }
        WriteLine("1. Info");
        WriteLine("2. Sites");
        WriteLine("3. Devices");
        WriteLine("4. Clients");
        WriteLine("5. Port Actions");
        WriteLine("6. Device Actions");
        WriteLine("7. Client Actions");
        WriteLine("8. Vouchers");
        WriteLine($"{NetworkExitOption}. Main menu");
        Write("Select an option: ");

        var input = ReadKey();
        WriteLine();

        return input.KeyChar switch
        {
            '1' => (1, null),
            '2' => (2, PrintSitesMenu()), // List Devices
            '3' => (3, PrintDevicesMenu()), // Get Device Details
            '4' => (4, PrintClientsMenu()), // List Clients
            '5' => (5, PrintPortActionsMenu()), // Port Actions
            '6' => (6, PrintDeviceActionsMenu()), // Device Actions
            '7' => (7, PrintClientActionsMenu()),
            '8' => (8, PrintVouchersMenu()),
            (char)NetworkExitOption => (NetworkExitOption, null), // Exit
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
