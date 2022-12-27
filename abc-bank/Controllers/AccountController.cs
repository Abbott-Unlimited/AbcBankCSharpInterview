using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp.Controllers
{
    public static class AccountController
    {
        public static List<Account> Accounts = new List<Account>();

        public static decimal GetAccountAmount(Account account)
        {
            decimal returnValue = (AccountController.GetAccountByID(account.AccountID).AccountAmount);


            return returnValue;
        }

        public static Guid AddAccount(AccountType accountType, Customer owner, decimal creationAmount)
        {
            var newAccount = new Account(
                accountType: accountType, 
                creationAmount: creationAmount, 
                owner: owner
                );

            owner.Accounts.Add(newAccount.AccountID, accountType);
            Accounts.Add(newAccount);

            return newAccount.AccountID;
        }

        public static bool DeleteAccount(Guid accountID)
        {
            var account = Accounts.FirstOrDefault(x => x.AccountID == accountID);
            Accounts.Remove(account);
            return Accounts.Any(x => x.AccountID == accountID);
        }
        public static bool DeleteAccount(Customer ownerName)
        {
            var account = Accounts.FirstOrDefault(x => x.Owner == ownerName);
            Accounts.Remove(account);
            return Accounts.Any(x => x.Owner == ownerName);
        }
        public static Account GetAccountByID(Guid accountID)
        {
            return Accounts.FirstOrDefault(x => x.AccountID == accountID);
        }
        public static decimal GetAccountAmount(Guid accountID)
        {
            return Accounts.FirstOrDefault(x => x.AccountID == accountID).AccountAmount;
        }     
        public static Guid GetAccountIDByCustomer(Customer owner)
        {
            return Accounts.FirstOrDefault(x => x.Owner == owner).AccountID;
        }
        public static Customer GetCustomerByID(Guid guid)
        {
            return Accounts.FirstOrDefault(x => x.AccountID == guid).Owner;
        }

        public static decimal GetAccountAmountByAccountType(AccountType accountType, Customer owner)
        {
            return Accounts.FirstOrDefault(x => x.Owner.CustomerID == owner.CustomerID && x.AccountType == accountType).AccountAmount;
        }

        public static decimal GetTotalForAllAccounts(Customer owner)
        {
            var savingsAmount = AccountController.GetAccountAmountByAccountType(AccountType.SAVINGS, owner);
            var checkingAmount = AccountController.GetAccountAmountByAccountType(AccountType.CHECKING, owner);
            return savingsAmount + checkingAmount;
        }

    }
}
