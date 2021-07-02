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
            account.accountNumber = (GetNumberOfAccounts() + 1);
            accounts.Add(account);
            return this;
        }

        public String Transfer(decimal amount, int fromacct, int toacct)
        {
            int x = 0;
            int confirmedfrom = -1;
            int confirmedto = -1;

            if (amount < 0.01m)
                return "ERROR: Transfer amount cannot be less than 1 cent.";


            if (fromacct == toacct)
                return "ERROR: Sending and receiving account numbers are the same.";

            foreach (Account a in accounts)
            {
                if (confirmedfrom == -1)
                {
                    if (a.accountNumber == fromacct)
                    { 
                        if (a.sumTransactions() < amount)                     
                            return "ERROR: Insufficient funds.";                     
                        confirmedfrom = x;
                    }
                }

                if (confirmedto == -1)
                {
                    if (a.accountNumber == toacct)
                    {
                        confirmedto = x;
                    }
                }
                if ((confirmedfrom >=0) && (confirmedto >=0))
                {
                    break;
                }
                x += 1;
            }
            if ((confirmedfrom >= 0) && (confirmedto >= 0))
                {
                accounts[confirmedfrom].Withdraw(amount);
                accounts[confirmedto].Deposit(amount);
                return "Transfer complete.";
            }
            else           
                return "ERROR: One or more account numbers are invalid.";  
        }

        public int GetNumberOfAccounts()
        {
            return accounts.Count;
        }

        public decimal TotalInterestEarned() 
        {
            decimal total = 0;
            foreach (Account a in accounts)
                total += a.totalInterest;
            return total;
        }

        public String GetStatement() 
        {
            String statement = null;
            statement = "Statement for " + name + "\n";
            decimal total = 0;
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
            s += "#" + a.accountNumber.ToString("X8") + ": ";

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
            decimal total = 0.0m;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            total += a.totalInterest;
            s += "Total Interest Earned: " + ToDollars(a.totalInterest) + "\n";
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(decimal d)
        {
            return String.Format("{0:C}", Math.Abs(d));
        }
    }
}
