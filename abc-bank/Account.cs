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
        public int accountNumber;
        public decimal totalInterest = 0;
        public decimal todayInterest = 0;
        public List<Transaction> transactions;
        public Account(int accountType, int accountNumber = 0) 
        {
            this.accountType = accountType;
            this.accountNumber = accountNumber;
            this.transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(amount));
            }
        }

        public void Withdraw(decimal amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public decimal InterestEarned() 
        {
                    return totalInterest;
            
        }

        public decimal AddDailyInterest()
        {
            todayInterest = 0;
            decimal balance = (sumTransactions());
            bool maxiprimerate = true;

            if (accountType == SAVINGS)
            {
                if (balance <= 1000)
                    todayInterest = balance * (0.001m / 365);
           
                else
                {
                    todayInterest = 1000 * (0.001m / 365);
                    todayInterest += (balance - 1000) * (0.002m / 365);
                }
            }
            else if (accountType == MAXI_SAVINGS)
            {
                foreach (Transaction t in transactions)
                {
                   if ((t.transactionDate - DateTime.Now).TotalDays < 5 && t.amount < 0)
                        maxiprimerate = false;
                }

                if (maxiprimerate)
                    todayInterest = balance * (0.05m / 365);
                else
                    todayInterest = balance * (0.001m / 365);
            }
            else
            {
                todayInterest = balance * (0.001m / 365);
            }
            if (todayInterest < 0)
                todayInterest = 0;
            totalInterest += todayInterest;
            return todayInterest;
        }


        public decimal sumTransactions() {
           return CheckIfTransactionsExist();
        }

        private decimal CheckIfTransactionsExist() 
        {
            decimal amount = 0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount + totalInterest;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
