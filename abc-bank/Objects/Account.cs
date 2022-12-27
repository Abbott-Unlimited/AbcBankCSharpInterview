using AbcCompanyEstablishmentApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp
{
    public class Account
    {
        public readonly AccountType Type;
        public Guid AccountID;
        public DateTime CreateTimeStamp;
        public Customer Owner;
        public List<Transaction> Transactions;
        public decimal TotalInterestEarned = 0;

        private decimal accountAmount;

        public decimal AccountAmount
        {
            get { return accountAmount; }
            set { if (TransactionController.ValidateTransaction(this, value)) { accountAmount = value; } }
        }

        public Account(AccountType accountType, decimal creationAmount, Customer owner)
        {
            Type = accountType;
            AccountID = Guid.NewGuid();            
            AccountAmount = creationAmount;
            Owner = owner;
            CreateTimeStamp = DateProvider.GetInstance().Now();
        }
    }
}
