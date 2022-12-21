using System;
using abc_bank.Utilities;

namespace abc_bank.Controllers
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
            statement += "\nTotal In All Accounts " + BankFunctions.ToDollars(total);
            return statement;
        }

        private static string StatementForAccount(Account account)
        {
            string workingstring = "";

            //Translate to pretty account type
            switch (account.Type)
            {
                case BankValues.AccountType.CHECKING:
                    workingstring += "Checking Account\n";
                    break;
                case BankValues.AccountType.SAVINGS:
                    workingstring += "Savings Account\n";
                    break;
                case BankValues.AccountType.MAXI_SAVINGS:
                    workingstring += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction transaction in account.Transactions)
            {
                workingstring += "  " + (transaction.amount < 0 ? "withdrawal" : "deposit") + " " + BankFunctions.ToDollars(transaction.amount) + "\n";
                total += transaction.amount;
            }
            workingstring += "Total " + BankFunctions.ToDollars(total);
            return workingstring;
        }

    }
}
