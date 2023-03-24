using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abc_bank
{
	public class Customer
	{
		public string Name { get; private set; }
		public readonly List<Account> Accounts = new List<Account>();
		public Customer(string name) => Name = name;
		public Customer OpenAccount(Account account)
		{
			Accounts.Add(account);
			return this;
		}
		public int GetNumberOfAccounts() => Accounts.Count;
		public decimal TotalInterestEarned() => Accounts.Sum(a => a.InterestEarned());
		public string GetStatement()
		{
			StringBuilder statement = new StringBuilder()
				.AppendLine("Statement for " + Name);
			foreach (Account a in Accounts)
				statement.AppendLine(StatementForAccount(a));
			return statement
				.AppendLine("Total In All Accounts " + ToDollars(Accounts.Sum(a => a.SumTransactions())))
				.ToString();
		}
		private static string StatementForAccount(Account account)
		{
			StringBuilder sb = new StringBuilder()
				.AppendLine(account.AccountType.GetDescription());
			foreach (Transaction t in account.Transactions)
				sb.AppendLine("\t" + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount));
			return sb
				.AppendLine("Total " + ToDollars(account.SumTransactions()))
				.ToString();
		}
		public static string ToDollars(decimal d) => string.Format("$%,.2f", Math.Abs(d));
	}
}
