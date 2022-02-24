using System;

public class CalculationException : Exception
{
    private int Operand1 { get; }
    private int Operand2 { get; }
    public override string Message { get; }
    private Exception Inner { get; }
    public CalculationException(int operand1, int operand2, string message, Exception inner)
    {
        Operand1 = operand1;
        Operand2 = operand2;
        Message = message;
        Inner = inner;
    }
}

public class CalculatorTestHarness
{
    private Calculator _calculator;

    public CalculatorTestHarness(Calculator calculator)
    {
        _calculator = calculator;
    }

    public string TestMultiplication(int x, int y)
    {
        try
        {
            Multiply(x, y);
            return "Multiply succeeded";
        }
        catch (CalculationException ex)
        {
            return ex.Message;
        }
    }

    public void Multiply(int x, int y)
    {
        try
        {
            checked
            {
                int result = (x * y);
            }
        }
        catch (Exception ex)
        {
            if (x < 0 && y < 0)
            {
                throw new CalculationException(x, y, $"Multiply failed for negative operands. {ex.Message}",
                    ex.InnerException);
            }
            else
            {
                throw new CalculationException(x, y, $"Multiply failed for mixed or positive operands. {ex.Message}",
                    ex.InnerException);
            }
        }
    }
}


// Please do not modify the code below.
// If there is an overflow in the multiplication operation
// then a System.OverflowException is thrown.
public class Calculator
{
    public int Multiply(int x, int y)
    {
        checked
        {
            return x * y;
        }
    }
}
