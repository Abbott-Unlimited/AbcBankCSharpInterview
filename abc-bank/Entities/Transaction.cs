using System;

namespace abc_bank.Entities
{
    public class Transaction
    {
        public readonly double amount;

        private readonly DateTime transactionDate;

        public Transaction(double amount)
        {
            this.amount = amount;
            this.transactionDate = DateTime.Now;
        }

        public double Amount { get; }
    }
}