using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double Amount;

        public readonly DateTime TransactionDate;

        public Transaction(double amount) 
        {
            Amount = amount;

            TransactionDate = DateProvider.getInstance().Now();
        }
    }
}