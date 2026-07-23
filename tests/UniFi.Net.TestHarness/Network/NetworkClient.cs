using UniFi.Net.Network;
using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;

public partial class NetworkClient(INetworkClient uniFiClient)
{
    private const byte NetworkExitOption = 0;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            var sites = await uniFiClient.ListSites(cancellationToken: cancellationToken);

            SelectedSite = sites.Data.Count > 0 ? sites.Data[0] : null;

            if (SelectedSite != null)
            {
                var devices = await uniFiClient.ListDevices(SelectedSite.Id, null, null, 200, cancellationToken);
                SelectedDevice = devices.Data.Count > 0 ? devices.Data[0] : null;
            }
        }
        catch (Exception ex)
        {
            WriteError($"Could not list sites/devices: {ex.Message}");
            PressAnyKeyToContinue();
        }

        (int Primary, object? Action) action = (-1, null);
        while (action.Primary != NetworkExitOption && !cancellationToken.IsCancellationRequested)
        {
            action = PrintMenu();

            try
            {
                await DoAction(action, cancellationToken);
            }
            catch (Exception ex)
            {
                WriteError($"Error: {ex.Message}");
                PressAnyKeyToContinue();
            }
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
            case 9:
                await DoReads(cancellationToken);
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
        var title = "Unifi Network Client Test Harness";
        if (SelectedSite is not null)
        {
            title += $"\nCurrent site is {SelectedSite.Name}";
        }
        if (SelectedDevice is not null)
        {
            title += $"\nCurrent device is {SelectedDevice.Name}";
        }
        if (SelectedClient is not null)
        {
            title += $"\nCurrent client is {SelectedClient.Name}";
        }

        var option = PromptOption(
            title,
            [
                "Info",
                "Sites",
                "Devices",
                "Clients",
                "Port Actions",
                "Device Actions",
                "Client Actions",
                "Vouchers",
                "All read endpoints",
            ],
            "Main menu");

        return option switch
        {
            1 => (1, null),
            2 => (2, PrintSitesMenu()),
            3 => (3, PrintDevicesMenu()),
            4 => (4, PrintClientsMenu()),
            5 => (5, PrintPortActionsMenu()),
            6 => (6, PrintDeviceActionsMenu()),
            7 => (7, PrintClientActionsMenu()),
            8 => (8, PrintVouchersMenu()),
            9 => (9, null), // All read endpoints
            0 => (NetworkExitOption, null), // Back to main menu
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
