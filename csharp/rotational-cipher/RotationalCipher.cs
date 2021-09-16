using System;
using System.Linq;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey) =>
        string.Concat(text.Select(ch => cipher(ch, shiftKey)));

    private static char cipher(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {
            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((((ch + key) - d) % 26) + d);
    }
}