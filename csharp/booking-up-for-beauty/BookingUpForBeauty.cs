using System;

static class Appointment
{
    public static DateTime Schedule(string appointmentDateDescription) =>
        DateTime.Parse(appointmentDateDescription);

    public static bool HasPassed(DateTime appointmentDate) =>
        appointmentDate < DateTime.Now;

    public static bool IsAfternoonAppointment(DateTime appointmentDate) =>
        IsTimeInTheAfternoon(appointmentDate.TimeOfDay,
            new TimeSpan(12, 0, 0),
            new TimeSpan(18, 0, 0));

    public static string Description(DateTime appointmentDate) =>
        $"You have an appointment on {appointmentDate:M/d/yyyy h:mm:ss tt}.";

    public static DateTime AnniversaryDate() =>
        new(DateTime.Now.Year, 9, 15);

    private static bool IsTimeInTheAfternoon(TimeSpan timeOfAppt, TimeSpan start, TimeSpan end) =>
        start < end ? start <= timeOfAppt && timeOfAppt < end : !(end < timeOfAppt && timeOfAppt < start);
}
