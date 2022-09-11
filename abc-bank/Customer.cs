using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private readonly string name;
        private readonly List<Account> accounts;

        public Customer(string name)
        {
            this.name = name;
            accounts = new List<Account>();
        }

        public string GetName()
        {
            return name;
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (var a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public string GetStatement() 
        {
            string statement;
            //statement = "Statement for " + name + "\n";
            statement = $"Statement for {name}\n";
            double total = 0.0;
            foreach (var a in accounts) 
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private string StatementForAccount(Account a) 
        {
            string s = "";

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
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private string ToDollars(double d)
        {
            return string.Format("$%,.2f", Math.Abs(d));
        }
    }
}
