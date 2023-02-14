using abc_bank.InterestStrategies;
using System;
using System.Collections.Generic;

namespace abc_bank.Accounts
{
    class CheckingAccount : Account
    {
        public List<Transaction> transactions;

        private static double ReturnCleanValue(double amount)
        {
            return Math.Round(amount, 2);
        }

        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, TransactionType.DEPOSIT, DateTime.Now));
            }
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, TransactionType.WITHDRAWEL, DateTime.Now));
            }
        }

        public double InterestEarned()
        {
            InterestContext context = new InterestContext();
            context.SetInterestStrategy(new StandardInterest());

            return context.Calculate(transactions);
            
        }

        public double sumTransactions()
        {
            return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll)
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return ReturnCleanValue(amount);
        }

        public AccountTypes GetAccountType()
        {
            return AccountTypes.CHECKING;
        }
        public CheckingAccount()
        {
            if(transactions == null)
            {
                transactions = new List<Transaction>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double SumTransactions()
        {
            double total = 0.0;

            foreach(var t in transactions)
            {
                total += t.amount;
            }
            return ReturnCleanValue(total);
        }

        double Account.CheckIfTransactionsExist(bool checkAll)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }
    }
}
