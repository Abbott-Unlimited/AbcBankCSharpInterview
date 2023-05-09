using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Customer
    {
        private readonly string        _name;
        private readonly List<Account> _accounts;

        public Customer(string name)
        {
            _name = name;
            _accounts = new List<Account>();
        }

        public string GetName()
        {
            return _name;
        }

        public Customer OpenAccount(Account account)
        {
            _accounts.Add(account);
            return this;
        }

        public int GetNumberOfAccounts()
        {
            return _accounts.Count;
        }

        public double TotalInterestEarned()
        {
            return _accounts.Sum(a => a.InterestEarned());
        }

        public string GetStatement() 
        {
            var statement = "Statement for " + _name + "\n";
            var    total     = 0.0;

            foreach (var account in _accounts)
            {
                statement += "\n" + StatementForAccount(account) + "\n";
                total     += account.SumTransactions();
            }

            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private static string StatementForAccount(Account a) 
        {
            var s = string.Empty;

            //Translate to pretty account type
            switch(a.GetAccountType()){
                case Account.Checking:
                    s += "Checking Account\n";
                    break;
                case Account.Savings:
                    s += "Savings Account\n";
                    break;
                case Account.MaxiSavings:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            var total = 0.0;
            foreach (var t in a.Transactions) {
                s     += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private static string ToDollars(double d)
        {
            return $"{Math.Abs(d):C}";
        }
    }
}
