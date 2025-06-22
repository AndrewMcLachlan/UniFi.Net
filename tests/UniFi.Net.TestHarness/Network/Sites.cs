using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;
using static System.Console;

namespace UniFi.Net.TestHarness.Network;

public partial class NetworkClient
{
    private Site? SelectedSite { get; set; } = null;

    private async Task DoSites(int action, CancellationToken cancellationToken)
    {
        switch (action)
        {
            case 1:
                var sites = await uniFiClient.ListSites(cancellationToken: cancellationToken);
                PrintSiteSelectionMenu(sites.Data);
                break;
            case 2:
                var filteredSites = await uniFiClient.ListSites(new AndFilter(new EqualityFilter<string>("name", "Default"), new EqualityFilter<string>("name", "Default", true)), cancellationToken: cancellationToken);
                PrintSiteSelectionMenu(filteredSites.Data);
                break;
            default:
                WriteLine("Invalid site action, please try again.");
                break;
        }
    }

    private static int PrintSitesMenu()
    {
        Clear();
        WriteLine("Sites Menu");
        WriteLine("----------");
        WriteLine("1. List Sites");
        WriteLine("2. List Sites with filter");
        WriteLine("3. Back to Main Menu");
        Write("Select an option: ");
        var input = ReadKey();

        if (!Int32.TryParse(input.KeyChar.ToString(), out var option) || option > 3)
        {
            return PrintSitesMenu();
        }

        return option;
    }

    private void PrintSiteSelectionMenu(IReadOnlyList<Site> sites)
    {
        // Obviously this will not work if there are more than 8 sites, but for simplicity, we will assume there are fewer.
        Clear();
        WriteLine("Select a Site:");
        WriteLine("-------------------------------");
        for (int i = 0; i < sites.Count; i++)
        {
            var site = sites.ElementAt(i);
            WriteLine($"{i + 1}. {site.Name} ({site.Id})");
        }
        WriteLine($"{sites.Count + 1}. Back to Sites Menu");
        Write("\nSelect an option: ");

        var input = ReadKey();

        if (!Int32.TryParse(input.KeyChar.ToString(), out var option) || option > sites.Count + 1)
        {
            WriteLine("Invalid option, please try again.");
            PrintSiteSelectionMenu(sites);
        }
        else if (option < sites.Count + 1)
        {
            SelectedSite = sites[option - 1];
            // Back to Sites Menu
            return;
        }
    }
}
