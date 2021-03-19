using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Account
    {
        public static int MaxiSavingsMinIntrestWindow_Days = 10;
        public static TimeSpan MaxiSavingsLowInterestWindow = TimeSpan.FromDays(MaxiSavingsMinIntrestWindow_Days);

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

        public double InterestEarned() 
        {
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * 0.001;
                    else
                        return 1 + (amount-1000) * 0.002;
                //case SUPER_SAVINGS:
                //    if (amount <= 4000)
                //        return 20;
                case MAXI_SAVINGS:
                    //if (amount <= 1000)
                    //    return amount * 0.02;
                    //if (amount <= 2000)
                    //    return 20 + (amount-1000) * 0.05;
                    //return 70 + (amount-2000) * 0.1;
                    return amount * GetMaxiSavingsInterestRate();
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

        public int GetAccountType() 
        {
            return accountType;
        }

        private DateTime? GetLastWithdrawlDate()
        {
            if (transactions.Count == 0)
                throw new BankAppException("There are no transactions");

            Transaction lastWithdrawl = transactions.Where(
                transaction => transaction.amount < 0.0 && transaction.TransactionDate == transactions.Max(
                    txMax => txMax.TransactionDate)).FirstOrDefault();

            if (lastWithdrawl == null)
                return null;
            else
                return lastWithdrawl.TransactionDate;
        }

        private double GetMaxiSavingsInterestRate()
        {
            DateTime cutoff = DateTime.Now.Subtract(Account.MaxiSavingsLowInterestWindow);

            DateTime? lastWithdrawlDate = GetLastWithdrawlDate();

            if (lastWithdrawlDate == null || lastWithdrawlDate < cutoff)
            {
                return 0.05;
            }
            else
            {
                return 0.001;
            }
        }

        public void AccureDailyInterest()
        {
            double accountBalance = sumTransactions();
            double dailyInterestRate = 0.0;
            double accruedInterest;

            // no interest for negative balances
            if (accountBalance <= 0.0)
                return;
            
            switch (accountType)
            {
                case CHECKING:
                    dailyInterestRate = 0.001 / 365;
                    break;
                case SAVINGS:
                    if (accountBalance <= 1000)
                        dailyInterestRate = 0.001 / 365;
                    else
                        dailyInterestRate = 0.002 / 365;
                    break;
                case MAXI_SAVINGS:
                    dailyInterestRate = GetMaxiSavingsInterestRate() / 365;
                    break;
            }

            accruedInterest = accountBalance * dailyInterestRate;

            Deposit(accruedInterest);
        }
    }
}
