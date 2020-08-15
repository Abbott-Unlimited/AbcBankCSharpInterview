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

        public String GetStatement() 
        {
            var statement = new StringBuilder(string.Format("Statement for {0}\n", name));
            double total = 0.0;
            foreach (Account a in accounts)
            {
                statement.Append(string.Format("\n{0}\n", statementForAccount(a)));
                total += a.sumTransactions(DateTime.Now);
            }
            statement.Append(string.Format("\nTotal In All Accounts {0}", ToDollars(total)));
            return statement.ToString();
        }

        public void Transfer(Account fromAccount, Account toAccount, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount must be greater than zero");
            }
            else
            {
                fromAccount.Withdraw(amount);
                toAccount.Deposit(amount);
            }
        }

        private String statementForAccount(Account a) 
        {
            var s = new StringBuilder();

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.CHECKING:
                    s.Append("Checking Account\n");
                    break;
                case Account.SAVINGS:
                    s.Append("Savings Account\n");
                    break;
                case Account.MAXI_SAVINGS:
                    s.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s.Append("  ");
                s.Append(t.amount < 0 ? "withdrawal" : "deposit");
                s.Append(" ");
                s.Append(ToDollars(t.amount));
                s.Append("\n");
                total += t.amount;
            }
            s.Append("Total ");
            s.Append(ToDollars(total));
            return s.ToString();
        }

        private String ToDollars(double d)
        {
            return String.Format("{0:C}", Convert.ToInt32(d));
        }
    }
}
