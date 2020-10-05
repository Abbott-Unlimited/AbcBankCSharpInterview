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
        public List<Account> accounts;

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
            return String.Format("{0}", Math.Abs(d).ToString("C"));
        }

        public void TransferToAnotherAccount(Account accountToWithdrawFrom, Account accountToAddTo, double amount)
        {
            accountToWithdrawFrom.Withdraw(amount);
            accountToAddTo.Deposit(amount);
        }

        public Account GetAccount(int accountType)
        {
            switch(accountType)
            {
                case 1:
                     Account temp = new Account(Account.CHECKING);
                    return accounts[accounts.FindIndex(a => a.GetType() ==  temp.GetType())];
                    
                case 2:
                    Account tempSavings = new Account(Account.SAVINGS);
                    return accounts[accounts.FindIndex(a => a.GetType() == tempSavings.GetType())];
                    
                case 3:
                    Account tempMaxiSavings = new Account(Account.MAXI_SAVINGS);
                    return accounts[accounts.FindIndex(a => a.GetType() == tempMaxiSavings.GetType())];
                    
            }
            return null;
        }
    }
}
