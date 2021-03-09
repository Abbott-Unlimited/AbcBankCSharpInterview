using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-2;

        private Account checkingAccount;
        private Account savingsAccount;
        private Account maxiSavingsAccount;
        private Customer customer;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer("John");
            checkingAccount = new Account(AccountType.CHECKING, customer);
            savingsAccount = new Account(AccountType.SAVINGS, customer);
            maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, customer);
        }

        [TestMethod]
        public void TestCustomerSummary()
        {
            var bank = new Bank();
            customer.OpenAccount(checkingAccount);
            bank.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void TestCheckingAccount()
        {
            var bank = new Bank();
            customer.OpenAccount(checkingAccount);
            bank.AddCustomer(customer);
            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.10, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestSavingsAccount()
        {
            var bank = new Bank();
            bank.AddCustomer(customer.OpenAccount(savingsAccount));
            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.00, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        }
    }
}
