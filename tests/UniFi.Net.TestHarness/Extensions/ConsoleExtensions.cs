namespace UniFi.Net.TestHarness;
internal static class ConsoleExtensions
{
    extension(Console)
    {
        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine();
            var original = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press any key to continue...");
            Console.ForegroundColor = original;
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
    /// Writes a cyan heading with an underline.
    /// </summary>
    public static void WriteHeading(string heading)
    {
        var original = Console.ForegroundColor;
        var lines = heading.Split('\n');

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(lines[0]);

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        foreach (var line in lines.Skip(1))
        {
            Console.WriteLine(line);
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('-', Math.Max(8, lines.Max(l => l.Length))));
        Console.ForegroundColor = original;
    }

    /// <summary>
    /// Clears the console, renders a titled numbered menu and returns the chosen
    /// 1-based option, or 0 for the back option. Re-prompts until the input is valid.
    /// Menus with up to nine options take a single key press; larger menus read a full line.
    /// </summary>
    public static int PromptOption(string title, IReadOnlyList<string> options, string backLabel = "Back")
    {
        while (true)
        {
            Console.Clear();
            WriteHeading(title);

            var original = Console.ForegroundColor;
            for (int i = 0; i < options.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{i + 1}. ");
                Console.ForegroundColor = original;
                Console.WriteLine(options[i]);
            }
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"0. {backLabel}");
            Console.ForegroundColor = original;

            int? choice;
            if (options.Count <= 9)
            {
                Console.Write("Select an option: ");
                choice = Int32.TryParse(Console.ReadKey(true).KeyChar.ToString(), out var key) ? key : null;
            }
            else
            {
                Console.Write("Select an option (and press enter): ");
                choice = Int32.TryParse(Console.ReadLine(), out var line) ? line : null;
            }

            if (choice is int option && option >= 0 && option <= options.Count)
            {
                Console.WriteLine();
                return option;
            }
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
