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

        public Customer TransferMoney(string withdrawAccountName, string depositAccountName, int amount)
        {
            Account withdrawAccount, depositAccount;
            withdrawAccount = depositAccount = null;

            bool successful = false;
            foreach(Account a in accounts)
            {
                if (a.accountName == withdrawAccountName && withdrawAccount == null)
                {
                    withdrawAccount = a;
                }
                else if (a.accountName == depositAccountName && depositAccount == null)
                {
                    depositAccount = a;
                }

                if (withdrawAccount != null && depositAccount != null)
                {
                    withdrawAccount.Withdraw(amount);
                    depositAccount.Deposit(amount);
                    successful = true;
                    break;
                }
            }

            if (!successful)
            {
                if(withdrawAccount == null)
                    throw new ArgumentException("withdraw account not found");
                else
                    throw new ArgumentException("deposit account not found");
            }

            return this;
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
                total += a.SumTransactions();
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
            return String.Format("${0:n}", Math.Abs(d));
        }
    }
}
