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

        //Savings rate
        private const double SavingFirstThoRate = 0.001;
        private const double SavingRemainingRate = 0.002;

        //Maxi Saving rates
        private const double MaxiSavingFirstThoRate = 0.02;
        private const double MaxiSavingSecondThoRate = 0.05;
        private const double MaxiSavingRemainingRate = 0.1;
        private const double MaxiSavingWithdrawal10DaysRate = 0.001;
        private const double MaxiSavingNoWithdrawalRate = 0.05;

        //Checking's flat rate
        private const double CheckingRate = 0.001;
        
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
            //I changed the various rates to constants in the top of the page, because I am way to used to those things changing.  I don't think that this will
            //   add siginificant CPU time and makes it more stable if someone tries to change the rate % over the years.
            //If for an unchanging or more complex item, I would leave the values hardcoded, but use polymorphism with each class as a different banking option.
            //I changed the switch's default value to a warning method, incase someone in the future tries to add an un-supported type,
            //   instead of it seeming to flow though properly.  With the Checking account type as its own dedicated case now.
            double amount = sumTransactions();
            switch(accountType){
                case SAVINGS:
                    if (amount <= 1000)
                        return amount * SavingFirstThoRate;
                    else
                        return (SavingFirstThoRate * 1000) + ((amount-1000) * SavingRemainingRate);
                case MAXI_SAVINGS:
                    if (MaxiDaysSinceWithdrawal(10)<=10)
                        return amount * MaxiSavingWithdrawal10DaysRate;                    
                    else
                        return amount * MaxiSavingNoWithdrawalRate;
                case CHECKING:
                    return amount * CheckingRate;
                default:
                    throw new Exception("Warning: Unknown account type. Interest unable to calculate.");
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
        public int MaxiDaysSinceWithdrawal(int outerLimit = 10)
        {
            int? days = null;
            int checkDays = 0;
            foreach (Transaction t in transactions)
            {
                if (t.amount > 0) continue;
                checkDays = t.DaysSinceTransaction();
                if (checkDays < outerLimit)
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

        public int GetAccountType() 
        {
            return accountType;
        }

    }
}
