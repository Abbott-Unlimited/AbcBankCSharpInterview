using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        /// <summary>
        /// The customer's full name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The number of accounts the customer has.
        /// </summary>
        public int NumberOfAccounts
        {
            get {
                return accounts.Count;
            }
        }

        /// <summary>
        /// Accounts the customer owns.
        /// </summary>
        private List<Account> accounts;

        /// <summary>
        /// Creates a new Customer.
        /// </summary>
        /// <param name="name">The customer's full name.</param>
        public Customer(string name)
        {
            this.Name = name;
            this.accounts = new List<Account>();
        }

        /// <summary>
        /// Associates an account with this customer.
        /// </summary>
        /// <param name="account">The account to add.</param>
        /// <returns>The current Customer object.</returns>
        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        /// <summary>
        /// Calculates the total amount of interest earned across all the customer's accounts.
        /// </summary>
        /// <returns>Total interest earned.</returns>
        public decimal TotalInterestEarned() 
        {
            decimal total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        /// <summary>
        /// Generates a statement for this customer's accounts.
        /// </summary>
        /// <returns>string containing the statement text.</returns>
        public string GetStatement() 
        {
            string statement = null;
            statement = "Statement for " + Name + "\n";
            decimal total = 0.00m;
            foreach (Account a in accounts) 
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        /// <summary>
        /// Generates a statement for an individual account.
        /// </summary>
        /// <param name="a">The account for which to generate the statement.</param>
        /// <returns>string containing the statement text.</returns>
        private string statementForAccount(Account a) 
        {
            string s = "";

           //Translate to pretty account type
            switch(a.Type){
                case AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            decimal total = 0.00m;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        /// <summary>
        /// Formats a value as a dollar amount.
        /// </summary>
        /// <param name="d">The decimal amount.</param>
        /// <returns>string representation of the value as a dollar amount.</returns>
        private string ToDollars(decimal d)
        {
            return string.Format("${0:N2}", Math.Abs(d));
        }
    }
}
