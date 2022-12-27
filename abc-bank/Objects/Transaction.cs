using AbcCompanyEstablishmentApp.Controllers;
using System;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;
using AbcCompanyEstablishmentApp.Utilities;

namespace AbcCompanyEstablishmentApp
{
    public class Transaction
    {
        public readonly Guid TransactionID;
        public readonly Account AccountChanged;
        public readonly DateTime TransactionDate;
        public readonly TransactionType TransactionType;

        private decimal accountAmount;

        public decimal AccountAmount
        {
            get { return accountAmount; }            
        }

        public Transaction(decimal amount, Account AccountChanged)
        {
            this.accountAmount = amount;
            this.TransactionDate = DateProvider.GetInstance().Now();
            this.TransactionID = new Guid();
            this.AccountChanged = AccountChanged;
            this.TransactionType = amount > 0 ? TransactionType.DEPOSIT : TransactionType.WITHDRAWAL;
        }

        private void ValidateAndSetAccountAmount(decimal amount)
        {
            //eventually set up customer to have overdraft protection or whatever
            //check if customer has overdraft here
            
            var accountAmount = AccountController.GetAccountAmount(AccountChanged.AccountID);
            if (accountAmount < 0 && amount < 0)
            {
                return;
            }

            AccountChanged.AccountAmount += amount;
        }
    }
}