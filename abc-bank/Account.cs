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

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Deposit(double amount, DateTime transDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(amount, transDate));
            }
        }

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public void Withdraw(double amount, DateTime transDate)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                transactions.Add(new Transaction(-amount, transDate));
            }
        }

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
                case MAXI_SAVINGS:
                    if (hasRecentWithdrawals(10))
                        return amount * 0.001;
                    else
                        return amount * 0.05;

                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CheckIfTransactionsExist(bool checkAll) 
        {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        /// <summary>
        /// Check if recent withdrawls exist
        /// </summary>
        /// <param name="days">Number of days to look back</param>
        /// <returns></returns>
        public bool hasRecentWithdrawals(int days)
        {
            if (days <= 0)
            {
                throw new ArgumentException("days must be greater than zero");
            }

            DateTime midnight = DateTime.Now.Date;

            var recentTransactions = from t in transactions
                                       where t.transactionDate > midnight.AddDays(-days)    
                                                && t.amount < 0
                                       select t;

            return recentTransactions.Count() > 0;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
