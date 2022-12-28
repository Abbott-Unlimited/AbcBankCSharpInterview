using System;
using System.Security.Principal;
using AbcCompanyEstablishmentApp.Utilities;

namespace AbcCompanyEstablishmentApp.Controllers
{
    public static class StatementGenerator
    {
        public static string GetCustomerStatementByID(Customer customer)
        {
            string statement = "";
            statement = "Statement for " + customer.FullName + "\n";
            decimal total = 0.0M;
            foreach (Guid guid in customer.Accounts.Keys)
            {
                var account = AccountController.GetAccountByID(guid);
                statement += "\n" + StatementForAccount(account) + "\n";
                total += account.AccountAmount;
            }
            statement += "\nTotal In All Accounts " + total.ToString("C");
            return statement;
        }

        private static string StatementForAccount(Account account)
        {
            string workingString = "";

            workingString += AbcFunctions.GenerateAccountTypeString(account.AccountType);
            var total = TransactionController.TotalTransactionsForCustomer(account.Transactions);
            
            foreach (Transaction transaction in account.Transactions)
            {
                workingString += "  " + transaction.TransactionType + " " + transaction.AccountAmount.ToString("C") + "\n";
            }

            workingString += "Total " + total.ToString("C");
            return workingString;
        }



    }
}
