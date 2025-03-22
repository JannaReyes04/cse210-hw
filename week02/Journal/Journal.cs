using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    public List<Entry> Entries { get; set; } = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        Entries.Add(entry);
    }

    public void Display()
    {
        foreach (var entry in Entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

    public void SaveToCsv(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Write header row
            writer.WriteLine("Date,Prompt,Response");

            // Write each entry as a CSV row
            foreach (Entry entry in Entries)
            {
                writer.WriteLine(entry.ToCsvRow());
            }
        }
        Console.WriteLine($"Journal saved to {filePath}");
    }

    public void LoadFromCsv(string filePath)
    {
        Entries.Clear();
        using (StreamReader reader = new StreamReader(filePath))
        {
            // Skip the header row
            reader.ReadLine();

            // Read each line and parse it into an Entry
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(',');
                if (parts.Length == 3)
                {
                    Entries.Add(new Entry
                    {
                        Date = parts[0].Trim('"'),
                        Prompt = parts[1].Trim('"'),
                        Response = parts[2].Trim('"')
                    });
                }
            }
        }
        Console.WriteLine($"Journal loaded from {filePath}");
    }
}
