public static class LogAnalysis 
{
    public static string SubstringAfter(this string s, string stringToMatch) =>
        s.Split(stringToMatch)[1];

    public static string SubstringBetween(this string s, string stringToMatch1, string stringToMatch2) =>
        s.Split(stringToMatch1)[1].Split(stringToMatch2)[0];

    public static string Message(this string s) =>
        s.SubstringAfter(": ");
                    
    public static string LogLevel(this string s) =>
        s.SubstringBetween("[", "]");
}