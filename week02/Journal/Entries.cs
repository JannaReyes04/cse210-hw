// Stored in a file for future retrieval.
// Easy to add more prompts or additional entry attributes.
// Clear separation of logic into classes, facilitating future updates.

using System;

class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    // Format the entry as a CSV row
    public string ToCsvRow()
    {
        string safeDate = EscapeCsv(Date);
        string safePrompt = EscapeCsv(Prompt);
        string safeResponse = EscapeCsv(Response);
        return $"{safeDate},{safePrompt},{safeResponse}";
    }

    // Escape special characters for CSV
    private string EscapeCsv(string field)
    {
        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\""); // Escape quotes by doubling them
            field = $"\"{field}\""; // Wrap in quotes
        }
        return field;
    }
}
