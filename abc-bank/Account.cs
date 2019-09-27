using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public DateTime accountCreated = DateTime.Now;
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
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        public double InterestEarned(int ageOfAccount) 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount *(ageOfAccount * getDailyPeriodicRate(0.1));
                    else
                        return 1 + (amount-1000) * (ageOfAccount * getDailyPeriodicRate(0.2));
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    foreach(Transaction t in transactions)
                    {
                        if (t.amount > 0)
                            continue;
                        else if (t.amount < 0 && t.transactionDate <= DateTime.Now.AddDays(-10))
                            continue;
                        else
                            return amount * (ageOfAccount * getDailyPeriodicRate(0.1));
                    }
                    return amount * (ageOfAccount * getDailyPeriodicRate(5));
                default:
                    return amount * (ageOfAccount * getDailyPeriodicRate(0.1));
            }
        }
        private double getDailyPeriodicRate(double APR)
        {
            const int yearInDays = 365;
            return (APR / yearInDays) / 100;
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
