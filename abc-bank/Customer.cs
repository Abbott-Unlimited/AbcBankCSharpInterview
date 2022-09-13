using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        public Customer(string name)
        {
            Name = name;
            Accounts = new List<Account>();
        }

        public string Name { get; set; }
        public List<Account> Accounts { get; set; }

        public Customer OpenAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return Accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in Accounts)
                total += a.InterestEarned(Accounts);
            return total;
        }

        public string GetStatement() 
        {
            var statement = new StringBuilder();
            statement.Append("Statement for " + Name + "\n");
            double total = 0.0;
            foreach (Account a in Accounts) 
            {
                statement.Append("\n" + StatementForAccount(a) + "\n");
                total += a.sumTransactions();
            }
            statement.Append("\nTotal In All Accounts " + ToDollars(total));
            return statement.ToString();
        }

        private string StatementForAccount(Account a) 
        {
            string s = "";
            
            //Translate to pretty account type
            switch (a.GetAccountType()){
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

        private string ToDollars(double d)
        {
            //return string.Format("$%,.2f", Math.Abs(d));
            return d.ToString("c", CultureInfo.CurrentCulture);
        }
    }
}
