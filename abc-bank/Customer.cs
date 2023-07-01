using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private String Name;
        private List<Account> Accounts;

        public Customer(String name)
        {
            this.Name = name;
            this.Accounts = new List<Account>();
        }

        public String GetName()
        {
            return Name;
        }

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
            {
                total += a.InterestEarned();
            }

            return total;
        }

        public String GetStatement() 
        {
            String statement = null;
            statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (Account a in Accounts) 
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.SumTransactions();
            }

            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(Account a) 
        {
            String s = "";

            // possibly need later; we'll see.
            int accounttype = a.GetAccountType();
           //Translate to pretty account type
            switch (accounttype)
            {
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
            foreach (Transaction t in a.transactions) 
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;// < 0 ? -t.amount : t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            // see the result string before returning to caller
            var seestring = String.Format("{0:C}", d);
            return seestring;
            //return String.Format("$%,.2f", Math.Abs(d));
            
        }
    }
}
