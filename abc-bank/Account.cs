using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
					return amount <= 1000m ? amount * 0.02m
						: amount <= 2000m ? 20m + (amount - 1000m) * 0.05m
						: 70m + (amount - 2000m) * 0.1m;
				default:
					return amount * 0.001m;
			}
		}
		public decimal SumTransactions() => Transactions.Sum(t => t.Amount);
	}
}
