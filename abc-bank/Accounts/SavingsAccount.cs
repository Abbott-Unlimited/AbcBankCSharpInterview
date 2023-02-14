using abc_bank.InterestStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    class SavingsAccount : Account
    {
        public List<Transaction> transactions;

        public SavingsAccount()
        {
            if (transactions == null)
            {
                transactions = new List<Transaction>();
            }
        }

        private static double ReturnCleanValue(double amount)
        {
            return Math.Round(amount, 2);
        }

        public double CheckIfTransactionsExist(bool checkAll)
        {
            throw new NotImplementedException();
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

        public AccountTypes GetAccountType()
        {
            return AccountTypes.SAVINGS;
        }

        public List<Transaction> GetTransactions()
        {
            return transactions;
        }

        public double InterestEarned()
        {
            InterestContext context = new InterestContext();
            context.SetInterestStrategy(new SavingsInterest());

            return context.Calculate(transactions);
        }

        public double SumTransactions()
        {
            double total = 0.0;

            foreach (var t in transactions)
            {
                total += t.amount;
            }
            return ReturnCleanValue(total);
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
    }
}
