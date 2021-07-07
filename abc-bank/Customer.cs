namespace ABC_bank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Customer
    {
        private readonly string name;
        private readonly List<Account> accounts;

        public Customer(string name)
        {
            this.name = name;
            this.accounts = new List<Account>();
        }

        public string GetName()
        {
            return this.name;
        }

        public Customer OpenAccount(Account account)
        {
            this.accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return this.accounts.Count;
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in this.accounts)
            {
                total += a.InterestEarned();
            }

            return total;
        }

        public string GetStatement() 
        {
            string statement = "Statement for " + this.name + Environment.NewLine;
            double total = 0.0;
            foreach (Account a in this.accounts) 
            {
                statement += Environment.NewLine + this.StatementForAccount(a) + Environment.NewLine;
                total += a.SumTransactions();
            }

            statement += Environment.NewLine + "Total In All Accounts " + this.ToDollars(total);
            return statement;
        }

        private string StatementForAccount(Account a) 
        {
            string s = string.Empty;

            // Translate to pretty account type
            switch (a.GetAccountType())
            {
                case Account.CHECKING:
                    s = "Checking Account" + Environment.NewLine;
                    break;
                case Account.SAVINGS:
                    s = "Savings Account" + Environment.NewLine;
                    break;
                case Account.MAXISAVINGS:
                    s = "Maxi Savings Account" + Environment.NewLine;
                    break;
            }

            // Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions)
            {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + this.ToDollars(t.Amount) + Environment.NewLine;
                total += t.Amount;
            }

            s += "Total " + this.ToDollars(total);
            return s;
        }

        private string ToDollars(double d)
        {
            return string.Format("{0:C}", Math.Abs(d));
        }
    }
}
