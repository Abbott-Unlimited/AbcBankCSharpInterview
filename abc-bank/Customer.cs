using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace abc_bank
{
    public class Customer
    {
        public string Name { get; }
        public List<Account> Accounts { get; } = new List<Account>();

        public Customer(String name)
        {
            Name = name;
        }

        public double TotalInterestEarned =>
            Accounts.Sum(account => account.InterestEarned);

        public Customer OpenAccount(Account account)
        {
            Accounts.Add(account);
            return this;
        }

        public String GetStatement() 
        {
            String statement = "Statement for " + Name + "\n";
            double total = 0.0;
            foreach (Account a in Accounts)
            {
                statement += "\n" + StatementForAccount(a) + "\n";
                total += a.Balance;
            }
            statement += "\nTotal In All Accounts " + ToDollars(total);
            return statement;
        }

        public void TransferBetweenAccounts(Account sourceAccount, Account targetAccount, double amount)
        {
            // Ensure both accounts belong to the customer before allowing the transfer
            if (sourceAccount == null || !Accounts.Contains(sourceAccount))
            {
                throw new ArgumentException("must belong to the customer", nameof(sourceAccount));
            }

            if (targetAccount == null || !Accounts.Contains(targetAccount))
            {
                throw new ArgumentException("must belong to the customer", nameof(targetAccount));
            }

            sourceAccount.Transfer(targetAccount, amount);
        }

        public string ToNameAndAccountsCountString() =>
            $"{Name} ({Accounts.Count} {(Accounts.Count == 1 ? "account" : "accounts")})";

        private String StatementForAccount(Account a)
        {
            String s = "";

            s += $"{a.Name}\n";

            //Now total up all the transactions
            double total = 0.0;
            foreach (Transaction t in a.Transactions) {
                s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
                total += t.Amount;
            }
            s += "Total " + ToDollars(total);
            return s;
        }

        private String ToDollars(double amount) =>
            Math.Abs(amount).ToString("C", CultureInfo.GetCultureInfo("en-US"));
    }
}
