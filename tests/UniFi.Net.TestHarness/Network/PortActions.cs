using UniFi.Net.Network.Exceptions;
using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;
public partial class NetworkClient
{
    private (PortAction? Primary, int? Secondary) PrintPortActionsMenu()
    {
        Clear();
        WriteLine("Port Actions Menu:");
        WriteLine("-------------------------");
        if (SelectedDevice is not null)
        {
            WriteLine($"Current device is {SelectedDevice.Name}");
            WriteLine("-------------------------");
        }
        WriteLine("1. Power Cycle");
        WriteLine("2. Exit");
        Write("Select an option: ");

        var input = ReadKey(true);

        int primaryAction = int.TryParse(input.KeyChar.ToString(), out int primary) ? primary : 0;

        int? secondaryAction = null;

        if (primaryAction < 1 || primaryAction > 2)
        {
            WriteLine("Invalid option, please try again.");
            return PrintPortActionsMenu();
        }

        if (primaryAction == 2)
        {
            return (null, null); // Exit action
        }

        if (primaryAction == 1)
        {
            Write("\nEnter port ID (and press enter): ");
            var siteInput = ReadLine();
            secondaryAction = int.TryParse(siteInput, out int secondary) ? secondary : 0;
        }

        return ((PortAction)primaryAction - 1, secondaryAction);
    }

    private async Task DoPortActions((PortAction? PortAction, int? PortId) action, CancellationToken cancellationToken)
    {
        if (action.PortAction == null || action.PortId == null)
        {
            return;
        }

        if (!SelectedSiteCheck() || !SelectedDeviceCheck())
        {
            return;
        }

        try
        {
            switch (action.PortAction)
            {
                case PortAction.PowerCycle:
                    WriteLine($"Power cycling port {action.PortId}...");
                    await uniFiClient.PowerCyclePort(action.PortId.Value, SelectedSite!.Id, SelectedDevice!.Id, cancellationToken);
                    WriteLine("Power cycle completed.");
                    break;
                default:
                    WriteLine("Invalid port action, please try again.");
                    break;
            }
        }
        catch (UniFiNetworkException ex)
        {
            WriteLine($"Error executing port action: {ex.Message}");
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
