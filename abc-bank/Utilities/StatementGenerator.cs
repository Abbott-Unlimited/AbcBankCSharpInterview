using System;
using System.Security.Principal;
using AbcCompanyEstablishmentApp.Utilities;

namespace AbcCompanyEstablishmentApp.Controllers
{
    internal static class StatementGenerator
    {
        public static string GetCustomerStatementByID(Guid customerID)
        {
            var customer = CustomerController.GetCustomerByID(customerID);
            string statement = "";
            statement = "Statement for " + customer.FullName + "\n";
            double total = 0.0;
            foreach (Guid guid in customer.Accounts.Keys)
            {
                var account = AccountController.GetAccountByID(guid);
                statement += "\n" + StatementForAccount(account) + "\n";
                total += account.AccountAmount;
            }
            statement += "\nTotal In All Accounts " + AbcFunctions.ToDollars(total);
            return statement;
        }

        private static string StatementForAccount(Account account)
        {
            string workingString = "";

            workingString += AbcFunctions.GenerateAccountTypeString(account.Type);
            var total = TransactionController.TotalTransactions(account.Transactions);
            
            foreach (Transaction transaction in account.Transactions)
            {
                workingString += "  " + transaction.transactionType + " " + AbcFunctions.ToDollars(transaction.amount) + "\n";
            }

            workingString += "Total " + AbcFunctions.ToDollars(total);
            return workingString;
        }



    }
}
