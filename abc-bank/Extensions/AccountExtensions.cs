namespace abc_bank.Extensions
{
    public static class AccountExtensions
    {
        public static void TransferTo(this Account account, Account destinationAccount, double amount)
        {
            account.Withdraw(amount);
            destinationAccount.Deposit(amount);
        }
    }
}
