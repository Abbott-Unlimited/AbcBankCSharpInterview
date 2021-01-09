using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Customer
    {
        private String name;
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
            foreach (Account a in accounts)
                total += a.InterestEarned();
            return total;
        }

        public double GetCurrentBalanceForAccount(Account account)
        {
            double total = 0.0;
            foreach (Transaction t in account.transactions)
            {
                total += t.amount;
            }

            return total;
        }

        public string GetStatement() 
        {
            string statement;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement += "\n" + GetStatementForAccount(a) + "\n";
                total += a.SumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private string GetStatementForAccount(Account a) 
        {
            string statement = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.AccountType.CHECKING:
                    statement += "Checking Account\n";
                    break;
                case Account.AccountType.SAVINGS:
                    statement += "Savings Account\n";
                    break;
                case Account.AccountType.MAXI_SAVINGS:
                    statement += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                statement += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            statement += "Total " + ToDollars(total);
            return statement;
        }

        private string ToDollars(double d)
        {
            return String.Format("${0:0,0.00}", Math.Abs(d));
        }
    }
}
