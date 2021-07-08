using System;
using abc_bank.Entities;
using System.Linq;
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
                case AccountTypeEnum.MAXI_SAVINGS:
                    return GetMaxiSavingsInterest(amount, account);
                default:
                    return amount * 0.001;
            }
        }

        private static double GetMaxiSavingsInterest(double amount, Account account)
        {
            var trans = account.Transactions.Where(a=> a.amount < 0).OrderByDescending(t => t.transactionDate).FirstOrDefault();

            if (trans != null && (DateTime.Now.Date - trans.transactionDate.Date).TotalDays <= 10 && trans.amount <= 0)
            {
                return amount * .001;
            }
            else
                return amount * .05;       
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
