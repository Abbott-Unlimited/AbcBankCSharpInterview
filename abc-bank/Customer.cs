using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    // A customer can open an account.
    // A customer can deposit / withdraw funds from an account.
    // A customer can request a statement that shows transactions and totals for each of their accounts.

    public class Customer
    {
        private readonly string _name;
        public string Name { get { return _name; } }

        private readonly List<Account> _accounts = new List<Account>();

        public Customer(string name)
        {
            _name = name;
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

        public decimal GetTotalInterestEarned() 
        {
            decimal total = 0.0m;
            
            foreach (Account account in _accounts)
            {
                total += account.GetInterestEarned();
            }
            return total;
        }

        public string GetStatementForAllAcounts() 
        {
            string statement = $"Statement for {_name}\n";
            decimal totalInAllAccounts = 0;
            
            foreach (Account account in _accounts) 
            {
                statement += $"\n{GetStatementForOneAccount(account)}\n";
                totalInAllAccounts += account.SumTransactions();
            }
            statement += $"\nTotal In All Accounts {ToDollars(totalInAllAccounts)}";
            
            return statement;
        }

        public string GetStatementForOneAccount(Account account) 
        {
            string statement = "";

            //Translate to pretty account type
            statement += $"{account.AccountName}\n";

            //Now total up all the transactions
            decimal total = 0;
            foreach (Transaction transaction in account.GetTransactions())
            {
                statement += $"  {transaction.TransactionType} {ToDollars(transaction.Amount)}\n";
                total += transaction.Amount;
            }
            statement += $"Total {ToDollars(total)}";

            return statement;
        }

        private string ToDollars(decimal amount)
        {
            return string.Format("{0:C}", Math.Abs(amount));
        }
    }
}
