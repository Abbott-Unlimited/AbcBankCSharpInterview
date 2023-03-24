using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace abc_bank
{
	public class Account
	{
		public enum AccountTypeEnum
		{
			[Description("Checking Account")]
			CHECKING = 0,
			[Description("Savings Account")]
			SAVINGS = 1,
			[Description("Maxi Savings Account")]
			MAXI_SAVINGS = 2,
		};
		public readonly AccountTypeEnum AccountType;
		public readonly List<Transaction> Transactions = new List<Transaction>();
		public Account(AccountTypeEnum accountType) => AccountType = accountType;
		public void Deposit(decimal amount)
		{
			if (amount <= 0)
				throw new ArgumentException("amount must be greater than zero. Was: " + amount);
			Transactions.Add(new Transaction(amount));
		}
		public void Withdraw(decimal amount)
		{
			if (amount <= 0)
				throw new ArgumentException("amount must be greater than zero. Was: " + amount);
			Transactions.Add(new Transaction(-amount));
		}
		public decimal InterestEarned()
		{
			decimal amount = SumTransactions();
			switch (AccountType)
			{
				case AccountTypeEnum.SAVINGS:
					return amount <= 1000m ? amount * 0.001m
						: 1m + (amount - 1000m) * 0.002m;
				case AccountTypeEnum.MAXI_SAVINGS:
					//Change **Maxi-Savings accounts** to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
					return amount * (!IsWithdrawAfter(DateTime.Now - TimeSpan.FromDays(10)) ? 0.05m : 0.001m);
				default:
					return amount * 0.001m;
			}
		}
		public bool IsWithdrawAfter(DateTime dateTime) => Transactions
			.Where(t => t.Amount < 0
				&& t.TransactionDate >= dateTime)
			.Any();
		public decimal SumTransactions() => Transactions.Sum(t => t.Amount);
		public string GetStatement()
		{
			StringBuilder statement = new StringBuilder()
				.AppendLine(AccountType.GetDescription());
			foreach (Transaction transaction in Transactions)
				statement.AppendLine("\t" + (transaction.Amount < 0 ? "withdrawal" : "deposit") + " " + transaction.Amount.ToDollars());
			return statement
				.AppendLine("Total " + SumTransactions().ToDollars())
				.ToString();
		}
	}
}
