using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;
public partial class NetworkClient
{
    private ClientAction? PrintClientActionsMenu()
    {
        Clear();
        WriteLine("Client Actions Menu:");
        WriteLine("-------------------------");
        if (SelectedClient is not null)
        {
            WriteLine($"Current client is {SelectedClient.Name}");
            WriteLine("-------------------------");
        }

        WriteLine("1. Authorise Guest Access");
        WriteLine("2. Exit");
        Write("Select an option: ");

        var input = ReadKey(true);

        int primaryAction = int.TryParse(input.KeyChar.ToString(), out int primary) ? primary : 0;

        if (primaryAction < 1 && primaryAction > 2)
        {
            WriteLine("Invalid selection");
            return PrintClientActionsMenu();
        }
        else if (primaryAction == 2)
        {
            return null;
        }

        return (ClientAction)primaryAction - 1;
    }

    private async Task DoClientActions(ClientAction? action, CancellationToken cancellationToken)
    {
        switch (action)
        {
            case ClientAction.AuthorizeGuestAccess:
                await uniFiClient.AuthorizeClientGuestAccess(SelectedSite!.Id, SelectedClient!.Id, cancellationToken: cancellationToken);
                break;
            default:
                WriteLine("Invalid client action, please try again.");
                break;
        }
    }
}
