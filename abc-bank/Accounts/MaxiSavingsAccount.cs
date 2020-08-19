namespace abc_bank.Accounts
{
    public class MaxiSavingsAccount : Account
    {
        public override string Name => "Maxi Savings Account";

        public override double InterestEarned
        {
            get
            {
                double amount = Balance;

                if (amount <= 1000.0)
                {
                    return amount * 0.02;
                }

                if (amount <= 2000.0)
                {
                    return 20.0 + (amount - 1000.0) * 0.05;
                }

                return 70.0 + (amount - 2000.0) * 0.1;
            }
        }
    }
}
