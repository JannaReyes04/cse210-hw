using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    private static List<Scripture> scriptures = new List<Scripture>();

    static void Main()
    {
        Console.WriteLine("Current Directory: " + Directory.GetCurrentDirectory());
        Console.WriteLine("Welcome to the Scripture Memorizer!");

        // Load scriptures from file
        LoadScriptures(@"C:\Users\JannaReyes\OneDrive\Documents\cse210-hw\week03\ScriptureMemorizer\scriptures.txt");

        if (scriptures.Count == 0)
        {
            Console.WriteLine("No scriptures loaded. Exiting program.");
            return;
        }

        // Select a random scripture
        Random random = new Random();
        Scripture selectedScripture = scriptures[random.Next(scriptures.Count)];

        while (true)
        {
            Console.Clear();
            selectedScripture.Display();

            if (selectedScripture.IsFullyHidden())
            {
                Console.WriteLine("You have successfully memorized the scripture!");
                break;
            }

            Console.WriteLine("Press Enter to hide more words or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input?.ToLower() == "quit")
            {
                break;
            }

            selectedScripture.HideRandomWords();
        }
    }

    private static void LoadScriptures(string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"File {filename} does not exist.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split('|', 2); // Split the line into two parts: reference and text
            if (parts.Length != 2)
            {
                Console.WriteLine($"Invalid line format: {line}");
                continue;
            }

            string reference = parts[0].Trim(); // Scripture reference (e.g., "1 Corinthians 6:19-20")
            string text = parts[1].Trim();      // Scripture text

            try
            {
                ScriptureReference scriptureReference = ParseReference(reference); // Parse the reference
                scriptures.Add(new Scripture(scriptureReference, text));           // Add to scriptures list
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing line: {line}\n{ex.Message}");
            }
        }
    }

    private static ScriptureReference ParseReference(string reference)
    {
        string[] parts = reference.Split(new[] { ' ' }, 2); // Split into book name and chapter/verses
        if (parts.Length != 2)
        {
            throw new FormatException($"Invalid reference format: {reference}");
        }

        string book = parts[0].Trim();
        string chapterAndVerses = parts[1].Trim();

        if (char.IsDigit(book[0])) // Handle books with numeric prefixes (e.g., "1 Corinthians")
        {
            string[] subParts = chapterAndVerses.Split(' ', 2);
            if (subParts.Length > 1)
            {
                book = $"{book} {subParts[0]}";
                chapterAndVerses = subParts[1];
            }
        }

        string[] chapterParts = chapterAndVerses.Split(':');
        if (chapterParts.Length != 2)
        {
            throw new FormatException($"Invalid chapter and verse format: {chapterAndVerses}");
        }

        int chapter = int.Parse(chapterParts[0]);
        string verses = chapterParts[1];

        if (verses.Contains('-'))
        {
            string[] range = verses.Split('-');
            int startVerse = int.Parse(range[0]);
            int endVerse = int.Parse(range[1]);
            return new ScriptureReference(book, chapter, startVerse, endVerse);
        }
        else
        {
            int singleVerse = int.Parse(verses);
            return new ScriptureReference(book, chapter, singleVerse);
        }
    }
}

class Scripture
{
    private ScriptureReference Reference { get; }
    private string Text { get; }
    private List<Word> Words { get; }

    public Scripture(ScriptureReference reference, string text)
    {
        Reference = reference;
        Text = text;
        Words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void Display()
    {
        Console.WriteLine(Reference);
        Console.WriteLine(string.Join(" ", Words));
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        foreach (var word in Words.Where(w => !w.IsHidden).OrderBy(_ => random.Next()).Take(3))
        {
            word.Hide();
        }
    }

    public bool IsFullyHidden()
    {
        return Words.All(w => w.IsHidden);
    }
}

class ScriptureReference
{
    public string Book { get; }
    public int Chapter { get; }
    public int? StartVerse { get; }
    public int? EndVerse { get; }

    public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public ScriptureReference(string book, int chapter, int verse)
        : this(book, chapter, verse, verse) { }

    public override string ToString()
    {
        if (StartVerse == EndVerse)
        {
            return $"{Book} {Chapter}:{StartVerse}";
        }
        return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

class Word
{
    private string Text { get; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}
