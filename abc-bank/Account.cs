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
            } else {
                transactions.Add(new Transaction(-amount));
            }
        }

        //added by Robert Murrell
        public void Transfer(double amount, Account name)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                Withdraw(amount);
                name.Deposit(amount);
            }
        }

        public double InterestEarned() 
        {
            /*
             * altered method to calculate interest on a daily rate (interest rate / 365), 
             * multiplied by the number of days the account has been active,
             * as determined by the earliest available transaction date
             * 
             * Robert Murrell
             */

            double amount = sumTransactions();
            int accountDays = DaysAccountActive();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * DailyRate(0.001) * accountDays;
                    else
                        return 1 + (amount-1000) * DailyRate(0.002) * accountDays;
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    //changed by Robert Murrell

                    return amount * DailyRate((NoTransactionsforTenDays() ? .05 : .001)) * accountDays;

                    //if (amount <= 1000)
                    //    return amount * 0.02;
                    //if (amount <= 2000)
                    //    return 20 + (amount-1000) * 0.05;
                    //return 70 + (amount-2000) * 0.1;
                default:
                    return amount * DailyRate(0.001) * accountDays;
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

        //added by Robert Murrell
        private double DailyRate(double rate)
        {
            return rate / 365;
        }

        //adde by Robert Murrell
        public int DaysAccountActive()
        {
            DateTime today = DateTime.Now.AddDays(30); //DateTime.Now;
            DateTime start = (from t in transactions
                              select t.transactionDate).Min();

            //days calculation returns negative number; multiply by -1 to convert positive
            return (start - today).Days * -1;
        }

        //added by Robert Murrell
        private bool NoTransactionsforTenDays()
        {
            DateTime today = DateTime.Now.AddDays(11); //DateTime.Now;

            //count transactions occurring in the last ten days from runtime
            int tendays = (from t in transactions
                           where t.transactionDate > today.AddDays(-10)
                           select t).Count();

            return tendays == 0;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
