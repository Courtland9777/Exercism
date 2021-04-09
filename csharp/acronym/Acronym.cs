using System.Linq;

public static class Acronym
{
    public static string Abbreviate(string phrase) =>
        string.Concat(phrase.Split(new[] { ' ', '-' })
            .Where(a => a.Length > 0)
            .Select(c => c.First(char.IsLetter))).ToUpper();
}