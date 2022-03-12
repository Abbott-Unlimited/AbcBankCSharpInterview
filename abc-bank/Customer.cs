using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace abc_bank
{
    public class Customer
    {
        private readonly String name;
        private readonly List<Account> accounts;

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
            String statement = "Statement for " + name + "\n";
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.GetBalance();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        public void Transfer(double amount, Account fromAccount, Account toAccount)
        {
            if (amount <= 0)
                throw new Exception("amount must be greater than 0");

            if (amount > fromAccount.GetBalance())
                throw new Exception("account does not have enough to transfer");

            if (!accounts.Contains(fromAccount) || !accounts.Contains(toAccount))
                throw new Exception("customer does not own the provided accounts");

            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
        }


        private String StatementForAccount(Account a) 
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

            foreach (Transaction t in a.GetTransactions()) {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
            }

            s += "Total " + ToDollars(a.GetBalance());
            return s;
        }

        private String ToDollars(double d)
        {
            return Math.Abs(d).ToString("C", CultureInfo.CurrentCulture);
        }
    }
}
