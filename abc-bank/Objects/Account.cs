using abc_bank.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using static abc_bank.Utilities.BankValues;

namespace abc_bank
{
    public class Account
    {
        public readonly AccountType Type;
        public Guid AccountID;
        public DateTime CreateTimeStamp;
        public string OwnerName;
        public List<Transaction> Transactions;
        public double TotalInterestEarned = 0;

        private double accountAmount;

        public double AccountAmount
        {
            get { return accountAmount; }
            set { if (TransactionController.ValidateTransaction(this, value)) { accountAmount = value; } }
        }

        public Account(int accountType, double creationAmount, string ownerName)
        {
            Type = (AccountType)accountType;
            AccountID = Guid.NewGuid();            
            AccountAmount = creationAmount;
            OwnerName = ownerName;
            CreateTimeStamp = DateProvider.GetInstance().Now();
        }
    }
}
