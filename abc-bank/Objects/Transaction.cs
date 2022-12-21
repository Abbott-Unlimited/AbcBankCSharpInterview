using System;

namespace abc_bank
{
    public class Transaction
    {
        public readonly Guid transactionID;
        public readonly Account accountChanged;
        public readonly double amount;
        public readonly DateTime transactionDate;

        public Transaction(double amount, Account accountChanged)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
            this.transactionID = new Guid();
            this.accountChanged = accountChanged;
        }
    }
}