using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        /* Current Features
        A customer can open an account - DONE.
        A customer can request a statement that shows transactions and totals for each of their accounts.
        A bank manager can get a report showing the total interest paid by the bank on all accounts - DONE.*/

        private String name { get; set; }

        private List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public String GetName()
        {
            return name;
        }

        /// <summary>
        /// A customer can open an account.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        /// <summary>
        /// returns total number of accounts
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        /// <summary>
        /// /*A bank manager can get a report showing the total interest paid by the bank on all accounts.*/
        /// returns total interest earned
        /// </summary>
        /// <returns></returns>
        public double TotalInterestEarned() 
        {
            double total = 0.0;
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        /// <summary>
        /// get a statement 
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStatement() 
        {
            StringBuilder statement = new StringBuilder();
            statement.Append("Statement for " + name + "\n");

            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement.Append("\n" + AccountTypeWithTotalTransactionsForStatement(a) + "\n");
                total += a.sumTransactions();
            }
            statement.Append("\nTotal In All Accounts " + ToDollars(total));
            return statement;
        }

        private String AccountTypeWithTotalTransactionsForStatement(Account a) 
        {
            String s = string.Empty;

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case Account.AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case Account.AccountType.MAXI_SAVINGS:
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

        private String ToDollars(double d)
        {
            return String.Format("${0:0.00}", Math.Abs(d));
        }
    }
}
