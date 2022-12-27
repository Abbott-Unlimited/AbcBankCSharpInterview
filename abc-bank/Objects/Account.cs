using AbcCompanyEstablishmentApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp
{
    public class Account
    {
        public readonly AccountType AccountType;
        public Guid AccountID;
        public DateTime CreateTimeStamp;
        public Customer Owner;
        public List<Transaction> Transactions = new List<Transaction>();
        public decimal TotalInterestEarned = 0;
        public decimal AccountAmount;
        

        public Account(AccountType accountType, decimal creationAmount, Customer owner)
        {
            AccountType = accountType;
            AccountID = Guid.NewGuid();            
            AccountAmount += creationAmount;
            Owner = owner;
            CreateTimeStamp = DateProvider.GetInstance().Now();
        }
    }
}
