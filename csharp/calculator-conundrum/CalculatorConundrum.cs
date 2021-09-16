using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation) =>
        (operand2 == 0 && operation.Equals("/"))
        ? "Division by zero is not allowed."
        : $"{operand1} {operation ?? throw new ArgumentNullException(nameof(operation), "operation is null.")} {operand2} = {Total(operand1, operand2, operation)}";
    
    private static int Total(int operand1, int operand2, string operation) => operation switch
    {
        "" => throw new ArgumentException("operation empty.", nameof(operation)),
        "+" => SimpleOperation.Addition(operand1, operand2),
        "*" => SimpleOperation.Multiplication(operand1, operand2),
        "/" => operand2 == 0 ? throw new DivideByZeroException("Division by zero is not allowed.") : SimpleOperation.Division(operand1, operand2),
        _ => throw new ArgumentOutOfRangeException(nameof(operation), "Invalid operator.")
    };
}

/**** Please do not modify the code below ****/
public static class SimpleOperation
{
    public static int Division(int operand1, int operand2)
    {
        return operand1 / operand2;
    }

    public static int Multiplication(int operand1, int operand2)
    {
        return operand1 * operand2;
    }
    public static int Addition(int operand1, int operand2)
    {
        return operand1 + operand2;
    }
}
