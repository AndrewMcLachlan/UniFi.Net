using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFi.Net.TestHarness.SiteManager;
internal partial class SiteManagerClient
{
    public int PrintSDWanConfigsMenu()
    {
        Console.WriteLine("SD-WAN Configs Menu:");
        Console.WriteLine("1. List SD-WAN Configs");
        Console.WriteLine("2. Get SD-WAN Config Status");
        Console.WriteLine("3. Exit");
        Console.Write("Select an option: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
        {
            return choice;
        }
        Console.WriteLine("Invalid choice. Please try again.");
        return PrintSDWanConfigsMenu();
    }
}
