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
				.AppendLine("Statement for " + Name)
				.AppendLine();
			foreach (Account account in Accounts)
				statement.AppendLine(account.GetStatement());
			return statement
				.AppendLine("Total In All Accounts " + Accounts.Sum(a => a.SumTransactions()).ToDollars())
				.ToString();
		}
	}
}
