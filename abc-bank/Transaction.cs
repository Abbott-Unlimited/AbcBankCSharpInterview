using System;

namespace abc_bank
{
	public class Transaction
	{
		public readonly decimal Amount;
		public readonly DateTime TransactionDate;
		public Transaction(decimal amount)
		{
			Amount = amount;
			TransactionDate = DateTime.Now;
		}
	}
}
