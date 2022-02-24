using System;

public class Clock : IComparable<Clock>
{
    private const int MinInDay = 1440;
    private readonly int _timeInMin;

    public Clock(int hours, int minutes) =>
        _timeInMin = NormalizeTime(HoursAndMinToMin(hours, minutes));

    private Clock(int minutes) => _timeInMin = NormalizeTime(minutes);

    private static int NormalizeTime(int timeInMin) =>
        timeInMin >= 0 ? timeInMin : MinInDay + timeInMin;

    private static int MinToHours(int timeInMin) =>
        (timeInMin / 60) % 24;

    private static int GetRemainingMin(int timeInMin) =>
        timeInMin % 60;

    private static int HoursAndMinToMin(int hours, int minutes) =>
        (((hours % 24) * 60) + minutes) % MinInDay;

    public Clock Add(int minutesToAdd) =>
        new(NormalizeTime((_timeInMin + minutesToAdd) % MinInDay));

    public Clock Subtract(int minutesToSubtract) =>
        new(NormalizeTime((_timeInMin - minutesToSubtract) % MinInDay));

    public override string ToString() =>
    $"{MinToHours(NormalizeTime(_timeInMin)):00}:{GetRemainingMin(NormalizeTime(_timeInMin)):00}";

    public int CompareTo(Clock other) =>
        other == null ? 1 : _timeInMin.CompareTo(other._timeInMin);
}
