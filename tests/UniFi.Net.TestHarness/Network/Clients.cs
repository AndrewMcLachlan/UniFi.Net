using static System.Console;

namespace UniFi.Net.TestHarness.Network;

public partial class NetworkClient
{
    private UniFi.Net.Network.Models.Client? SelectedClient { get; set; } = null;

    private static int PrintClientsMenu()
    {
        Clear();
        WriteLine("Clients Menu:");
        WriteLine("1. List Clients");
        WriteLine("2. Get Client Details");
        WriteLine("3. Back to Main Menu");
        Write("Select an option: ");

        var input = ReadKey();

        WriteLine();

        if (!Int32.TryParse(input.KeyChar.ToString(), out var option) || option > 3)
        {
            WriteLine("Invalid option, please try again.");
            return PrintClientsMenu();
        }

        return option;
    }

    private async Task DoClients(int action, CancellationToken cancellationToken)
    {
        switch (action)
        {
            case 1:
                if (!SelectedSiteCheck()) return;
                var clients = await uniFiClient.ListClients(SelectedSite!.Id, null, null, 200, cancellationToken);
                if (PrintClientSelectionMenu(clients.Data))
                {
                    PrintClient(SelectedClient);
                }

                break;
            case 2:
                WriteLine("Getting Client Details...");
                // Call method to get client details
                break;
            default:
                WriteLine("Invalid client action, please try again.");
                break;
        }
        WriteLine("Press any key to continue...");
        ReadKey();
    }

    private bool PrintClientSelectionMenu(IReadOnlyList<UniFi.Net.Network.Models.Client> clients)
    {
        // Obviously this will not work if there are more than 8 clients, but for simplicity, we will assume there are fewer.
        Clear();
        WriteLine("Select a Client:");
        WriteLine("-------------------------------");
        for (int i = 0; i < clients.Count; i++)
        {
            var client = clients.ElementAt(i);
            WriteLine($"{i + 1}. {client.Name} ({client.Id})");
        }
        WriteLine($"{clients.Count + 1}. Back to Clients Menu");
        Write("Select an option: ");

        var input = ReadLine();
        WriteLine();

        if (!Int32.TryParse(input, out var option) || option < 1 || option > clients.Count + 1)
        {
            WriteLine("Invalid option, please try again.");
            PrintClientSelectionMenu(clients);
        }
        else if (option < clients.Count + 1)
        {
            SelectedClient = clients[option - 1];
            // Back to Clients Menu
            return true;
        }

        return false;
    }

    private static void PrintClient(UniFi.Net.Network.Models.Client? client)
    {
        if (client is null) return;

        WriteLine("Client Details");
        WriteLine("--------------");
        WriteLine($"Id:              {client.Id}");
        WriteLine($"Name:            {client.Name}");
        WriteLine($"Connected At:    {client.ConnectedAt:yyyy-MM-dd HH:mm:ss}");
        WriteLine($"IP Address:      {client.IpAddress}");
        WriteLine($"Type:            {client.Type}");
        WriteLine($"MAC Address:     {client.MacAddress}");
        WriteLine($"Uplink DeviceId: {client.UplinkDeviceId}");
        WriteLine($"Access Type:     {client.Access?.Type}");
        WriteLine();
    }
}

