using System;
using System.Linq;

public static class HighSchoolSweethearts
{
    public static string DisplaySingleLine(string studentA, string studentB)
    {
        var namesAndHeart = $"{studentA} {Convert.ToChar(0x2661)} {studentB}";
        var spacesLeft = 61 - namesAndHeart.Length;
        var spacesInFront = AddSpacesToBeginingOfString(spacesLeft);
        return string.Concat(spacesInFront,namesAndHeart,AddSpacesToEndOfString(spacesLeft - spacesInFront.Length));
    }

    private static string AddSpacesToEndOfString(int spacesLeft) =>
        string.Concat(Enumerable.Repeat(' ', spacesLeft));

    private static string AddSpacesToBeginingOfString(int spacesLeft) =>
        spacesLeft % 2 == 0
        ? string.Concat(Enumerable.Repeat(' ', (spacesLeft / 2) - 1))
        : string.Concat(Enumerable.Repeat(' ', spacesLeft / 2));

    public static string DisplayBanner(string studentA, string studentB) =>
        banner.Replace("L. G.", studentA).Replace("P. R.", studentB);
    //$@"
        //     ******       ******
        //   **      **   **      **
        // **         ** **         **
        //**            *            **
        //**                         **
        //**     {studentA}  +  {studentB}     **
        // **                       **
        //   **                   **
        //     **               **
        //       **           **
        //         **       **
        //           **   **
        //             ***
        //              *
        //";

    private static string banner =
        "      ******       ******\\n   **      **   **      **\\n **         ** **         **\\n**            *           **\\n**                         **\\n**     L. G.  +  P. R.     **\\n **                       **\\n   **                   **\\n     **               **\\n       **           **\\n         **       **\\n           **   **\\n             ***\\n              *";

    public static string DisplayGermanExchangeStudents(string studentA
        , string studentB, DateTime start, float hours)
    {
        throw new NotImplementedException($"Please implement the (static) HighSchoolSweethearts.DisplayGermanExchangeStudents() method");
    }


}
