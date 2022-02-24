using System;

public static class Say
{
    private const long OneThousand = 1_000;
    private const long OneMillion = 1_000_000;
    private const long OneBillion = 1_000_000_000;
    private const long OneTrillion = 1_000_000_000_000;

    public static string InEnglish(long number) =>
        number switch
        {
            < 0 => throw new ArgumentOutOfRangeException(nameof(number), "Number is less than zero"),
            < 100 => SayOneToNinetyNine(number),
            < OneThousand => SayHundredToThousandLessOne(number),
            < OneMillion => SayThousandToMillionLessOne(number),
            < OneBillion => SayMillionToBillionLessOne(number),
            < OneTrillion => SayBillionToTrillionLessOne(number),
            >= OneTrillion => throw new ArgumentOutOfRangeException(nameof(number), "Number is a trillion or greater"),
        };

    private static string SayBillionToTrillionLessOne(long input) =>
        input < OneBillion ? SayMillionToBillionLessOne(input) : input % OneBillion == 0
            ? $"{SayOneToNinetyNine(input / OneBillion)} billion"
            : $"{SayHundredToThousandLessOne(input / OneBillion)} billion {SayMillionToBillionLessOne(input % OneBillion)}";

    private static string SayMillionToBillionLessOne(long input) =>
        input < OneMillion ? SayThousandToMillionLessOne(input) : input % OneMillion == 0
            ? $"{SayOneToNinetyNine(input / OneMillion)} million"
            : $"{SayHundredToThousandLessOne(input / OneMillion)} million {SayThousandToMillionLessOne(input % OneMillion)}";

    private static string SayThousandToMillionLessOne(long input) =>
        input < OneThousand ? SayHundredToThousandLessOne(input) : input % OneThousand == 0
            ? $"{SayOneToNinetyNine(input / OneThousand)} thousand"
            : $"{SayHundredToThousandLessOne(input / OneThousand)} thousand {SayHundredToThousandLessOne(input % OneThousand)}";

    private static string SayHundredToThousandLessOne(long input) =>
        input < 100 ? SayOneToNinetyNine(input) : input % 100 == 0
            ? $"{SayOneToNinetyNine(input / 100)} hundred"
            : $"{SayOneToNinetyNine(input / 100)} hundred {SayOneToNinetyNine(input % 100)}";

    private static string SayOneToNinetyNine(long input) => input is > 99 or < 0
            ? throw new ArgumentOutOfRangeException(nameof(input), "Out of range.")
            : input < 20
            ? ((ZeroToNineteen)input).ToString().ToLower()
            : input % 10 == 0
            ? ((TwentyToNinetyByTen)int.Parse(input.ToString()[0].ToString())).ToString().ToLower()
            : $"{(TwentyToNinetyByTen)int.Parse(input.ToString()[0].ToString())}-{(ZeroToNineteen)int.Parse(input.ToString()[1].ToString())}".ToLower();
}

public enum TwentyToNinetyByTen
{
    Twenty = 2,
    Thirty = 3,
    Forty = 4,
    Fifty = 5,
    Sixty = 6,
    Seventy = 7,
    Eighty = 8,
    Ninety = 9
}

public enum ZeroToNineteen
{
    Zero = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Eleven = 11,
    Twelve = 12,
    Thirteen = 13,
    Fourteen = 14,
    Fifteen = 15,
    Sixteen = 16,
    Seventeen = 17,
    Eighteen = 18,
    Nineteen = 19
}