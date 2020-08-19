using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly double amount;
        public DateTime transactionDate;

        public Transaction(double amount) 
        {
            this.amount = amount;
            transactionDate = DateTime.Now;
        }
    }
}
