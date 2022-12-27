using AbcCompanyEstablishmentApp.Controllers;
using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp
{
    public class Customer
    {
        public readonly Guid CustomerID;
        public readonly AccountType TypeOfAccount;
        public readonly DateTime CreateTimeStamp;

        public string FirstName;
        public string LastName;
        public string FullName;
        public string ContactNumber;
        public string ContactEmail;
        public List<AccountType> AccountTypes = new List<AccountType>();
        public Dictionary<Guid, AccountType> Accounts = new Dictionary<Guid, AccountType>();

        public Customer(AccountType accountType, decimal creationAmount, string firstName, string lastName)
        {
            CustomerID = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{firstName} {lastName}";
            CreateTimeStamp = DateProvider.GetInstance().Now();            
        }
    }
}
