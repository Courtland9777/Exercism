using System.Linq;
using System.Text.RegularExpressions;

public class LogParser
{
    public bool IsValidLine(string text) =>
        Regex.IsMatch(text, @"^\[[A-Z]{3}\].*$");

    public string[] SplitLogLine(string text) => Regex.Split(text, @"<.+?>");

    public int CountQuotedPasswords(string lines) => Regex.Matches(lines, @".*[^\""]*password[^\""]*.*",
        RegexOptions.IgnoreCase | RegexOptions.Multiline).Count;

    public string RemoveEndOfLineText(string line) =>
        Regex.Replace(line, @"end-of-line\d+", "");

    public string[] ListLinesWithPasswords(string[] lines) => (from line in lines
        let match = Regex.Match(line, @"password[^\s]+", RegexOptions.IgnoreCase)
        select match.Success ? $"{match.Value}: {line}" : $"--------: {line}").ToArray();
}
