using System;
using System.Collections.Generic;

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

        public void transferFunds(Account a, Account b, double Amt)
        {
            //account a is the account to transfer from
            //account b is the account to transfer to
            //double Amt is the amount to transfer
            try
            {
                a.Withdraw(Amt);
                b.Deposit(Amt);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                    {
                    throw new Exception("Transfer Not Possible");
                }
            }



        }

        private String statementForAccount(Account a)
        {
            String s = "";

            //Translate to pretty account type
            switch (a.GetAccountType())
            {
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
            foreach (Transaction t in a.transactions)
            {
                s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
                total += t.amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double d)
        {
            return String.Format("{0:C}", Math.Abs(d));
        }
    }
}
