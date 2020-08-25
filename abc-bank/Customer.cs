using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//WT1 Claude Collins August 2020
//WT3 Claude Collins August 2020 (xfer money)

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
            String statement = null;
            statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private String statementForAccount(Account a) 
        {
            String s = "";

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
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
			//return String.Format("$%,.2f", Math.Abs(d));
			//WT1 - Replaced above with below. Above is for Java
			return String.Format("{0:C}", Math.Abs(d));
        }

		//WT3 added below Xfer
		public void Xfer(Account from, Account to, double amount)
		{
			if (amount <= 0)
			{
				throw new ArgumentException("Amount must be greater than zero");
			}
			else if (from == to)
			{
				throw new ArgumentException("Accounts must be different");
			}
			else if (amount > from.sumTransactions())
			{
				throw new ArgumentException("Not enough moola in account to transfer");
			}
			else
			{
				from.Withdraw(amount);
				to.Deposit(amount);
			}
		}

	}
}
