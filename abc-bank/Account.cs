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

        public void Deposit(double amount, DateTime? depositDate = null) 
        {
            if (amount <= 0) 
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else 
            {
               int tCount = transactions.Count;
               transactions.Add(new Transaction(amount));
               if (depositDate != null)
               {
                  transactions[tCount].transactionDate = (DateTime)depositDate;
               }
            }
        }

        public void Withdraw(double amount, DateTime? withdrawalDate = null) 
        {
            if (amount <= 0) {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
               int tCount = transactions.Count;
               transactions.Add(new Transaction(-amount));
               if (withdrawalDate != null)
               {
                  transactions[tCount].transactionDate = (DateTime)withdrawalDate;
               }
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
                        return 1 + ((amount-1000) * 0.002);
    //            case SUPER_SAVINGS:
    //                if (amount <= 4000)
    //                    return 20;
                case MAXI_SAVINGS:
                     int countOfDays = countCurrentYearWithdrawalDays();
                     double interestAmount = 0.0;
                     if (countOfDays == 0)
                     {
                        interestAmount = amount * 0.05;
                     }
                     else
                     {
                        double regularDailyRate = (0.05 / 365);
                        double withdrawalDailyRate = (0.001 / 365);
                        int numDaysInYear = DateTime.IsLeapYear(DateProvider.getInstance().Now().Year) ? 366 : 365;
                        interestAmount = ((numDaysInYear - countOfDays) * regularDailyRate * amount) +
                                          (countOfDays * withdrawalDailyRate * amount);
                     }
                     return interestAmount;
                default:
                    return amount * 0.001;
            }
        }

        public double sumTransactions()
      {
         double amount = 0.0;
         foreach (Transaction t in transactions)
            amount += t.amount;
         return amount;
      }

      private int countCurrentYearWithdrawalDays()
      {
         // assumption - checkAll true means all time, false means current calendar year...assumes we only care about current year for interest calcs

         int numDaysImpacted = 0;
         int currentYear = DateProvider.getInstance().Now().Year;
         bool isLeapyear = DateTime.IsLeapYear(currentYear);
         bool wasLeapyear = DateTime.IsLeapYear(currentYear - 1);
         int daysInCurrentYear = isLeapyear ? 366 : 365;
         int daysInLastYear = wasLeapyear ? 366 : 365;
         int daysTillEndOfYear = 0;
         foreach (Transaction t in transactions)
         {
            if (t.amount < 0)
            {
               if (t.transactionDate.Year == currentYear)
               {
                  daysTillEndOfYear = daysInCurrentYear - t.transactionDate.DayOfYear;
                  // do not penalise them more days than are left in the year - and we will catch it next year
                  numDaysImpacted = numDaysImpacted + (daysTillEndOfYear >= 10 ? 10 : daysTillEndOfYear);
               }
               else
               {
                  if (t.transactionDate.Year == currentYear - 1)
                  {
                     daysTillEndOfYear = daysInLastYear - t.transactionDate.DayOfYear;
                     if (daysTillEndOfYear < 10)
                     {
                        numDaysImpacted = numDaysImpacted + (10 - daysTillEndOfYear);
                     }
                  }
               }
            }
         }
         // assuming a separate ten day penalty for each withdrawal, even if they overlap, but not more than the number of days in the year
         if (numDaysImpacted > daysInCurrentYear)
            numDaysImpacted = daysInCurrentYear;

         return numDaysImpacted;
      }
         public int GetAccountType() 
        {
            return accountType;
        }

    }
}
