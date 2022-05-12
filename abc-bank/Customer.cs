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

		// mn6473 - Added this method per Additional Feature requirement to allow a customer to transfer between funds
		public void Transfer(int fromAccountType, int toAccountType, double amount)
		{
			Account fromAccount = accounts.Where(a => a.GetAccountType() == fromAccountType).SingleOrDefault();

			if (fromAccount != null)
			{
				Account toAccount = accounts.Where(a => a.GetAccountType() == toAccountType).SingleOrDefault();

				if (toAccount == null)
				{
					toAccount = new Account(toAccountType);
					OpenAccount(toAccount);
				}

				fromAccount.Withdraw(amount);
				toAccount.Deposit(amount);
			}
			else
			{
				throw (new Exception($"Attempt to transfer from a non-existent account: account type {fromAccountType}"));
			}
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
			// Negative amount will be wrapped in parentheses. For example, ($500.25) instead of -$500.25.
			return String.Format($"{d:C}");		
//            return String.Format("$%,.2f", Math.Abs(d));
        }
    }
}
