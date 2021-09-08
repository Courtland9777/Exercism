using System;
using System.Linq.Expressions;

static class AssemblyLine
{
    private const int ProductionPerHour = 221;

    public static double ProductionRatePerHour(int speed) =>
        GetSuccessRate(speed)*0.01 * speed * ProductionPerHour;

    public static int WorkingItemsPerMinute(int speed) =>
        (int)ProductionRatePerHour(speed) / 60;

    private static int GetSuccessRate(int speed) =>
         speed < 5 ? 100 :
            speed < 9 ? 90 :
            speed < 10 ? 80 :
            speed == 10 ? 77 : throw new ArgumentOutOfRangeException(nameof(speed));
}
