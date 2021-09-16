using System;
using System.Linq;
using System.Text;

public static class Identifier
{
    private static readonly string GreekLetters;

    static Identifier()
    {
        GreekLetters = CreateGreekLettersString();
    }

    public static string Clean(string identifier)
    {
        if (identifier.All(c => !char.IsLetter(c))) return string.Empty;
        if (identifier.Contains(" ")) identifier = ReplaceSpaceWithUnderScore(identifier);
        if (identifier.Any(char.IsControl)) identifier = ReplaceControlCharacter(identifier);
        if (identifier.Contains("-")) identifier = KebabToCamelCase(identifier);
        return RemoveGreekLetters(identifier, GreekLetters);
    }

    private static string ReplaceSpaceWithUnderScore(string identifier) =>
        identifier.Replace(' ', '_');

    private static string ReplaceControlCharacter(string identifier)
    {
        var sb = new StringBuilder();

        foreach (var c in identifier)
        {
            if (char.IsControl(c))
            {
                sb.Append("CTRL");
                continue;
            }

            sb.Append(c);
        }

        return sb.ToString();
    }

    private static string KebabToCamelCase(string identifier)
    {
        var sb = new StringBuilder();
        sb.Append(identifier[0]);
        for (var i = 1; i < identifier.Length; i++)
        {
            if (char.IsLetter(identifier[i]) && identifier[i - 1].Equals('-'))
            {
                sb.Append(identifier[i].ToString().ToUpper());
                continue;
            }

            sb.Append(identifier[i]);
        }
        return sb.ToString().Replace("-", string.Empty);
    }

    private static string CreateGreekLettersString()
    {
        var sb = new StringBuilder();
        for (var i = 0x03B1; i <= 0x03C9; i++)
        {
            sb.Append(Convert.ToChar(i));
        }

        return sb.ToString();
    }

    private static string RemoveGreekLetters(string identifier, string greekLetters) => new(identifier
        .Where(c => (!greekLetters.Contains(c) && char.IsLetter(c)) || c.Equals('_')).ToArray());
}
