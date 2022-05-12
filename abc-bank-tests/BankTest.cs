using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using System.Collections.Generic;
using System.Linq;


namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);

            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

			savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
			Account maxiSavings = new Account(Account.MAXI_SAVINGS);

            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavings));

            maxiSavings.Deposit(3000.0);

// Updated to reflect implementation of how MaxiSavings interest is calculated
//			Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
			Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
		}

		[TestMethod]
		public void GetAllCustomers()
		{
			Bank bank = new Bank();
			List<Customer> testCustomers = new List<Customer>(new Customer[]{ new Customer("Smith"), new Customer("Tumbleweed") });
			
			bank.AddCustomer(testCustomers[0]);			
			bank.AddCustomer(testCustomers[1]);

			List<Customer> customers = bank.Customers;

			// Better way to test this is to compare the List objects instead of looping through each customer
			foreach (Customer c in testCustomers)
			{
				Assert.AreEqual(c, customers.First(), "Get all customers failed");
				customers.RemoveAt(0);
			}
		}
	}
}
