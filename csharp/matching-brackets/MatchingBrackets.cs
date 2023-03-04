using System;
using System.Collections.Generic;

public static class MatchingBrackets
{
    private static readonly Func<Stack<char>, char, bool> IsValid = (stack, letter) =>
        stack.Count > 0 && stack.Pop() == letter;

    public static bool IsPaired(string input)
    {
        var stack = new Stack<char>();
        foreach (var letter in input)
        {
            switch (letter)
            {
                case '(':
                    stack.Push(')');
                    break;
                case '[':
                    stack.Push(']');
                    break;
                case '{':
                    stack.Push('}');
                    break;
                case ')':
                case ']':
                case '}':
                    if (!IsValid(stack, letter))
                    {
                        return false;
                    }

                    break;
            }
        }

        return stack.Count == 0;
    }
}