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

        private List<int> validAccountTypes = new List<int>() { CHECKING, SAVINGS, MAXI_SAVINGS };

        private readonly int accountType;
        public List<Transaction> transactions;

        public Account(int accountType)
        {
            if (!validAccountTypes.Contains(accountType))
                throw new ArgumentException("invalid account type");

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
            double balance = 0;
            double interestAccrued = 0;

            // Assumption: Transaction list is sorted by transaction date, oldest to newest
            // Assumption: Interest accrues daily on balance at EOD
            // Assumption: No transactions = $0 interest
            // Assumption: Simple interest, no compounding

            if (transactions.Count < 1)
                return interestAccrued;

            DateTime currentDate = DateTime.MinValue;
            DateTime balanceDate = transactions[0].GetDate().Date;
            DateTime lastWithdrawal = DateTime.MinValue;

            for (int i = 0; i < transactions.Count; i++)
            {
                currentDate = transactions[i].GetDate().Date;

                // Accumulate balance for multiple transactions on same day
                if (currentDate == balanceDate)
                {
                    balance += transactions[i].amount;
                    if (transactions[i].amount < 0)
                        lastWithdrawal = currentDate;
                }

                if (currentDate > balanceDate)
                {
                    // Simple Interest
                    interestAccrued += (DailyInterestAccrued(accountType, balance, lastWithdrawal)) * ((currentDate - balanceDate).TotalDays + 1);
                    balance += transactions[i].amount;
                    balanceDate = currentDate;
                    if (transactions[i].amount < 0)
                        lastWithdrawal = currentDate;
                }
            }

            // Calculate interest from last set of transactions through yesterday (interest calculated at EOD)
            currentDate = DateTime.Now.Date;
            // Simple Interest
            interestAccrued += (DailyInterestAccrued(accountType, balance, lastWithdrawal)) * ((currentDate - balanceDate).TotalDays + 1);

            return interestAccrued;
        }

        public double DailyInterestAccrued(int accountType, double balance, DateTime lastWithDrawal)
        {
            double interestAccrued = 0;

            // This method returns interest accrued for ONE day
            // Assumption: 365 days per year, no accounting for leap years
            switch (accountType)
            {
                case (CHECKING):
                    interestAccrued = InterestAccrued_Checking(balance);
                    break;
                case (SAVINGS):
                    interestAccrued = InterestAccrued_Savings(balance);
                    break;
                case (MAXI_SAVINGS):
                    interestAccrued = InterestAccrued_MAXI(balance, lastWithDrawal);
                    break;
            }

            return (interestAccrued / 365);
        }

        public double InterestAccrued_Checking(double balance)
        {
            double interestAccrued = 0;

            // Assumption: No interest accrues on negative balance
            if (balance < 0)
                return interestAccrued;

            // Single tier for Checking account
            // Flat rate = .1%
            // Assumption: Yes, I'm using "magic" numbers instead of named constants - clear localized documentation reduces risk
            interestAccrued += balance * .001;

            return interestAccrued;
        }

        public double InterestAccrued_Savings(double balance)
        {
            double interestAccrued = 0;

            // Assumption: No interest accrues on negative balance
            if (balance < 0)
                return interestAccrued;

            // Two tiers for Savings account
            // .1% for portion of balance up to $1,000
            // .2% for portion  of balance over $1,000
            // Assumption: Yes, I'm using "magic" numbers instead of named constants - clear localized documentation reduces risk
            interestAccrued += Math.Min(balance, 1000) * .001;
            interestAccrued += Math.Max(balance - 1000, 0) * .002;

            return interestAccrued;
        }

        public double InterestAccrued_MAXI(double balance, DateTime lastWithdrawal)
        {
            double interestAccrued = 0;
            double daysSinceLastWithdrawal = (lastWithdrawal != DateTime.MinValue) ? (DateTime.Now - lastWithdrawal).TotalDays : double.MaxValue;

            // Assumption: No interest accrues on negative balance
            if (balance < 0)
                return 0;

            // Single tier for MAXI accounts, rate depends on last withdrawal
            // Flat rate = .1% if withdrawal in last 10 days
            // Flat rate = 5% if no withdrawal in last 10 days
            // Assumption: Yes, I'm using "magic" numbers instead of named constants - clear localized documentation reduces risk
            interestAccrued += balance * (daysSinceLastWithdrawal < 11 ? .001 : .05);

            return interestAccrued;
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
