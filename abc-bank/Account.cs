using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType)
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            transactions.Add(new Transaction(amount));
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            transactions.Add(new Transaction(-amount));
        }

        public decimal InterestEarned()
        {
            decimal amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000m)
                        return amount * 0.001m;
                    return 1 + (amount - 1000m) * 0.002m;
                case MAXI_SAVINGS:
                    if (!withdrawlWithin10Days())
                        return amount * 0.05m;
                    return amount * 0.001m;
                default:
                    return amount * 0.001m;
            }
        }

        public decimal sumTransactions()
        {
            decimal amount = 0.0m;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType()
        {
            return accountType;
        }

        private bool withdrawlWithin10Days()
        {
            return transactions.Exists(transaction =>
                transaction.amount < 0.0m &&
                transaction.transactionDate > DateTime.Now - TimeSpan.FromDays(10));
        }
    }
}