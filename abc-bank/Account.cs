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

        public void Withdraw(double amount) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            } else if (amount > sumTransactions()) {
                throw new ArgumentException("insufficient funds");
            } else {
                transactions.Add(new Transaction(-amount));
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
                    if (amount <= 1000)
                        return amount * 0.02;
                    if (amount <= 2000)
                        return 20 + (amount-1000) * 0.05;
                    return 70 + (amount-2000) * 0.1;
                default:
                    return amount * 0.001;
            }
        }

        public double DailyInterest()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return (0.01 / 365.0) * amount;
                    else
                        return (0.02 / 365.0) * amount;
                case MAXI_SAVINGS:
                    if (transactions.Any(t => t.amount < 0.0 && (t.transactionDate - DateTime.Now).TotalDays < 10))
                        return (0.01 / 365.0) * amount;
                    else
                        return (0.05 / 365.0) * amount;
                default:
                    return (0.01 / 365.0) * amount;
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

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
