using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFi.Net.TestHarness.SiteManager;

internal partial class SiteManagerClient
{
    public int PrintIspMetricsMenu()
    {
        Console.WriteLine("ISP Metrics Menu:");
        Console.WriteLine("1. List ISP Metrics");
        Console.WriteLine("2. Get ISP Metric by ID");
        Console.WriteLine("3. Exit");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
        {
            return choice;
        }
        Console.WriteLine("Invalid choice, please try again.");
        return PrintIspMetricsMenu();
    }
}
