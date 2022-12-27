using System;
using System.Security.Principal;
using AbcCompanyEstablishmentApp.Utilities;

namespace AbcCompanyEstablishmentApp.Controllers
{
    public static class StatementGenerator
    {
        public static string GetCustomerStatementByID(Guid customerID)
        {
            var customer = CustomerController.GetCustomerByID(customerID);
            string statement = "";
            statement = "Statement for " + customer.FullName + "\n";
            decimal total = 0.0M;
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
            var total = TransactionController.TotalTransactionsForCustomer(account.Transactions);
            
            foreach (Transaction transaction in account.Transactions)
            {
                workingString += "  " + transaction.transactionType + " " + AbcFunctions.ToDollars(transaction.amount) + "\n";
            }

            workingString += "Total " + AbcFunctions.ToDollars(total);
            return workingString;
        }



    }
}
