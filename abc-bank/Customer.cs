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
        /// Transfers funds between a customer's accounts.
        /// </summary>
        /// <param name="fromAccount">The account to transfer funds out of.</param>
        /// <param name="toAccount">The account to transfer funds into.</param>
        /// <param name="amount">The amount to transfer.</param>
        public void Transfer(Account fromAccount, Account toAccount, decimal amount)
        {
            if(!accounts.Contains(fromAccount) || !accounts.Contains(toAccount))
            {
                throw new ArgumentException("Accounts must be owned by the customer in order to transfer funds.");
            }

            // If there are insufficient funds, let that exception bubble up.
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        }

        /// <summary>
        /// Generates a statement for this customer's accounts.
        /// </summary>
        /// <returns>string containing the statement text.</returns>
        public string GetStatement() 
        {
            StringBuilder statement = new StringBuilder();
            statement.AppendLine("Statement for " + Name);
            decimal total = 0.00m;
            foreach (Account a in accounts) 
            {
                statement.AppendLine();
                statement.AppendLine(a.GenerateStatement());
                total += a.Balance;
            }
            statement.AppendLine();
            statement.Append("Total In All Accounts " + FormatUtils.ToDollars(total));
            return statement.ToString();
        }
    }
}
