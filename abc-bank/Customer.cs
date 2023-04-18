using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            return accounts.Sum(x => x.InterestEarned());
        }

        public String GetStatement()
        {
            StringBuilder statement = new StringBuilder($"Statement for {name}\n");

            foreach (Account account in accounts)
            {
                statement.Append($"\n{statementForAccount(account)}\n");
            }

            double total = accounts.Sum(x => x.SumTransactions());

            statement.Append($"\nTotal In All Accounts {ToDollars(total)}");

            return statement.ToString();
        }

        private String statementForAccount(Account account)
        {
            StringBuilder statement = new StringBuilder();

            switch (account.GetAccountType())
            {
                case Account.CHECKING:
                    statement.Append("Checking Account\n");
                    break;
                case Account.SAVINGS:
                    statement.Append("Savings Account\n");
                    break;
                case Account.MAXI_SAVINGS:
                    statement.Append("Maxi Savings Account\n");
                    break;
            }

            foreach (Transaction transaction in account.Transactions)
            {
                String transactionType = transaction.amount < default(double) ? "withdrawal" : "deposit";

                statement.Append($"  {transactionType} {ToDollars(transaction.amount)}\n");
            }

            double total = account.Transactions.Sum(x => x.amount);

            statement.Append($"Total {ToDollars(total)}");

            return statement.ToString();
        }

        private String ToDollars(double d)
        {
            return Math.Abs(d).ToString("C");
        }
    }
}