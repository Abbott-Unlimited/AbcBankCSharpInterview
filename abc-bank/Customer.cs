using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Customer
    {
        public string name;
        public List<Account> accounts;

        public Customer(String name)
        {
            this.name = name;
            accounts = new List<Account>();
        }

        public Customer OpenAccount(Account account)
        {
            accounts.Add(account);
            return this;
        }

        public void Transfer(Account acctFrom, Account acctTo, double amount)
        {
            if (acctFrom.CalculateBalance() < amount)
            {
                throw new ArgumentException("Transfer Amount must be no larger than amount in source account.");
            }
            acctFrom.AddTransaction(-amount);
            acctTo.AddTransaction(amount);
        }

        public String GetStatement() 
        {
            string statement = $"Statement for {name}\n";
            double total = 0.0;
            foreach (Account a in accounts) 
            {
                statement += AccountDetails(a);
                total += a.CalculateBalance();
            }
            statement += $"\nTotal In All Accounts {String.Format("{0:C2}", total)}";
            return statement;
        }
     
        private String AccountDetails(Account a) 
        {
            var s = $"\n{a.accountType.GetDescription()} Account\n";

            double total = 0.0;
            foreach (Transaction t in a.transactions) {
                s += $"\t{(t.amount < 0 ? "withdrawal" : "deposit")} {ToDollars(t.amount)}\n";
                total += t.amount;
            }
            s += $"Total {ToDollars(total)}\n";
            return s;
        }

        private String ToDollars(double d)
        {
            return String.Format("{0:C2}", Math.Abs(d));
        }
    }
}
