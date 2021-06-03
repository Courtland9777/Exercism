using System;
using System.Text;

public static class BeerSong
{
    public static string Recite(int startBottles, int takeDown)
    {
        var sb = new StringBuilder();
        for (int i = 1; i <= takeDown && startBottles >= 0; i++)
        {
            switch (startBottles)
            {
                case 0:
                    return sb.Append("No more bottles of beer on the wall, no more bottles of beer.\n")
                        .Append("Go to the store and buy some more, 99 bottles of beer on the wall.").ToString();
                case 1:
                    sb.Append(startBottles).Append(" bottle of beer on the wall, ").Append(startBottles)
                        .Append(" bottle of beer.\n")
                        .Append("Take it down and pass it around, no more bottles of beer on the wall.");
                    break;
                case 2:
                    sb.Append(startBottles).Append(" bottles of beer on the wall, ").Append(startBottles)
                        .Append(" bottles of beer.\n").Append("Take one down and pass it around, ")
                        .Append(startBottles - 1)
                        .Append(" bottle of beer on the wall.");
                    break;
                default:
                    sb.Append(startBottles).Append(" bottles of beer on the wall, ").Append(startBottles)
                        .Append(" bottles of beer.\n").Append("Take one down and pass it around, ")
                        .Append(startBottles - 1)
                        .Append(" bottles of beer on the wall.");
                    break;
            }
            if (i < takeDown)
            {
                sb.Append("\n\n");
            }
            startBottles--;
        }
        return sb.ToString();
    }
}