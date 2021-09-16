using System;
using System.Globalization;
using System.Runtime.InteropServices;

public enum Location
{
    NewYork,
    London,
    Paris
}

public enum AlertLevel
{
    Early,
    Standard,
    Late
}

public static class Appointment
{
    public static DateTime ShowLocalTime(DateTime dtUtc) => dtUtc.ToLocalTime();

    public static DateTime Schedule(string appointmentDateDescription, Location location) => location switch
    {
        Location.NewYork => ToUtc(DateTime.Parse(appointmentDateDescription), TzInfo(IsWindows() ? "Eastern Standard Time" : "America/New_York")),
        Location.London => ToUtc(DateTime.Parse(appointmentDateDescription), TzInfo(IsWindows() ? "GMT Standard Time" : "Europe/London")),
        Location.Paris => ToUtc(DateTime.Parse(appointmentDateDescription), TzInfo(IsWindows() ? "W. Europe Standard Time" : "Europe/Paris")),
        _ => throw new ArgumentException("Invalid location.", nameof(location))
    };

    private static DateTime ToUtc(DateTime dt, TimeZoneInfo tzi) =>
        TimeZoneInfo.ConvertTimeToUtc(dt, tzi);

    private static bool IsWindows() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    private static TimeZoneInfo TzInfo(string timezoneId) =>
        TimeZoneInfo.FindSystemTimeZoneById(timezoneId);

    public static DateTime GetAlertTime(DateTime appointment, AlertLevel alertLevel) => alertLevel switch
    {
        AlertLevel.Late => appointment.AddMinutes(-30),
        AlertLevel.Standard => appointment.Add(-(new TimeSpan(1, 45, 0))),
        AlertLevel.Early => appointment.AddDays(-1),
        _ => throw new ArgumentException("Invalid AlertLevel.", nameof(alertLevel))
    };

    public static bool HasDaylightSavingChanged(DateTime dt, Location location) => location switch
    {
        Location.NewYork => HasDstChanged(TzInfo(IsWindows() ? "Eastern Standard Time" : "America/New_York"), dt),
        Location.London => HasDstChanged(TzInfo(IsWindows() ? "GMT Standard Time" : "Europe/London"), dt),
        Location.Paris => HasDstChanged(TzInfo(IsWindows() ? "W. Europe Standard Time" : "Europe/Paris"), dt),
        _ => throw new ArgumentException("Invalid location.", nameof(location))
    };

    private static bool HasDstChanged(TimeZoneInfo tz, DateTime dt) =>
        tz.IsDaylightSavingTime(dt) != tz.IsDaylightSavingTime(dt.AddDays(-7));

    public static DateTime NormalizeDateTime(string dtStr, Location location)
    {
        try
        {
            return location switch
            {
                Location.NewYork => DateTime.Parse(dtStr, GetCultureInfo("en-US")),
                Location.London => DateTime.Parse(dtStr, GetCultureInfo("en-GB")),
                Location.Paris => DateTime.Parse(dtStr, GetCultureInfo("en-GB")),
                _ => throw new ArgumentException("Invalid location.", nameof(location))
            };
        } catch
        {
            return new(1, 1, 1, 0, 0, 0);
        }
    }

    private static CultureInfo GetCultureInfo(string country) => new(country);
}
