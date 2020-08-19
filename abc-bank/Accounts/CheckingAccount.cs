namespace abc_bank.Accounts
{
    public class CheckingAccount : Account
    {
        public override string Name => "Checking Account";

        public override double InterestEarned => Balance * 0.001;
    }
}
