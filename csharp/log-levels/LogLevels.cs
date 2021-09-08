using System;
using System.Collections.Generic;
using System.Linq;

static class LogLine
{
    public static string Message(string logLine) =>
        logLine.Split(':').Last().Trim();

    public static string LogLevel(string logLine) =>
        logLine.Split('[', ']')[1].ToLower();

    public static string Reformat(string logLine) =>
        $"{Message(logLine)} ({LogLevel(logLine)})";
}
