using System.Linq;

public static class Bob
{
    private const string Whatever = "Whatever.";
    private const string ChillOut = "Whoa, chill out!";
    private const string Sure = "Sure.";
    private const string CalmDown = "Calm down, I know what I'm doing!";
    private const string Fine = "Fine. Be that way!";

    public static string Response(string statement) =>
        string.IsNullOrWhiteSpace(statement)
            ? Fine
            : AreAllLettersCapital(statement)
              && IsLastCharQuestionMark(statement)
              && IsThereLetters(statement)
            ? CalmDown
            : AreAllLettersCapital(statement) && IsThereLetters(statement)
            ? ChillOut
            : IsLastCharQuestionMark(statement)
            ? Sure
            : Whatever;

    private static bool IsThereLetters(string statement) =>
        statement.Any(char.IsLetter);

    private static bool AreAllLettersCapital(string statement) =>
        statement.Where(char.IsLetter).All(char.IsUpper);

    private static bool IsLastCharQuestionMark(string statement) =>
        statement.Trim().Last().Equals('?');
}