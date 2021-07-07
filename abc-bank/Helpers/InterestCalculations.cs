using abc_bank.Entities;
using abc_bank.Enums;

namespace abc_bank.Helpers
{
    public static class InterestCalculations
    {
        public static double TotalInterestEarned(Customer customer)
        {
            double total = 0;
            foreach (Account a in customer.Accounts)
                total += InterestEarned(a);
            return total;
        }

        public static double InterestEarned(Account account)
        {
            double amount = SumTransactions(account);
            switch (account.AccountType)
            {
                case AccountTypeEnum.SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount - 1000) * 0.002;
                //            case SUPER_SAVINGS:
                //                if (amount <= 4000)
                //                    return 20;
                case AccountTypeEnum.MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount - 1000) * 0.05;
                    return 70 + (amount - 2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public static double SumTransactions(Account account)
        {
            double amount = 0.0;
            foreach (Transaction t in account.Transactions)
                amount += t.amount;
            return amount;
        }
    }
}
