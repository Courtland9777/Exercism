using System;
using System.Collections.Generic;
using System.Linq;

public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

public class Meetup
{
    private readonly int _month;
    private readonly int _year;

    public Meetup(int month, int year)
    {
        _month = month;
        _year = year;
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule) => schedule switch
    {
        Schedule.First => PossibleDateList(1, 7).First(d => d.DayOfWeek == dayOfWeek),
        Schedule.Second => PossibleDateList(8, 14).First(d => d.DayOfWeek == dayOfWeek),
        Schedule.Third => PossibleDateList(15, 21).First(d => d.DayOfWeek == dayOfWeek),
        Schedule.Fourth => PossibleDateList(22, 28).First(d => d.DayOfWeek == dayOfWeek),
        Schedule.Last => PossibleDateList(1, DateTime.DaysInMonth(_year, _month)).Last(d => d.DayOfWeek == dayOfWeek),
        Schedule.Teenth => PossibleDateList(13, 19).First(d => d.DayOfWeek == dayOfWeek),
        _ => throw new ArgumentOutOfRangeException(nameof(schedule), schedule, "Invalid enum selection."),
    };

    private IEnumerable<DateTime> PossibleDateList(int start, int end)
    {
        var list = new List<DateTime>();
        for (int i = start; i <= end; i++)
        {
            list.Add(new DateTime(_year,_month,i));
        }

        return list;
    }
}