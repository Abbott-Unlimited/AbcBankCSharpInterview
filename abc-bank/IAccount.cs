namespace abc_bank
{
    public interface IAccount
    {
        void Deposit(double amount);
        int GetAccountType();
        double InterestEarned();
        double sumTransactions();
        void Withdraw(double amount);
    }
}