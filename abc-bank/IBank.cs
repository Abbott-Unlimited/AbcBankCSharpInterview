namespace abc_bank
{
    public interface IBank
    {
        void AddCustomer(Customer customer);
        string CustomerSummary();
        string GetFirstCustomer();
        double totalInterestPaid();
    }
}