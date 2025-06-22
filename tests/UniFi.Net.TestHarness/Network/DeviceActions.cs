using UniFi.Net.Network.Exceptions;
using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;
public partial class NetworkClient
{
    private DeviceAction? PrintDeviceActionsMenu()
    {
        const int exitOption = 2;

        Clear();
        WriteLine("Device Actions Menu:");
        WriteLine("-------------------------");
        if (SelectedDevice is not null)
        {
            WriteLine($"Current device is {SelectedDevice.Name}");
            WriteLine("-------------------------");
        }
        WriteLine("1. Restart");
        WriteLine($"{exitOption}. Exit");
        Write("Select an option: ");

        var input = ReadKey();

        int primaryAction = int.TryParse(input.KeyChar.ToString(), out int primary) ? primary : 0;

        if (primaryAction < 1 && primaryAction > exitOption)
        {
            return PrintDeviceActionsMenu();
        }
        else if (primaryAction == exitOption)
        {
            return null;
        }

        return (DeviceAction?)primaryAction-1;
    }

    private async Task DoDeviceActions(DeviceAction? action, CancellationToken cancellationToken)
    {
        if (action == null || !SelectedSiteCheck() || !SelectedDeviceCheck())
        {
            return;
        }

        try
        {
            switch (action)
            {
                case DeviceAction.Restart:
                    WriteLine($"Restarting device {SelectedDevice!.Name}...");
                    await uniFiClient.RestartDevice(SelectedSite!.Id, SelectedDevice!.Id, cancellationToken);
                    WriteLine("Device restarted.");
                    break;
                default:
                    WriteLine("Invalid device action, please try again.");
                    break;
            }

        }
        catch (UniFiNetworkException ex)
        {
            WriteLine($"Error executing device action: {ex.Message}");
        }
        catch (Exception ex)
        {
            WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        finally
        {
            WriteLine("Press any key to continue...");
            ReadKey();
        }
    }
}
