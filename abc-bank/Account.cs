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

        // Checking Account Rate
        private const double CheckingAccountFlatRate = 0.001;

        // Savings Account Rates
        private const double SavingsAccountFirstRate = 0.001;
        private const double SavingsAccountSecondRate = 0.002;

        // Maxi Savings Account Rates
        private const double OriginalMaxiFirstRate = 0.02;
        private const double OriginalMaxiSecondRate = 0.05;
        private const double OriginalMaxiThirdRate = 0.1;
        private const double MaxiSavingsNoWithdrawl10Days = 0.05;
        private const double MaxiSavingsWithdrawlPast10Days = 0.001;

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

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(accountType){
                case CHECKING:
                    return amount * CheckingAccountFlatRate;
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * SavingsAccountFirstRate;
                    else
                        return (SavingsAccountFirstRate * 1000) + ((amount - 1000) * SavingsAccountSecondRate);
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    //if (amount <= 1000)
                    //    return amount * OriginalMaxiFirstRate;
                    //if (amount <= 2000)
                    //    return 20 + (amount-1000) * OriginalMaxiSecondRate;
                    //return 70 + (amount-2000) * OriginalMaxiThirdRate;

                    if (MaxiDaysSinceLastWithdrawl(10) <= 10)
                        return amount * MaxiSavingsNoWithdrawl10Days;
                    else
                        return amount * MaxiSavingsWithdrawlPast10Days;
                default:
                    throw new Exception("Warning: Unknown account type. Interest not calculated.");
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

        public int MaxiDaysSinceLastWithdrawl(int maxDays = 10)
        {
            int? days = null;
            int checkDays = 0;

            foreach (Transaction t in transactions)
            {
                if (t.amount > maxDays) continue;

                checkDays = t.DaysSinceTransaction();
                if (checkDays < maxDays)
                    return checkDays;
                else if (days == null || days < checkDays)
                    days = checkDays;
            }
            return (int)(days == null ? 0 : days);
        }

        public int MaxiDaysSinceLastTransaction()
        {
            int? days = null;
            int checkDays = 0;

            foreach (Transaction t in transactions)
            {
                checkDays = t.DaysSinceTransaction();
                if (days == null || days < checkDays)
                    days = checkDays;
            }
            return (int)(days == null ? 0 : days);
        }

    }
}
