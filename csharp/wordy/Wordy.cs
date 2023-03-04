using System;

public static class Wordy
{
    public static int Answer(string question)
    {
        var parts = question
            .Replace("What is ", "")
            .Replace("?", "")
            .Replace("multiplied by", "multiplied")
            .Replace("divided by", "divided")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return parts.Length switch
        {
            1 when int.TryParse(parts[0], out var value) => value,
            3 when int.TryParse(parts[0], out var leftOperand)
                   && int.TryParse(parts[2], out var rightOperand)
                => ProcessParts(leftOperand, parts[1], rightOperand),
            5 when int.TryParse(parts[0], out var left)
                   && int.TryParse(parts[2], out var mid)
                   && int.TryParse(parts[4], out var right)
                => ProcessParts(ProcessParts(left, parts[1], mid), parts[3], right),
            _ => throw new ArgumentException()
        };
    }

    private static int ProcessParts(int leftOperand, string operation, int rightOperand) =>
        operation switch
        {
            "plus" => leftOperand + rightOperand,
            "minus" => leftOperand - rightOperand,
            "multiplied" => leftOperand * rightOperand,
            "divided" => leftOperand / rightOperand,
            _ => throw new ArgumentException()
        };
}