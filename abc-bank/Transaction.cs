using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly int ID;
        public readonly double Amount;
        public readonly DateTime Date;

        public Transaction(double amount) 
        {
            Amount = amount;
            Date = DateTime.Now;
        }
    }
}
