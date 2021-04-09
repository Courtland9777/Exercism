using System;
using System.Linq;

public static class PhoneNumber
{
    public static string Clean(string phoneNumber)
    {
        phoneNumber = string.Concat(phoneNumber.ToCharArray().Where(char.IsDigit));

        if (phoneNumber.Length == 11 && ParseDigit(phoneNumber, 0) == 1) phoneNumber = phoneNumber[1..];

        return phoneNumber.Length == 10 && ParseDigit(phoneNumber,0) > 1 && ParseDigit(phoneNumber, 3) > 1 ?
            phoneNumber : throw new ArgumentException(nameof(phoneNumber));
    }

    private static int ParseDigit(string phNumber, int index) =>
        int.Parse(phNumber[index].ToString());
}