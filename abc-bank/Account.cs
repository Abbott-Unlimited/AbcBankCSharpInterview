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
        private DateTime lastInterestDate { get; set; }

        public Account(int accountType) 
        {
            this.accountType = accountType;
            this.transactions = new List<Transaction>();
            this.lastInterestDate = DateTime.Now;
        }

        public void Deposit(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
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

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            int daysSinceLastInterest = (DateTime.Now - lastInterestDate).Days; // assuming lastInterestDate is a DateTime property that stores the date when the last interest was credited to the account
            double dailyInterestRate = 0.0;

            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        dailyInterestRate = 0.001 / 365;
                    else
                        dailyInterestRate = 0.002 / 365;
                    break;
                case MAXI_SAVINGS:
                    if (amount <= 1000)
                        dailyInterestRate = 0.02 / 365;
                    else if (amount <= 2000)
                        dailyInterestRate = 0.05 / 365;
                    else
                        dailyInterestRate = 0.1 / 365;
                    break;
                default:
                    dailyInterestRate = 0.001 / 365;
                    break;
            }

            double dailyInterest = amount * dailyInterestRate;
            double interestEarned = dailyInterest * daysSinceLastInterest;

            return interestEarned;
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

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
