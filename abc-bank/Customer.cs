using System;
using System.Collections.Generic;

namespace abc_bank
{
    /// <summary>
    /// Single customer for a Bank
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Name of the customer
        /// </summary>
        private string name;
        /// <summary>
        /// All accounts under the customer, hashset to prevent duplicates
        /// </summary>
        private HashSet<Account> accounts;

        /// <summary>
        /// Constructor setting defaults (passed in name and no accounts)
        /// </summary>
        /// <param name="name">Name of the customer being made</param>
        public Customer(string name)
        {
            this.name = name;
            accounts = new HashSet<Account>();
        }

        /// <summary>
        /// Getter method for getting the customer name
        /// </summary>
        /// <returns>Name of the customer</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// Adds an account to "accounts", meaning a new account is made for this customer
        /// </summary>
        /// <param name="account">Account being made for the customer</param>
        /// <returns>Updated customer object with new account</returns>
        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        /// <summary>
        /// Gets the number of accounts the customer owns
        /// </summary>
        /// <returns>Number of accounts stored in the hashset</returns>
        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        /// <summary>
        /// Gets total amount of money earned through interest on the customer's accounts
        /// </summary>
        /// <returns>Amount of interest the bank paid the customer in interest</returns>
        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        /// <summary>
        /// Gets the transaction history of all accounts for the customer along with total amount of money for each and all accounts
        /// </summary>
        /// <returns>All account information for the customer</returns>
        public string GetStatement() 
        {
            string statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total, true);
            return statement;
        }

        /// <summary>
        /// Moves money from one account to another given the accounts exist under the customer
        /// </summary>
        /// <param name="accountA">Account being withdrawn from</param>
        /// <param name="accountB">Account being deposited into</param>
        /// <param name="amount">Amount of money being moved from A to B</param>
        public void Transfer(Account accountA, Account accountB, double amount) 
        {
            if (accounts.Contains(accountA) && accounts.Contains(accountB)) {
                accountA.Withdraw(amount);
                accountB.Deposit(amount);
            }

            return;
        }

        /// <summary>
        /// Gets statement information for a single account including all transactions and a sum
        /// </summary>
        /// <param name="a">Account statement information is being processed for</param>
        /// <returns>Statement for an account as a string value</returns>
        private string statementForAccount(Account a) 
        {
            string s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
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
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount, false) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total, true);
            return s;
        }

        /// <summary>
        /// Gets a dollar amount for either a transaction, account total, or customer total.
        /// </summary>
        /// <param name="d">Amount of money in a transaction or some total</param>
        /// <param name="isTotal">If this is a total being processed for use in formatting negative numbers in the total</param>
        /// <returns>Dollar value as a string</returns>
        private string ToDollars(double d, bool isTotal)
        {
            var absValue = Math.Abs(d);
            var value = d < 0 && isTotal ? string.Format("-${0:N2}", absValue) : string.Format("${0:N2}", absValue);
            return value;
        }
    }
}
