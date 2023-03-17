using System;
using System.Collections.Generic;
using System.Data;

namespace abc_bank
{
    public class Customer
    {
        private string _name;
        private List<Account> _accounts;

        public Customer(string name)
        {
            _name = name;
            _accounts = new List<Account>();
        }

        public string CustomerName
        {
            get { return _name; } private set { _name = value; }
        }

        public List<Account> Accounts
        {
            get { return _accounts; }
        }

        public Customer OpenAccount(Account account)
        {
            _accounts.Add(account);
            return this;
        }

        public int NumberOfAccounts
        {
            get { return _accounts.Count; }
        }

        public double TotalInterestEarned() 
        {
            double total = 0;
            foreach (Account a in _accounts)
                total += a.InterestEarned();
            return total;
        }

        public string GetStatement() 
        {
            string statement = null;
            statement = "Statement for " + _name + "\n";
            double total = 0.0;
            foreach (Account a in _accounts) 
            {
                statement += "\n" + statementForAccount(a) + "\n";
                total += a.sumTransactions();
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        private string statementForAccount(Account a) 
        {
            string s = "";

           //Translate to pretty account type
            switch(a.AccountType){
                case AccountTypeEnum.CHECKING:
                    s += "Checking Account\n";
                    break;
                case AccountTypeEnum.SAVINGS:
                    s += "Savings Account\n";
                    break;
                case AccountTypeEnum.MAXI_SAVINGS:
                    s += "Maxi Savings Account\n";
                    break;
            }

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.AccountTransactions) {
                s += "  " + (t.TransactionAmount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.TransactionAmount) + "\n";
                total += t.TransactionAmount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private string ToDollars(double d)
        {
            var amount = Math.Abs(d);
            var dollars = string.Format("{0:C}", amount);
            return dollars;
        }

        public string Transfer(Account origination, Account destination, double amount)
        {
            if(origination == null || destination == null || amount < 0)
            {
                throw new ArgumentException("One or more of the arguements provided did not meet expected criteria!");
            }

            if (origination.sumTransactions() - amount < 0)
            {
                throw new Exception("Cannot transfer more many than exists in an account!");
            }

            origination.Withdraw(amount);
            destination.Deposit(amount);

            return string.Format("{0} was withdrawn from {1} and {0} was deposted into {2}", amount, origination.AccountType.ToString(), destination.AccountType.ToString());
        }
    }
}
