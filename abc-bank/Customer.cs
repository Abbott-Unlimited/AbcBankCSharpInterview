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

        public double TotalInterestEarned() => accounts.Sum(x => x.InterestEarned());

        public string GetStatement()
        {
            // Had to change all of my appendlines to \n. What was most likely happening is it was adding in a different
            // kind of line break, such as \n\r or carriage return.
            var statementBuilder = new StringBuilder();
            var total = 0.0;

            statementBuilder.Append($"Statement for {name}\n\n");

            foreach (var account in accounts)
            {
                statementBuilder.Append($"{StatementForAccount(account)}\n");
                total += account.SumTransactions();
            }

            statementBuilder.Append($"Total In All Accounts {ToDollars(total)}");
            return statementBuilder.ToString();
        }

        private String StatementForAccount(Account account)
        {
            // Switch to string builder to save memory, strings are immutable.
            var statementBuilder = new StringBuilder();

            //Translate to pretty account type
            switch (account.AccountType)
            {
                case AccountType.Checking:
                    statementBuilder.Append("Checking Account\n");
                    break;
                case AccountType.Savings:
                    statementBuilder.Append("Savings Account\n");
                    break;
                case AccountType.MaxiSavings:
                    statementBuilder.Append("Maxi Savings Account\n");
                    break;
            }

            //Now total up all the transactions
            var total = 0.0;
            var counter = 0;

            foreach (var transaction in account.Transactions)
            {
                var transactionType = transaction.TransactionType == TransactionType.Deposit ? "deposit" : "withdrawal";
                statementBuilder.Append($"  {transactionType} {ToDollars(transaction.Amount)}");

                // There are simpler ways to do this but in the interest of time I am going to have it just add lines 
                // after transactions until it hits the end of the list.
                counter += 1;
                if (counter != account.Transactions.Count)
                {
                    statementBuilder.Append("\n");
                }

                total += transaction.Amount;
            }

            statementBuilder.Append("\n");
            statementBuilder.Append($"Total {ToDollars(total)}\n");
            return statementBuilder.ToString();
        }

        private static string ToDollars(double amount) => $"{Math.Abs(amount):C}";
    }
}