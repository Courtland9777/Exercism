using System;

public enum Code
{
    Encode = 1,
    Decode = -1
}

public class SimpleCipher
{
    private const string DefaultKey = "aaaaaaaaaa";

    public SimpleCipher() =>
        Key = DefaultKey;

    public SimpleCipher(string key) =>
        Key = key;

    public string Key { get; }

    public string Encode(string plaintext) =>
        ShiftLetters(plaintext, (int)Code.Encode);

    public string Decode(string ciphertext) =>
        ShiftLetters(ciphertext, (int)Code.Decode);

    private string ShiftLetters(string text, int shiftDirection)
    {
        char[] buffer = text.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = (char)(buffer[i] + ((Key[i % Key.Length] - 97) * shiftDirection));

            if (letter > 'z')
            {
                letter = (char)(letter - 26);
            }
            else if (letter < 'a')
            {
                letter = (char)(letter + 26);
            }

            buffer[i] = letter;
        }
        return new string(buffer);
    }
}