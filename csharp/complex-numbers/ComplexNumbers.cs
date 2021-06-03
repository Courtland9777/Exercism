using System;

public struct ComplexNumber
{
    readonly double real;
    readonly double imaginary;

    public ComplexNumber(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    public double Real() =>
        real;

    public double Imaginary() =>
        imaginary;

    public ComplexNumber Mul(ComplexNumber other)
        => new ComplexNumber((real * other.real) - (imaginary * other.imaginary),
            (imaginary * other.real) + (real * other.imaginary));

    public ComplexNumber Add(ComplexNumber other)
        => new ComplexNumber(real + other.real, imaginary + other.imaginary);

    public ComplexNumber Sub(ComplexNumber other)
        => new ComplexNumber(real - other.real, imaginary - other.imaginary);

    public ComplexNumber Div(ComplexNumber other)
    {
        var dividedReal = (real * other.real + imaginary * other.imaginary) /
                          (Math.Pow(other.real, 2D) + Math.Pow(other.imaginary, 2D));
        var dividedImaginary = ((imaginary * other.real) - (real * other.imaginary)) /
                               (Math.Pow(other.real, 2D) + Math.Pow(other.imaginary, 2D));
        return new ComplexNumber(dividedReal, dividedImaginary);
    }

    public double Abs() => Math.Sqrt(Math.Pow(real, 2D) + Math.Pow(imaginary, 2D));

    public ComplexNumber Conjugate() => new ComplexNumber(real, -imaginary);

    public ComplexNumber Exp() =>
        new ComplexNumber(Math.Exp(real) * Math.Cos(imaginary), Math.Exp(real) * Math.Sin(imaginary));
}