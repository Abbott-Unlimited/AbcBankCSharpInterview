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
				.AppendLine("Statement for " + Name)
				.AppendLine();
			foreach (Account account in Accounts)
				statement.AppendLine(account.GetStatement());
			return statement
				.AppendLine("Total In All Accounts " + Accounts.Sum(a => a.SumTransactions()).ToDollars())
				.ToString();
		}
		public void Transfer(decimal amount, Account from, Account to)
		{
			if (amount <= 0)
				throw new ArgumentException("amount must be greater than zero. Was: \"" + amount + "\"");
			if (!Accounts.Contains(from))
				throw new ArgumentException("\"from\" account does not belong to customer \"" + Name + "\"");
			if (!Accounts.Contains(to))
				throw new ArgumentException("\"to\" account does not belong to customer \"" + Name + "\"");
			if (from.SumTransactions() < amount)
				throw new InvalidOperationException("Insufficient funds for transfer.");
			// For a real bank, there'd be additional safety checks in here to make sure neither operation fails.
			from.Withdraw(amount);
			to.Deposit(amount);
		}
		/// <summary>
		/// Safe version of Transfer
		/// </summary>
		/// <returns>true if transfer succeeded</returns>
		public bool TryTransfer(decimal amount, Account from, Account to) => TryTransfer(amount, from, to, out _);
		public bool TryTransfer(decimal amount, Account from, Account to, out Exception exception)
		{
			exception = null;
			try
			{
				Transfer(amount, from, to);
			}
			catch (Exception ex)
			{
				exception = ex;
				return false;
			}
			return true;
		}
	}
}
