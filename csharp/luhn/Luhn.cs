using System.Linq;

public static class Luhn
{
    private static int ConvertDigit(int digit, int position)
    {
        var value = position % 2 != 0 ? digit * 2 : digit;
        return value > 9 ? value - 9 : value;
    }

    public static bool IsValid(string number)
    {
        var digits = number
            .Reverse()
            .Where(c => c != ' ')
            .Select(c => char.IsDigit(c) ? c - '0' : -1)
            .ToList();
        var checksum = digits
            .Select(ConvertDigit)
            .Sum();
        return digits.Count > 1 && digits.All(d => d >= 0) && checksum % 10 == 0;
    }
}