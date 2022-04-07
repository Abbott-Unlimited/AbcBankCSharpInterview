using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
   public class Account
   {
      public enum AccountTypes
      {
         CHECKING = 0,
         SAVINGS = 1,
         MAXI_SAVINGS = 2
      }
      //public const int CHECKING = 0;
      //public const int SAVINGS = 1;
      //public const int MAXI_SAVINGS = 2;

      private readonly AccountTypes accountType;
      public List<Transaction> transactions;

      public Account(AccountTypes accountType)
      {
         this.accountType = accountType;
         this.transactions = new List<Transaction>();
      }

      public void Deposit(double amount)
      {
         if (amount <= 0)
         {
            throw new ArgumentException("amount must be greater than zero");
         }
         else
         {
            transactions.Add(new Transaction(amount));
         }
      }

      public void Withdraw(double amount)
      {
         double totalAvailable = this.sumTransactions();
         if (amount <= 0)
         {
            throw new ArgumentException("amount must be greater than zero");
         }
         else if (amount > totalAvailable)
         {
            throw new ArgumentException($"Insufficient funds in {this.accountType} account");
         }
         else
         {
            transactions.Add(new Transaction(-amount));
         }
      }

      /// <summary>
      /// Calculate the current interest earned based on the account transactions. Interest is accrued daily
      /// and each day's is added a new transction to the account. 
      /// </summary>
      /// <returns></returns>
      public double InterestEarned()
      {
         double amount = sumTransactions();
         double accruedInterestEarned = 0.0;
         double accountBalance = 0.0;
         Transaction firstTransaction = transactions.OrderBy(t => t.transactionDate).FirstOrDefault();
         if (firstTransaction != null)
         {
            DateTime startDate = firstTransaction.transactionDate;
            DateTime calcDate = startDate;
            double calcDayInterest = 0.0;

            while (calcDate <= DateProvider.getInstance().Now())
            {
               int daysInYear = DateTime.IsLeapYear(calcDate.Year) ? 366 : 365;
               accountBalance = transactions.Where(t => t.transactionDate <= calcDate).Sum(t => t.amount);
               calcDayInterest = CalculateCompoundingPeriodInterest(accountBalance, daysInYear);
               transactions.Add(new Transaction(calcDayInterest, calcDate));
               accruedInterestEarned += calcDayInterest;
               calcDate = calcDate.AddDays(1);
            }
         }
         return accruedInterestEarned;
      }

      public double CalculateCompoundingPeriodInterest(double accountBalance, int compoundingPeriodsPerYear)
      {
         double calcAmount;

         switch (accountType)
         {
            case AccountTypes.SAVINGS:
               if (accountBalance <= 1000)
                  calcAmount = accountBalance * 0.001;
               else
                  //The first $1000 is calculated at 0.001 (or 0.1%)
                  calcAmount = 1 + (accountBalance - 1000) * 0.002;
               break;
            // case SUPER_SAVINGS: if (amount <= 4000) return 20;
            case AccountTypes.MAXI_SAVINGS:

               if (transactions.Any(t => t.amount < 0 && t.transactionDate > DateProvider.getInstance().Now().AddDays(-10)))
               {
                  calcAmount = accountBalance * 0.001;
               }
               else
               {
                  calcAmount = accountBalance * 0.05;
               }
               break;
            //if (amount <= 1000)
            //   return amount * 0.02;
            //if (amount <= 2000)
            //   return 20 + (amount - 1000) * 0.05;
            //return 70 + (amount - 2000) * 0.1;

            default:
               calcAmount = accountBalance * 0.001;
               break;
         }
         //CaclAmount is per compounding period, return the period in the year.
         return calcAmount / compoundingPeriodsPerYear;
      }

      public double sumTransactions()
      {
         return this.transactions.Any() ? this.transactions.Sum(a => a.amount) : 0.0;
      }

      //private double CheckIfTransactionsExist(bool checkAll)
      //{
      //   double amount = 0.0;
      //   foreach (Transaction t in transactions)
      //      amount += t.amount;
      //   return amount;
      //}

      public AccountTypes GetAccountType()
      {
         return accountType;
      }
   }
}