using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    /// <summary>
    ///   Customer class
    /// </summary>
    public class Customer
    {
        private String name;
        private List<Account> accounts;

        /// <summary>Initializes a new instance of the <see cref="Customer" /> class.</summary>
        /// <param name="name">The name.</param>
        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        /// <summary>Gets the name.</summary>
        /// <returns>
        ///   returns the name of the customer.
        /// </returns>
        public String GetName()
        {
            return name;
        }

        /// <summary>
        /// Opens the account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        /// <summary>
        /// Gets the number of accounts.
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        /// <summary>
        /// Totals the interest earned.
        /// </summary>
        /// <returns></returns>
        public double TotalInterestEarned()
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        /// <summary>
        /// Gets the statement.
        /// </summary>
        /// <returns></returns>
        public String GetStatement()
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        /// <summary>
        /// Statements for account.
        /// </summary>
        /// <param name="a">a.</param>
        /// <returns></returns>
        private String statementForAccount(Account a)
        {
            String s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        /// <summary>
        /// Converts to dollars.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <returns></returns>
        private String ToDollars(double d)
        {
            //Following commented code is invalid for formatting.  Corrected version is below.
            //return String.Format("$%,.2f", Math.Abs(d));

            return Math.Abs(d).ToString("C2", CultureInfo.CurrentCulture);
        }
    }
}
