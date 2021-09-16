static class SavingsAccount
{
    public static float InterestRate(decimal balance) =>
        balance < 0 ? -3.213f : balance < 1000 ? 0.5f : balance < 5000 ? 1.621f : 2.475f;

    public static decimal AnnualBalanceUpdate(decimal balance) =>
        InterestRate(balance) > 0 ? (decimal)(1f + (InterestRate(balance) * 0.01f)) * balance
            : -(1.03213m * -balance);

    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        var years = 0;
        while (balance < targetBalance)
        {
            balance = AnnualBalanceUpdate(balance);
            years++;
        }

        return years;
    }
}
