using System;

namespace abc_bank
{
    public class Transaction
    {
        public double Amount { get; }
        public DateTime Date { get; }

        public Transaction(double amount, DateTime? date = null)
        {
            Amount = amount;
            Date = date ?? DateTime.Now;
        }
    }
}
