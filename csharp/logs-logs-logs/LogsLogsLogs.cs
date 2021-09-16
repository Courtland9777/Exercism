using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

public enum LogLevel
{
    Trace = 0,
    Debug = 1,
    Info = 4,
    Warning = 5,
    Error = 6,
    Fatal = 7,
    Unknown = 42
}
static class LogLine
{
    public static LogLevel ParseLogLevel(string logLine)
    {
        var dict = new Dictionary<string, LogLevel>
        {
            {"TRC", LogLevel.Trace},
            {"DBG", LogLevel.Debug},
            {"INF", LogLevel.Info},
            {"WRN", LogLevel.Warning},
            {"ERR", LogLevel.Error},
            {"FTL", LogLevel.Fatal},
        };
        try
        {
            return dict[ParseLog(logLine)];
        }
        catch
        {
            return LogLevel.Unknown;
        }
    }

    private static string ParseLog(string logLine) =>
        logLine.Split(':')[0].Substring(1, 3);

    public static string OutputForShortLog(LogLevel logLevel, string message)
    {
        return $"{(int)logLevel}:{message}";
    }

}
