// Stored in a file for future retrieval.
// Easy to add more prompts or additional entry attributes.
// Clear separation of logic into classes, facilitating future updates.

using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a CSV file");
            Console.WriteLine("4. Load the journal from a CSV file");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal);
                    break;
                case "2":
                    journal.Display();
                    break;
                case "3":
                    SaveJournal(journal);
                    break;
                case "4":
                    LoadJournal(journal);
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void WriteNewEntry(Journal journal)
    {
        string[] prompts = {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        Random random = new Random();
        string selectedPrompt = prompts[random.Next(prompts.Length)];

        Console.WriteLine($"Prompt: {selectedPrompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Entry entry = new Entry
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            Prompt = selectedPrompt,
            Response = response
        };

        journal.AddEntry(entry);
        Console.WriteLine("Entry added to the journal.");
    }

    static void SaveJournal(Journal journal)
    {
        Console.Write("Enter the file name to save the journal (e.g., journal.csv): ");
        string filePath = Console.ReadLine();
        journal.SaveToCsv(filePath);
    }

    static void LoadJournal(Journal journal)
    {
        Console.Write("Enter the file name to load the journal (e.g., journal.csv): ");
        string filePath = Console.ReadLine();
        journal.LoadFromCsv(filePath);
    }
}
