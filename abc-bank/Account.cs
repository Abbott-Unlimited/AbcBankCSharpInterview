using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        public AccountType AccountType { get; }
        public List<Transaction> Transactions { get; }
        private DateTime _accountAge;

        public Account(AccountType accountType)
        {
            AccountType = accountType;
            Transactions = new List<Transaction>();

            _accountAge = DateTime.Now;
        }

        public TimeSpan GetAccountAge() =>
            DateTime.Now.Subtract(_accountAge);

        // For testing only.
        public void SetAccountAge(int days) => _accountAge = DateTime.Now.Subtract(TimeSpan.FromDays(days)).Date;

        public void Deposit(double amount, DateTime date)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            Transactions.Add(new Transaction(amount, TransactionType.Deposit, date));
        }

        public void Withdraw(double amount, DateTime date)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            Transactions.Add(new Transaction(-amount, TransactionType.Withdraw, date));
        }

        public double InterestEarned()
        {
            var amount = SumTransactions();
            const double savingsLow = 0.000027;
            const double savingsHigh = 0.000055;
            const double maxiSavings = 0.00014;
            const double checking = 0.000027;

            // Helper pure function to calculate more easily. Only used here so shouldn't be in outer scope.
            double CalcRate(double rate, double accountAmount, int days) => accountAmount * (rate * days);

            // Interest accrues over days.
            if (AccountType == AccountType.Savings)
            {
                if (amount <= 1000)
                {
                    return CalcRate(savingsLow, amount, GetAccountAge().Days);
                }

                return CalcRate(savingsLow, 1000, GetAccountAge().Days) +
                       CalcRate(savingsHigh, amount - 1000, GetAccountAge().Days);
            }

            if (AccountType == AccountType.MaxiSavings)
            {
                if (!Transactions.Exists(x =>
                    x.TransactionType == TransactionType.Withdraw &&
                    x.TransactionDate > DateTime.Now.Subtract(TimeSpan.FromDays(10))))
                {
                    return CalcRate(maxiSavings, amount, GetAccountAge().Days);
                }
            }

            return CalcRate(checking, amount, GetAccountAge().Days);
        }

        public double SumTransactions() => Transactions.Sum(x => x.Amount);

        // Customer can transfer between accounts, I'm assuming they mean money.
        public void Transfer(double amount, Account account)
        {
            // Transfer can only go one way, if you want to take money from the other account use the 
            // method on that account.
            if (amount > 0)
            {
                account.Deposit(amount, DateTime.Now);

                // Implicit this.
                Withdraw(amount, DateTime.Now);
            }
        }
    }
}