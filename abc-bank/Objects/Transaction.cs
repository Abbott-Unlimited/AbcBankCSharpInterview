using System;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp
{
    public class Transaction
    {
        public readonly Guid transactionID;
        public readonly Account accountChanged;
        public readonly decimal amount;
        public readonly DateTime transactionDate;
        public readonly TransactionType transactionType;

        public Transaction(decimal amount, Account accountChanged)
        {
            this.amount = amount;
            this.transactionDate = DateProvider.GetInstance().Now();
            this.transactionID = new Guid();
            this.accountChanged = accountChanged;
            this.transactionType = amount > 0 ? TransactionType.DEPOSIT : TransactionType.WITHDRAWAL;
        }
    }
}