using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

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
				expected: @"Customer Summary
 - John (1 account)",
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
			Account checkingAccount = new Account(Account.AccountTypeEnum.SAVINGS);
			bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));
			checkingAccount.Deposit(1500m);
			Assert.AreEqual(
				expected: 2m,
				actual: bank.TotalInterestPaid());
		}
		[TestMethod]
		public void Maxi_savings_account()
		{
			Bank bank = new Bank();
			Account checkingAccount = new Account(Account.AccountTypeEnum.MAXI_SAVINGS);
			bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));
			checkingAccount.Deposit(3000m);
			Assert.AreEqual(
				expected: 170m,
				actual: bank.TotalInterestPaid());
		}
	}
}
