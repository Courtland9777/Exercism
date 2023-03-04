using System.Collections.Generic;

public static class SecretHandshake
{
    public static string[] Commands(int commandValue)
    {
        var result = new List<string>();
        if ((commandValue & 0b1) != 0)
        {
            result.Add("wink");
        }

        if ((commandValue & 0b10) != 0)
        {
            result.Add("double blink");
        }

        if ((commandValue & 0b100) != 0)
        {
            result.Add("close your eyes");
        }

        if ((commandValue & 0b1000) != 0)
        {
            result.Add("jump");
        }

        if ((commandValue & 0b10000) != 0)
        {
            result.Reverse();
        }

        return result.ToArray();
    }
}