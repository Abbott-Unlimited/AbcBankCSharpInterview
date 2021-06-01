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

        private String statementForAccount(Account a) 
        {
            String s = "";
            //StringBuilder str = new StringBuilder(" ");

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.CHECKING:
                    s += "Checking Account\n";
                    //str.Append("Checking Account\n");
                    break;
                case Account.SAVINGS:
                    s += "Savings Account\n";
                    //str.Append("Savings Account\n");
                    break;
                case Account.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    //str.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions)
            {
                //s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                s += "  ";
                //str.Append("  ");
                if (t.depositType == (int)Transaction.DepositType.DEPOSIT)
                    s += "deposit";
                    //str.Append("deposit");
                else if (t.depositType == (int)Transaction.DepositType.WITHDRAW)
                    s += "withdrawal";
                    //str.Append("withdrawal");
                s += " ";
                //str.Append(" ");
                s += ToDollars(t.amount) + "\n";
                //str.Append(ToDollars(t.amount) + "\n");
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            //str.Append("Total " + ToDollars(total));
            return s;
            //return str.ToString();
        }

        private String ToDollars(double d)
        {
            return string.Format("{0:C}", Math.Abs(d));
        }
    }
}
