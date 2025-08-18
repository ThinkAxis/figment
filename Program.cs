// Usings
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Hashing;

namespace figment
{
    class Program
    {
        static async Task Main()
        {
            string[] options = { "Browse directory for files", "Find duplicate files in directory" };
            int selectedIndex = 0;

            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("Figment\n");
                Console.WriteLine("Use ↑ and ↓ to navigate, Enter to select:\n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"  {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                    selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
                else if (key == ConsoleKey.DownArrow)
                    selectedIndex = (selectedIndex + 1) % options.Length;

            } while (key != ConsoleKey.Enter);

            Console.Clear();
            switch (selectedIndex)
            {
                case 0:
                    Console.Write("Enter directory to scan: ");
                    string directoryPath = Console.ReadLine() ?? string.Empty;

                    var scanner = new FileScanner();
                    scanner.TraverseDirectory(directoryPath);

                    Console.WriteLine("Browsing directory for files...");
                    await ShowSpinnerForDuration(3000);
                   
                    Console.WriteLine("\nDirectory browsing complete, log file available in Data/");
                    break;
                case 1:
                    Console.WriteLine("Finding duplicates in files...");
                    await ShowSpinnerForDuration(5000);
                    Console.WriteLine("\nDuplicate search complete.");
                    break;
            }
        }

        static async Task ShowSpinnerForDuration(int durationMs)
        {
            using var cts = new CancellationTokenSource();
            var spinnerTask = ShowSpinner(cts.Token);
            await Task.Delay(durationMs);
            cts.Cancel();
            await spinnerTask;
        }

        static async Task ShowSpinner(CancellationToken token)
        {
            var sequence = new[] { '|', '/', '-', '\\' };
            int counter = 0;
            while (!token.IsCancellationRequested)
            {
                Console.Write(sequence[counter % sequence.Length]);
                Console.Write('\b');
                counter++;
                await Task.Delay(100);
            }
        }
    }
}