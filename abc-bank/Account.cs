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
                /*
                 * Before adding the latest deposit, 
                 * we can calculate the interest and add it for daily accrual
                 * something like:
                 * 
                 * DepositDailyInterest();
                 * 
                 * Implemented this below
                 */
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
                case MAXI_SAVINGS:
                    if (HasWithdrawalInLastTenDays())
                        return amount * 0.01;
                    else
                        return amount * 0.05;
                default:
                    return amount * 0.001;
            }
        }

        // This is similar to the above, however, this only helps us determine the daily APR
        public double CalculateInterestRate()
        {
            double amount = sumTransactions();
            switch (accountType)
            {
                case SAVINGS:
                    if (amount <= 1000)
                        return 0.001;
                    else
                        return 0.002;
                case MAXI_SAVINGS:
                    if (HasWithdrawalInLastTenDays())
                        return 0.01;
                    else
                        return 0.05;
                default:
                    return 0.001;
            }
        }

        /*The Assumption here is that we calculate interest on a daily basis 
         * or whenever a deposit is made
         * will add the accrued interest so dar to the account
         */
        public void DepositDailyInterest(List<Transaction> transactionList)
        {
            DateTime today = DateTime.Now;
            double dailyAccrualRate = 0; // We will calculate it ahead
            double totalInterest = 0;
            Transaction lastTransaction = transactionList.LastOrDefault(t => t.transactionDate < today);

            if (lastTransaction != null)
            {
                double daysSinceLastTransaction = (today - lastTransaction.transactionDate).TotalDays;
                double totalDepositsTillNow = sumTransactions();

                dailyAccrualRate = CalculateInterestRate()/365; // This tells us our daily interest rate
                if (daysSinceLastTransaction > 0)
                {
                    totalInterest = daysSinceLastTransaction * dailyAccrualRate * totalDepositsTillNow;
                    transactions.Add(new Transaction(totalInterest)); ; // This will deposit the interest till now
                }
            }
        }


        public double sumTransactions() {
            double amount = 0.0;
            foreach (Transaction t in transactions)
                amount += t.amount;
            return amount;
        }

        public int GetAccountType() 
        {
            return accountType;
        }

        /*This method checks if any money was withdrawn in the last ten days*/
        public bool HasWithdrawalInLastTenDays()
        {
            return transactions.Any(t => t.transactionDate >= DateProvider.getInstance().Now().AddDays(-10) && t.amount < 0);
        }


    }
}
