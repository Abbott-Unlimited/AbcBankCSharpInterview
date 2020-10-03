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
        private HashSet<Account> accounts; // HashSet to prevent duplicates

        public Customer(String name)
        {
            this.name = name;
            this.accounts = new HashSet<Account>();
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
            statement += "\nTotal In All Accounts " + ToDollars(total, true);
            return statement;
        }

        public void Transfer(Account accountA, Account accountB, double amount) 
        {
            if (accounts.Contains(accountA) && accounts.Contains(accountB)) {
                accountA.Withdraw(amount);
                accountB.Deposit(amount);
            }

            return;
        }

        private String statementForAccount(Account a) 
        {
            String s = "";

           //Translate to pretty account type
            switch(a.GetAccountType()){
                case AccountType.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountType.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountType.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount, false) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total, true);
            return s;
        }

        private String ToDollars(double d, bool isTotal)
        {
            var absValue = Math.Abs(d);
            var value = d < 0 && isTotal ? string.Format("-${0:N2}", absValue) : string.Format("${0:N2}", absValue);
            return value;
        }
    }
}
