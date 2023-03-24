using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using System.Text;
using System;

namespace abc_bank_tests
{
	[TestClass]
	public class BankTest
	{
		public const decimal DOUBLE_DELTA = 1e-15m;
		[TestMethod]
		public void CustomerSummary()
		{
			Bank bank = new Bank();
			Customer john = new Customer("John");
			john.OpenAccount(new Account(Account.AccountTypeEnum.CHECKING));
			bank.AddCustomer(john);
			Assert.AreEqual(
				expected: new StringBuilder()
					.AppendLine("Customer Summary")
					.AppendLine(" - John (1 account)")
					.ToString(),
				actual: bank.CustomerSummary());
		}
		[TestMethod]
		public void CheckingAccount()
		{
			Bank bank = new Bank();
			Account checkingAccount = new Account(Account.AccountTypeEnum.CHECKING);
			Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
			bank.AddCustomer(bill);
			checkingAccount.Deposit(100m);
			Assert.AreEqual(
				expected: 0.1m,
				actual: bank.TotalInterestPaid());
		}
		[TestMethod]
		public void Savings_account()
		{
			Bank bank = new Bank();
			Account savingsAccount = new Account(Account.AccountTypeEnum.SAVINGS);
			bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));
			savingsAccount.Deposit(1500m);
			Assert.AreEqual(
				expected: 2m,
				actual: bank.TotalInterestPaid());
		}
		[TestMethod]
		public void Maxi_savings_account()
		{
			Bank bank = new Bank();
			Account maxiSavingsAccount = new Account(Account.AccountTypeEnum.MAXI_SAVINGS);
			bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));
			maxiSavingsAccount.Deposit(3000m);
			Assert.IsFalse(maxiSavingsAccount.IsWithdrawAfter(DateTime.Now - TimeSpan.FromDays(10)));
			Assert.AreEqual(
				expected: 150m, // 5% of 3000
				actual: bank.TotalInterestPaid());
			maxiSavingsAccount.Withdraw(1000m);
			Assert.IsTrue(maxiSavingsAccount.IsWithdrawAfter(DateTime.Now - TimeSpan.FromDays(10)));
			Assert.AreEqual(
				expected: 2000m,
				actual: maxiSavingsAccount.SumTransactions());
			Assert.AreEqual(
				expected: 2m, // 0.01% of 2000
				actual: bank.TotalInterestPaid());
		}
	}
}
