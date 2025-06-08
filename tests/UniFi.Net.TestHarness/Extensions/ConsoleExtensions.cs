namespace UniFi.Net.TestHarness;
internal static class ConsoleExtensions
{
    extension(Console)
    {
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
