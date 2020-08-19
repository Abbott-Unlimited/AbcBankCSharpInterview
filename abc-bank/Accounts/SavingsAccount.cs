namespace abc_bank.Accounts
{
    public class SavingsAccount : Account
    {
        public override string Name => "Savings Account";

        public override double InterestEarned
        {
            get
            {
                double amount = Balance;

                return amount <= 1000.0
                    ? amount * 0.001
                    : 1.0 + (amount - 1000.0) * 0.002;
            }
        }
    }
}
