using UniFi.Net.Network.Models;
using UniFi.Net.TestHarness.Network;
using static System.Console;

namespace UniFi.Net.TestHarness.Network;
public partial class NetworkClient
{
    private DeviceSummary? SelectedDevice { get; set; } = null;

    private static int PrintDevicesMenu()
    {
        Clear();
        WriteLine("Devices Menu:");
        WriteLine("1. List Devices");
        WriteLine("2. Get Device Details");
        Write("Select an option: ");

        var input = ReadKey();

        WriteLine();

        if (!Int32.TryParse(input.KeyChar.ToString(), out var option) || option > 3)
        {
            WriteLine("Invalid option, please try again.");
            return PrintDevicesMenu();
        }

        return option;
    }

    private async Task DoDevices(int action, CancellationToken cancellationToken)
    {
        switch (action)
        {
            case 1:
                if (!SelectedSiteCheck()) return;
                var devices = await uniFiClient.ListDevices(SelectedSite!.Id, null, null, 200, cancellationToken);
                if (PrintDeviceSelectionMenu(devices.Data))
                {
                    SelectedDevice!.PrettyPrint();
                }
                break;
            case 2:
                if (!SelectedSiteCheck()) return;
                if (!SelectedDeviceCheck()) return;
                var device = await uniFiClient.GetDevice(SelectedSite!.Id, SelectedDevice!.Id, cancellationToken);
                device.PrettyPrint();
                break;
            default:
                WriteLine("Invalid device action, please try again.");
                break;
        }
        WriteLine("Press any key to continue...");
        ReadKey();
    }

    private bool PrintDeviceSelectionMenu(IReadOnlyList<UniFi.Net.Network.Models.DeviceSummary> devices)
    {
        // Obviously this will not work if there are more than 8 devices, but for simplicity, we will assume there are fewer.
        Clear();
        WriteLine("Select a Device:");
        WriteLine("-------------------------------");
        for (int i = 0; i < devices.Count; i++)
        {
            var device = devices.ElementAt(i);
            WriteLine($"{i + 1}. {device.Name} ({device.Id})");
        }
        WriteLine($"{devices.Count + 1}. Back to Devices Menu");
        Write("Select an option: ");

        var input = ReadLine();
        WriteLine();

        if (!Int32.TryParse(input, out var option) || option < 1 || option > devices.Count + 1)
        {
            WriteLine("Invalid option, please try again.");
            PrintDeviceSelectionMenu(devices);
        }
        else if (option < devices.Count + 1)
        {
            SelectedDevice = devices[option - 1];
            // Back to Devices Menu
            return true;
        }

        return false;
    }

    private bool SelectedDeviceCheck()
    {
        if (SelectedDevice == null)
        {
            WriteLine("No device selected. Please select a device first.");
            return false;
        }
        return true;
    }
}
