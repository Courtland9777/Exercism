using System;
using System.Globalization;
using System.Linq;
using System.Text;

public static class HighSchoolSweethearts
{
    public static string DisplaySingleLine(string studentA, string studentB)
    {
        var namesAndHeart = $"{studentA} {Convert.ToChar(0x2661)} {studentB}";
        var spacesLeft = 61 - namesAndHeart.Length;
        var spacesInFront = PadLeft(spacesLeft);
        return string.Concat(spacesInFront,namesAndHeart,AddSpacesToEndOfString(spacesLeft - spacesInFront.Length));
    }

    private static string AddSpacesToEndOfString(int spacesLeft) =>
        string.Concat(Enumerable.Repeat(' ', spacesLeft));

    private static string PadLeft(int spacesLeft) =>
        string.Concat(Enumerable.Repeat(' ', (spacesLeft / 2) - 1));

    public static string DisplayBanner(string studentA, string studentB)
    {
        var sb = new StringBuilder();
        sb.Append("     ******       ******\n");
        sb.Append("   **      **   **      **\n");
        sb.Append(" **         ** **         **\n");
        sb.Append("**            *            **\n");
        sb.Append("**                         **\n");
        sb.Append("**     ").Append(studentA).Append(" +  ").Append(studentB).Append("    **\n");
        sb.Append(" **                       **\n");
        sb.Append("   **                   **\n");
        sb.Append("     **               **\n");
        sb.Append("       **           **\n");
        sb.Append("         **       **\n");
        sb.Append("           **   **\n");
        sb.Append("             ***\n");
        sb.Append("              *");

        return sb.ToString();
    }

    public static string DisplayGermanExchangeStudents(string studentA
        , string studentB, DateTime start, float hours) =>
        $"{studentA} and {studentB} have been dating since {start:dd.MM.yyyy} - that's {hours.ToString("N2", CultureInfo.CreateSpecificCulture("de-DE"))} hours";
}
