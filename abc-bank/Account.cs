using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        public const int CHECKING = 0;
        public const int SAVINGS = 1;
        public const int MAXI_SAVINGS = 2;

        private readonly int accountType;

        public List<Transaction> Transactions { get; set; }

        public Account(int accountType)
        {
            this.accountType = accountType;

            this.Transactions = new List<Transaction>();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0) throw new ArgumentException("amount must be greater than zero");

            Transactions.Add(new Transaction(amount));
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0) throw new ArgumentException("amount must be greater than zero");

            Transactions.Add(new Transaction(-amount));
        }

        public double InterestEarned()
        {
            double amount = SumTransactions();

            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    return 1 + (amount - 1000) * 0.002;
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount - 1000) * 0.05;
                    return 70 + (amount - 2000) * 0.10;
                default:
                    return amount * 0.001;
            }
        }

        public double SumTransactions()
        {
            return Transactions.Sum(x => x.amount);
        }

        public int GetAccountType()
        {
            return accountType;
        }
    }
}