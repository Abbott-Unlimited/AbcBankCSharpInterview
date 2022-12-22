using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp
{
    public class Establishment
    {
        public readonly Guid EstablishmentID;        
        public readonly DateTime CreateTimeStamp;
        public readonly EstablishmentType Type;

        public string EstablishmentName;
        public string EstablishmentOwnerName;
        public string EstablishmentPhysicalAddress;
        public string EstablishmentContactNumber;        
        public List<AccountType> AccountTypes = new List<AccountType>();
        public Dictionary<Guid, AccountType> Accounts = new Dictionary<Guid, AccountType>();

        public Establishment(EstablishmentType establishmentType, string establishmentName, string establishmentOwnerName, 
            string establishmentPhysicalAddress, string establishmentContactNumber)
        {
            EstablishmentID = Guid.NewGuid();
            Type = establishmentType;
            EstablishmentName = establishmentName;
            EstablishmentOwnerName = establishmentOwnerName;
            EstablishmentPhysicalAddress = establishmentPhysicalAddress;
            EstablishmentContactNumber = establishmentContactNumber;
            CreateTimeStamp = DateProvider.GetInstance().Now();
        }
    }
}
