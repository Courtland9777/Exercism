using System;
using Xunit;
using Exercism.Tests;

public class OperatorOverloadingTests
{
    [Fact]
    public void Equality_true()
    {
        Assert.True(Eq(new CurrencyAmount(55, "HD"), new CurrencyAmount(55, "HD")));
    }

    [Fact]
    public void Equality_false()
    {
        Assert.False(Eq(new CurrencyAmount(55, "HD"), new CurrencyAmount(60, "HD")));
    }

    [Fact]
    public void Equality_bad()
    {
        Assert.Throws<ArgumentException>(() => Eq(new CurrencyAmount(55, "HD"), new CurrencyAmount(60, "USD")));
    }

    private bool Eq(CurrencyAmount amountA, CurrencyAmount amountB)
    {
        return amountA == amountB;
    }
}
