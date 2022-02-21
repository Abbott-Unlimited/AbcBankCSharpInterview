using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Accounts;

namespace abc_bank.Customers
{
    /// <summary>
    /// An abstraction of a Bank Customer.
    /// </summary>
    public class Customer
    {
        private readonly String name;
        private readonly List<Account> accounts;

        /// <summary>
        /// Returns an instance of Customer.
        /// </summary>
        /// <param name="name">String containing the name of the customer. Can't be null or empty.</param>
        /// <exception cref="ArgumentException">Can't be null or empty.</exception>
        public Customer(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null or empty.");
            }

            this.name = name;
            this.accounts = new List<Account>();
        }

        /// <summary>
        /// Returns the name of the current instance.
        /// </summary>
        /// <returns>A string containing the customer name.</returns>
        public String GetName()
        {
            return name;
        }

        /// <summary>
        /// Opens an account for the calling Customer instance.
        /// </summary>
        /// <param name="account">An Account of Account.AccountType.</param>
        /// <returns>The calling Customer instance.</returns>
        public Customer OpenAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account cannot be null");
            }

            this.accounts.Add(account);
            return this;
        }

        /// <summary>
        /// Returns the number of accounts registered to the Customer instance.
        /// </summary>
        /// <returns>An integer with total number of accounts.</returns>
        public int GetNumberOfAccounts()
        {
            return this.accounts.Count;
        }

        /// <summary>
        /// Returns the total amount of interest across all this instance's accounts.
        /// </summary>
        /// <returns>A double representing the total interest earned.</returns>
        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account account in this.accounts)
                total += account.InterestEarned();
            return total;
        }

        /// <summary>
        /// Returns a string representation of the customer's current accounts.
        /// </summary>
        /// <returns>A string representation of this customer's accounts.</returns>
        public String GetStatement() 
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Statement for " + this.name);
            sb.AppendLine();

            double total = 0.0;
            foreach (Account account in this.accounts) 
            {
                sb.AppendLine();
                sb.Append(StatementForAccount(account));
                sb.AppendLine();
                total += account.SumTransactions();
            }
            sb.AppendLine();
            sb.Append("Total In All Accounts " + ToDollars(total));
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string representation of the account argument.
        /// </summary>
        /// <param name="account">An instance of Account.</param>
        /// <returns>A string representation of account.</returns>
        private String StatementForAccount(Account account) 
        {
            StringBuilder sb = new StringBuilder();

            //Translate to pretty account type
            switch (account.GetAccountType()){
                case Account.AccountType.Checking:
                    sb.AppendLine("Checking Account");
                    break;
                case Account.AccountType.Savings:
                    sb.AppendLine("Savings Account");
                    break;
                case Account.AccountType.MaxiSavings:
                    sb.AppendLine("Maxi Savings Account");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction transaction in account) {
                sb.AppendLine("  " + (transaction.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(transaction.Amount));
                total += transaction.Amount;
            }
            sb.Append("Total " + ToDollars(total));
            return sb.ToString();
        }

        /// <summary>
        /// Formats a double to display as a dollar amount. 
        /// </summary>
        /// <param name="amount">The double to convert to a dollar string representation.</param>
        /// <returns>String representation of dollar amount.</returns>
        private String ToDollars(double amount)
        {
            return String.Format("{0:C}", Math.Abs(amount));
        }
    }
}
