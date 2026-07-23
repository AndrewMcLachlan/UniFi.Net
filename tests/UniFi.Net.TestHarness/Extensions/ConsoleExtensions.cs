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

    /// <summary>
    /// Writes an error message in red.
    /// </summary>
    public static void WriteError(string message)
    {
        var original = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = original;
    }

    /// <summary>
    /// Clears the console, renders a titled numbered menu and returns the chosen
    /// 1-based option, or 0 for the back option. Re-prompts until the input is valid.
    /// Handles any number of options (input is read as a full line).
    /// </summary>
    public static int PromptOption(string title, IReadOnlyList<string> options, string backLabel = "Back")
    {
        while (true)
        {
            Console.Clear();
            foreach (var line in title.Split('\n'))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(new string('-', Math.Max(8, title.Split('\n').Max(l => l.Length))));
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            Console.WriteLine($"0. {backLabel}");
            Console.Write("Select an option (and press enter): ");

            var input = Console.ReadLine();

            if (Int32.TryParse(input, out var option) && option >= 0 && option <= options.Count)
            {
                Console.WriteLine();
                return option;
            }

            WriteError("Invalid option, please try again.");
            Console.PressAnyKeyToContinue();
        }
    }

    /// <summary>
    /// Clears the console, renders a numbered selection list and returns the chosen item,
    /// or <see langword="null"/> if the user backs out (or the list is empty).
    /// </summary>
    public static T? SelectItem<T>(string title, IReadOnlyList<T> items, Func<T, string> label) where T : class
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No items found.");
            Console.PressAnyKeyToContinue();
            return null;
        }

        var option = PromptOption(title, [.. items.Select(label)]);

        return option == 0 ? null : items[option - 1];
    }
}
