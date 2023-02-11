using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.CustomerServices
{
    public class GetCustomerStatementService
    {
        public string Get(Customer customer)
        {
            if (customer.GetAccounts().Count == 0)
                return $"Customer {customer.GetName()} has no accounts.";

            string statement = $"Statement for {customer.GetName()}\n";
            decimal total = 0;

            foreach (var account in customer.GetAccounts())
            {
                statement += GetStatementForAccount(account);
                total += account.transactions.Sum(x => x.amount);
            }

            statement += $"\nTotal In All Accounts {total.ToString("c")}";
            return statement;
        }

        private string GetStatementForAccount(Account account)
        {
            // Get the Account Type
            string statement = account.GetAccountType() == AccountType.CHECKING ? $"Checking Account\n" :
                account.GetAccountType() == AccountType.SAVINGS ? "Savings Account\n" :
                    "Maxi Savings Account\n";

            // Get the transactions
            decimal total = 0;
            foreach (var transaction in account.transactions)
            {
                statement += $"   {(transaction.amount < 0 ? "withdrawal" : "deposit")} {transaction.amount.ToString("c")}\n";
                total += transaction.amount;
            }
            statement += $"Total {total.ToString("c")}";

            return statement;
        }
    }
}
