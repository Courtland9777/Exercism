using System;

public struct CurrencyAmount : IEquatable<CurrencyAmount>
{
    private readonly decimal _amount;
    private readonly string _currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        _amount = amount;
        _currency = currency;
    }

    public decimal Amount() => _amount;
    public string Currency() => _currency;

    public bool Equals(CurrencyAmount other) => _currency != other.Currency()
        ? throw new ArgumentException("Different types of currency.")
        : _amount == other.Amount();

    public override int GetHashCode() => HashCode.Combine(_amount, _currency);

    public static bool operator ==(CurrencyAmount left, CurrencyAmount right) => left.Equals(right);

    public static bool operator !=(CurrencyAmount left, CurrencyAmount right) => !(left == right);
}
