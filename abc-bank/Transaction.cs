using System;

namespace abc_bank
{
   public class Transaction
   {
      public readonly double amount;
      public readonly DateTime transactionDate;

      public Transaction(double amount, DateTime? transactionDate = null)
      {
         if (transactionDate == null) transactionDate = DateProvider.getInstance().Now();
         this.amount = amount;
         this.transactionDate = transactionDate.GetValueOrDefault();
      }
   }
}