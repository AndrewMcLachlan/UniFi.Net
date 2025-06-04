using UniFi.Net.SiteManager.Models;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    private async Task DoHosts(int? action, CancellationToken cancellationToken)
    {
        switch(action)
        {
            case 1:
                var hosts = await client.ListHostsAsync(cancellationToken: cancellationToken);
                PrintHostSelectionMenu(hosts.Data);
                break;
            case 2:
                if (!SelectedHostCheck()) return;
                var host = await client.GetHostAsync(SelectedHost!.Id, cancellationToken);
                WriteLine($"{host.Data})");
                PressAnyKeyToContinue();
                break;
        }
    }

    private static int PrintHostsMenu()
    {
        Clear();
        WriteLine("1. List Hosts");
        WriteLine("2. Get Host by ID");
        WriteLine("3. Exit");
        Write("Select an option: ");
        if (int.TryParse(ReadKey().KeyChar.ToString(), out int choice))
        {
            return choice;
        }

        WriteLine("Invalid choice. Please try again.");
        return PrintHostsMenu();
    }

    private void PrintHostSelectionMenu(IReadOnlyList<Host> hosts)
    {
        Clear();
        WriteLine("Select a Host:");
        WriteLine("-------------------------------");
        for (int i = 0; i < hosts.Count; i++)
        {
            var host = hosts.ElementAt(i);
            WriteLine($"{host.IpAddress} ({host.Id})");
        }
        WriteLine($"{hosts.Count + 1}. Back to Hosts Menu");
        Write("\nSelect an option: ");

        var input = ReadKey();

        if (!Int32.TryParse(input.KeyChar.ToString(), out var option) || option > hosts.Count + 1)
        {
            WriteLine("Invalid option, please try again.");
            PrintHostSelectionMenu(hosts);
        }
        else if (option < hosts.Count + 1)
        {
            SelectedHost = hosts[option - 1];
            // Back to Hosts Menu
            return;
        }
    }

    private bool SelectedHostCheck()
    {
        if (SelectedHost == null)
        {
            WriteLine("No host selected. Please select a host first.");
            return false;
        }
        return true;
    }
}
