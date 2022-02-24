using System;
using System.Threading;
public class BankAccount
{
    private bool _isOpen;
    private int _balance;
    private readonly object _obj = new();

    public void Open()
    {
        if(_isOpen) return;
        _isOpen = true;
        _balance = 0;
    }

    public void Close() => _isOpen = false;

    public decimal Balance => _isOpen ? _balance : throw new InvalidOperationException();

    public void UpdateBalance(decimal change)
    {
        Monitor.Enter(_obj);
        _balance += (int)change;
        Monitor.Exit(_obj);
    }
}
