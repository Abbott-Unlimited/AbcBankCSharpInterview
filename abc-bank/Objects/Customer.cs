using abc_bank.Controllers;
using abc_bank.Utilities;
using System;
using System.Collections.Generic;
using static abc_bank.Utilities.BankValues;

namespace abc_bank
{
    public class Customer
    {
        public readonly Guid CustomerID;
        public readonly AccountType TypeOfAccount;
        public readonly DateTime CreateTimeStamp;
        public readonly Guid AccountID;

        public string FirstName;
        public string LastName;
        public string FullName;
        public string ContactNumber;
        public string ContactEmail;
        public List<AccountType> AccountTypes = new List<AccountType>();
        public Dictionary<Guid, AccountType> Accounts = new Dictionary<Guid, AccountType>();

        public Customer(int accountType, double creationAmount, string firstName, string lastName)
        {
            AccountID = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            FullName = $"{firstName} {lastName}";
            CreateTimeStamp = DateProvider.GetInstance().Now();            
        }
    }
}
