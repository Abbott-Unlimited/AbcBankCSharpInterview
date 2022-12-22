using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp.Controllers
{
    internal static class AccountController
    {
        public static List<Account> Accounts = new List<Account>();

        public static Guid AddAccount(AccountType accountType, string ownerName, double creationAmount)
        {
            var newAccount = new Account(
                accountType: accountType, 
                creationAmount: creationAmount, 
                ownerName: ownerName
                );

            Accounts.Add(newAccount);

            return newAccount.AccountID;
        }

        public static bool DeleteAccount(Guid accountID)
        {
            var account = Accounts.FirstOrDefault(x => x.AccountID == accountID);
            Accounts.Remove(account);
            return Accounts.Any(x => x.AccountID == accountID);
        }
        public static bool DeleteAccount(string ownerName)
        {
            var account = Accounts.FirstOrDefault(x => x.OwnerName == ownerName);
            Accounts.Remove(account);
            return Accounts.Any(x => x.OwnerName == ownerName);
        }

        public static Account GetAccountByID(Guid accountID)
        {
            return Accounts.FirstOrDefault(x => x.AccountID == accountID);
        }

        public static double GetAccountAmountStatus(Guid accountID)
        {
            return Accounts.FirstOrDefault(x => x.AccountID == accountID).AccountAmount;
        }
        public static double GetAccountAmountStatus(string ownerName)
        {
            return Accounts.FirstOrDefault(x => x.OwnerName== ownerName).AccountAmount;
        }

        public static Guid GetAccountIDByOwnerName(string ownerName)
        {
            return Accounts.FirstOrDefault(x => x.OwnerName == ownerName).AccountID;
        }

        public static string GetAccountOwnerNameByID(Guid guid)
        {
            return Accounts.FirstOrDefault(x => x.AccountID == guid).OwnerName;
        }
    }
}
