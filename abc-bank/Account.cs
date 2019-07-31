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

        public const double CHECKING_INTEREST_RATE = 0.001;

        public const double FIRST_THOUSAND_SAVINGS_RATE = 0.001;
        public const double OVER_THOUSAND_SAVINGS_RATE = 0.002;

        public const double MAXI_SAVINGS_OVER_TEN_DAY_INTEREST_RATE = 0.05;
        public const double MAXI_SAVINGS_UNDER_TEN_DAY_INTEREST_RATE = 0.001;

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
                    return CalculateSavingsInterest(amount);
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                    return CalculateMaxiSavingsInterest(amount);
                default:
                    return DefaultInterest(amount);
            }
        }

        public double sumTransactions() {
           return CheckIfTransactionsExist(true);
        }

        private double CalculateSavingsInterest(double amount)
        {
            double calculatedInterest = 0.0;

            if (amount <= 1000)
                calculatedInterest = amount * FIRST_THOUSAND_SAVINGS_RATE;
            else
                calculatedInterest = 1000 * FIRST_THOUSAND_SAVINGS_RATE + (amount - 1000) * OVER_THOUSAND_SAVINGS_RATE;

            return calculatedInterest;
        }

        private double CalculateMaxiSavingsInterest(double amount)
        {
            // For brevity's sake, we're assuming that all transactions occur in the same timezone.
            // Ideally, everything would have to be converted to UTC prior to attempting such an operation.
            // Also, ideally we would need to be mindful of MINDATE and MAXDATE.
            DateTime tenDayPriorDate = DateTime.Now.AddDays(-10);

            bool hasTransactionOccurredInTenDays = transactions.Where(currentTransaction => currentTransaction.transactionDate >= tenDayPriorDate).Count() > 0;

            return amount * (hasTransactionOccurredInTenDays? MAXI_SAVINGS_UNDER_TEN_DAY_INTEREST_RATE : MAXI_SAVINGS_OVER_TEN_DAY_INTEREST_RATE);
        }

        private double DefaultInterest(double amount)
        {
            return amount * CHECKING_INTEREST_RATE;
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
