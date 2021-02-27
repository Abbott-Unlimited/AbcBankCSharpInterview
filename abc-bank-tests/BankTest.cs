using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
		[TestClass]
		public class BankTest
		{
				private static readonly double DOUBLE_DELTA = 1e-15;

				[TestMethod]
				public void TestCustomerSummary() 
				{
						Bank bank = new Bank();
						Customer john = new Customer("John");
						john.OpenAccount(new Account(Account.CHECKING));
						bank.AddCustomer(john);

						Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
				}

				[TestMethod]
				public void TestCheckingAccount() 
				{
						Bank bank = new Bank();
						Account checkingAccount = new Account(Account.CHECKING);
						Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
						bank.AddCustomer(bill);

						checkingAccount.Deposit(100.0);

						Assert.AreEqual(0.1, checkingAccount.InterestEarned(), DOUBLE_DELTA);
				}

				[TestMethod]
				public void TestSavingsAccount() 
				{
						Bank bank = new Bank();
						Account savingsAcount = new Account(Account.SAVINGS);
						bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAcount));

						savingsAcount.Deposit(1500.0);

						Assert.AreEqual(2.0, savingsAcount.InterestEarned(), DOUBLE_DELTA);
				}

				[TestMethod]
				public void TestMaxiSavingsAccount() 
				{
						Bank bank = new Bank();
						Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
						bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

						maxiSavingsAccount.Deposit(3000.0);

						Assert.AreEqual(150.0, maxiSavingsAccount.InterestEarned(), DOUBLE_DELTA);

						maxiSavingsAccount.Withdraw(100.0);

						Assert.AreEqual(2.9, maxiSavingsAccount.InterestEarned(), DOUBLE_DELTA);
				}

				[TestMethod]
				public void TestGetFirstCustomer()
				{
						Bank bank = new Bank();
						Customer john = new Customer("John");
						Customer bill = new Customer("Bill");
						Customer henry = new Customer("Henry");

						Assert.AreEqual("No customers", bank.GetFirstCustomer());

						bank.AddCustomer(john);
						bank.AddCustomer(bill);
						bank.AddCustomer(henry);

						Assert.AreEqual("John", bank.GetFirstCustomer());
				}

				[TestMethod]
				public void TestTotalInterestPaid()
				{
						Bank bank = new Bank();

						Account johnMaxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
						bank.AddCustomer(new Customer("John").OpenAccount(johnMaxiSavingsAccount));

						Account billCheckingAccount = new Account(Account.CHECKING);
						bank.AddCustomer(new Customer("Bill").OpenAccount(billCheckingAccount));

						johnMaxiSavingsAccount.Deposit(1500.0);
						billCheckingAccount.Deposit(3000.0);

						Assert.AreEqual(78.0, bank.TotalInterestPaid(), DOUBLE_DELTA);
				}
		}
}
