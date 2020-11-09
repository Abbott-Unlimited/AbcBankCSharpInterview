namespace abc_bank
{
    public interface ICustomer
    {
        string GetName();
        int GetNumberOfAccounts();
        string GetStatement();
        Customer OpenAccount(Account account);
        double TotalInterestEarned();
    }
}