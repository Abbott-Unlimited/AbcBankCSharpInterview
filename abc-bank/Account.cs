using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Account
    {
        public const int Checking    = 0;
        public const int Savings     = 1;
        public const int MaxiSavings = 2;

        private readonly int               _accountType;
        public           List<Transaction> Transactions;

        public Account(int accountType)
        {
            _accountType  = accountType;
            Transactions = new List<Transaction>();
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            Transactions.Add(new Transaction(amount, DateTime.Now, Transaction.TransactionTypeEnum.Deposit));
        }

        public void Withdraw(double amount, DateTime transactionDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }

            Transactions.Add(new Transaction(-amount, transactionDate, Transaction.TransactionTypeEnum.Withdraw));
        }

        public double InterestEarned()
        {
            var amount = SumTransactions();
            switch (_accountType)
            {
                case Savings:
                    if (amount <= 1000)
                        return amount * 0.001;
                    return 1 + (amount - 1000) * 0.002;
                case MaxiSavings:
                    var transactionsPastTenDays = TransactionsPastTenDays(_accountType);
                    return transactionsPastTenDays ? amount * 0.01 : amount * 0.05;
                default:
                    return amount * 0.001;
            }
        }

        public double SumTransactions()
        {
            var totalDeposits = Transactions.Where(x => x.TransactionType == Transaction.TransactionTypeEnum.Deposit)
                .Sum(transaction => transaction.Amount);
            var totalWithdraws = Math.Abs(Transactions.Where(x => x.TransactionType == Transaction.TransactionTypeEnum.Withdraw)
                .Sum(transaction => transaction.Amount)); 
            var totalTransfers = Math.Abs(Transactions.Where(x => x.TransactionType == Transaction.TransactionTypeEnum.Transfer)
                .Sum(transaction => transaction.Amount));

            return totalDeposits - totalWithdraws - totalTransfers;
        }

        public static void TransferAmount(double amount, Account accountFrom, Account accountTo,
            DateTime                             transactionDate)
        {
            var availableBalance = accountFrom.Transactions.Sum(x => x.Amount);
            if (!(availableBalance > amount)) return;

            accountFrom.Withdraw(amount, transactionDate);
            accountTo.Deposit(amount);
        }

        public int GetAccountType()
        {
            return _accountType;
        }

        private bool TransactionsPastTenDays(int accountType)
        {
            return accountType == MaxiSavings && Transactions
                .Where(x => x.TransactionType == Transaction.TransactionTypeEnum.Withdraw).Any(transaction =>
                    transaction.TransactionDate < DateTime.Today.AddDays(10));
        }
    }
}