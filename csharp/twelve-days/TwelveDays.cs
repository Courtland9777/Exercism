public static class TwelveDays
{
    private static readonly string[] Gifts =
    {
        "a Partridge in a Pear Tree.",
        "two Turtle Doves",
        "three French Hens",
        "four Calling Birds",
        "five Gold Rings",
        "six Geese-a-Laying",
        "seven Swans-a-Swimming",
        "eight Maids-a-Milking",
        "nine Ladies Dancing",
        "ten Lords-a-Leaping",
        "eleven Pipers Piping",
        "twelve Drummers Drumming"
    };

    private static readonly string[] Days =
    {
        "first", "second", "third", "fourth", "fifth",
        "sixth", "seventh", "eighth", "ninth", "tenth",
        "eleventh", "twelfth"
    };

    public static string Recite(int verseNumber)
    {
        var verse = $"On the {Days[verseNumber - 1]} day of Christmas my true love gave to me: ";
        for (var i = verseNumber - 1; i >= 0; i--)
        {
            if (i == 0 && verseNumber != 1)
            {
                verse += "and ";
            }

            verse += $"{Gifts[i]}{(i != 0 ? ", " : "")}";
        }

        return verse;
    }

    public static string Recite(int startVerse, int endVerse)
    {
        var song = "";
        for (var i = startVerse; i <= endVerse; i++)
        {
            song += Recite(i) + "\n";
        }

        return song.TrimEnd();
    }
}