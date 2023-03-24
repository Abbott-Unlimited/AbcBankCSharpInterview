using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using System.Text;

namespace abc_bank_tests
{
	[TestClass]
	public class CustomerTest
	{
		[TestMethod]
		public void TestApp()
		{
			Account checkingAccount = new Account(Account.AccountTypeEnum.CHECKING),
				savingsAccount = new Account(Account.AccountTypeEnum.SAVINGS);
			Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
			checkingAccount.Deposit(100m);
			savingsAccount.Deposit(4000m);
			savingsAccount.Withdraw(200m);
			Assert.AreEqual(
				expected: new StringBuilder()
					.AppendLine("Statement for Henry")
					.AppendLine()
					.AppendLine("Checking Account")
					.AppendLine("\tdeposit $100.00")
					.AppendLine("Total $100.00")
					.AppendLine()
					.AppendLine("Savings Account")
					.AppendLine("\tdeposit $4,000.00")
					.AppendLine("\twithdrawal $200.00")
					.AppendLine("Total $3,800.00")
					.AppendLine()
					.AppendLine("Total In All Accounts $3,900.00")
					.ToString(),
				actual: henry.GetStatement());
		}
		[TestMethod]
		public void TestOneAccount() =>
			Assert.AreEqual(
				expected: 1,
				actual: new Customer("Oscar")
					.OpenAccount(new Account(Account.AccountTypeEnum.SAVINGS))
					.GetNumberOfAccounts());
		[TestMethod]
		public void TestTwoAccount() =>
			Assert.AreEqual(
				expected: 2,
				actual: new Customer("Oscar")
					.OpenAccount(new Account(Account.AccountTypeEnum.SAVINGS))
					.OpenAccount(new Account(Account.AccountTypeEnum.CHECKING))
					.GetNumberOfAccounts());
		[TestMethod]
		public void TestThreeAccounts() =>
			Assert.AreEqual(
				expected: 3,
				actual: new Customer("Oscar")
					.OpenAccount(new Account(Account.AccountTypeEnum.SAVINGS))
					.OpenAccount(new Account(Account.AccountTypeEnum.CHECKING))
					.OpenAccount(new Account(Account.AccountTypeEnum.MAXI_SAVINGS))
					.GetNumberOfAccounts());
		[TestMethod]
		public void Transfer()
		{
			Account checkingAccount = new Account(Account.AccountTypeEnum.CHECKING),
				savingsAccount = new Account(Account.AccountTypeEnum.SAVINGS);
			Customer oscar = new Customer("Oscar")
				.OpenAccount(checkingAccount)
				.OpenAccount(savingsAccount);
			checkingAccount.Deposit(3000m);
			savingsAccount.Deposit(10000m);
			Assert.IsFalse(oscar.TryTransfer(1m, checkingAccount, new Account(Account.AccountTypeEnum.MAXI_SAVINGS))); // Prevent transfers to other customers accounts.
			Assert.IsFalse(oscar.TryTransfer(4000m, checkingAccount, savingsAccount)); // Prevent overdraft transfer
			Assert.IsTrue(oscar.TryTransfer(2000m, checkingAccount, savingsAccount));
			Assert.AreEqual(
				expected: 1000m,
				actual: checkingAccount.SumTransactions());
			Assert.AreEqual(
				expected: 12000m,
				actual: savingsAccount.SumTransactions());
		}
	}
}
