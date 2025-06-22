using UniFi.Net.Network.Filters;
using UniFi.Net.Network.Models;

namespace UniFi.Net.TestHarness.Network;
public partial class NetworkClient
{
    private Voucher? SelectedVoucher { get; set; } = null;

    private int? PrintVouchersMenu()
    {
        const int exitOption = 6;

        Clear();
        WriteLine("Vouchers Menu:");
        WriteLine("-------------------------");
        if (SelectedSite is not null)
        {
            WriteLine($"Current site is {SelectedSite.Name}");
            WriteLine("-------------------------");
        }
        if (SelectedVoucher is not null)
        {
            WriteLine($"Current voucher is {SelectedVoucher.Name}");
            WriteLine("-------------------------");
        }
        WriteLine("1. List Vouchers");
        WriteLine("2. Generate Vouchers");
        WriteLine("3. Delete Vouchers");
        WriteLine("4. Get Voucher");
        WriteLine("5. Delete Voucher");
        WriteLine($"{exitOption}. Back to Main Menu");
        Write("Select an option: ");
        var input = ReadKey(true);

        int primaryAction = Int32.TryParse(input.KeyChar.ToString(), out int primary) ? primary : 0;
        if (primaryAction < 1 || primaryAction > exitOption)
        {
            WriteLine("Invalid option, please try again.");
            ReadKey(true);
            return PrintVouchersMenu();
        }
        else if (primaryAction == exitOption)
        {
            return null;
        }

        return primaryAction;
    }

    private async Task DoVouchers(int action, CancellationToken cancellationToken)
    {
        if (!SelectedSiteCheck())
        {
            return;
        }

        switch (action)
        {
            case 1:
                var vouchers = await uniFiClient.ListVouchers(SelectedSite!.Id, cancellationToken: cancellationToken);
                PrintVoucherSelectionMenu(vouchers.Data);
                break;
            case 2:
                var generatedVouchers = await uniFiClient.GenerateVouchers(SelectedSite!.Id, "TestHarness", 2, 60, 2, cancellationToken: cancellationToken);
                if (generatedVouchers.Any())
                {
                    WriteLine("Generated Vouchers:");
                    foreach (var voucher in generatedVouchers)
                    {
                        WriteLine($"- {voucher.Name} ({voucher.Id})");
                    }
                }
                else
                {
                    WriteLine("No vouchers were generated.");
                }
                WriteLine("Press any key to continue...");
                ReadKey(true);
                break;
            case 3:
                var deleteVouchersResult = await uniFiClient.DeleteVouchers(SelectedSite!.Id, new EqualityFilter<string>("name", "TestHarness"), cancellationToken: cancellationToken);
                WriteLine($"{deleteVouchersResult} voucher(s) deleted");
                WriteLine("Press any key to continue...");
                ReadKey(true);
                break;
            case 4:
                if (!SelectedVoucherCheck())
                {
                    return;
                }
                var receivedVoucher = await uniFiClient.GetVoucher(SelectedSite!.Id, SelectedVoucher!.Id, cancellationToken);
                WriteLine($"Voucher: Name {receivedVoucher.Name}, Id {receivedVoucher.Id}");
                WriteLine("Press any key to continue...");
                ReadKey(true);
                break;
            case 5:
                if (!SelectedVoucherCheck())
                {
                    return;
                }
                var deleteVoucherResult = await uniFiClient.DeleteVoucher(SelectedSite!.Id, SelectedVoucher!.Id, cancellationToken);
                WriteLine($"{deleteVoucherResult} voucher(s) deleted");
                WriteLine("Press any key to continue...");
                ReadKey(true);
                break;
            default:
                WriteLine("Invalid voucher action, please try again.");
                break;
        }
    }

    private void PrintVoucherSelectionMenu(IReadOnlyList<Voucher> vouchers)
    {
        Clear();
        WriteLine("Select a Voucher:");
        WriteLine("-------------------------------");
        for (int i = 0; i < vouchers.Count; i++)
        {
            var voucher = vouchers[i];
            WriteLine($"{i + 1}. {voucher.Name} ({voucher.Id})");
        }
        WriteLine($"{vouchers.Count + 1}. Back to Sites Menu");
        Write("Select an option (and press enter): ");

        var input = ReadLine();

        if (!Int32.TryParse(input, out var option) || option > vouchers.Count + 1)
        {
            PrintVoucherSelectionMenu(vouchers);
        }
        else if (option < vouchers.Count + 1)
        {
            SelectedVoucher = vouchers[option - 1];
            // Back to Vouchers Menu
            return;
        }
    }

    private bool SelectedVoucherCheck()
    {
        if (SelectedVoucher == null)
        {
            WriteLine("No voucher selected. Please select a voucher first.");
            WriteLine("Press any key to continue...");
            ReadKey(true);
            return false;
        }
        return true;
    }
}
