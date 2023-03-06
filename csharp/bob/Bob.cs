using System.Linq;

public static class Bob
{
    private const string Whatever = "Whatever.";
    private const string ChillOut = "Whoa, chill out!";
    private const string Sure = "Sure.";
    private const string CalmDown = "Calm down, I know what I'm doing!";
    private const string Fine = "Fine. Be that way!";

    public static string Response(string statement) =>
        statement switch
        {
            not null when string.IsNullOrWhiteSpace(statement) => Fine,
            not null when AreAllLettersCapital(statement) && IsLastCharQuestionMark(statement) &&
                          AnyLetters(statement) => CalmDown,
            not null when AreAllLettersCapital(statement) && AnyLetters(statement) => ChillOut,
            not null when IsLastCharQuestionMark(statement) => Sure,
            _ => Whatever
        };


    private static bool AnyLetters(string statement) =>
        statement.Any(char.IsLetter);

    private static bool AreAllLettersCapital(string statement) =>
        statement.Where(char.IsLetter).All(char.IsUpper);

    private static bool IsLastCharQuestionMark(string statement) =>
        statement.TrimEnd().Last().Equals('?');
}