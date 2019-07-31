﻿using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            var checkingAccount = new Account(Type.Checking);
            var savingsAccount = new Account(Type.Savings);

            var customer = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", customer.GetStatement());
        }

        [TestMethod]
        public void TestOneAccountsOpened()
        {
            var customer = new Customer("Oscar").OpenAccount(new Account(Type.Savings));
            Assert.AreEqual(1, customer.Accounts.Count);
        }

        [TestMethod]
        public void TestMultipleAccountsOpened()
        {
            var customer = new Customer("Oscar").OpenAccount(new Account(Type.Savings));
            customer.OpenAccount(new Account(Type.Checking));
            customer.OpenAccount(new Account(Type.Maxi_Savings));
            Assert.AreEqual(3, customer.Accounts.Count);
        }

        [TestMethod]
        public void TestMultipleSavingsAccountsOpened()
        {
            var customer = new Customer("Oscar").OpenAccount(new Account(Type.Savings));
            customer.OpenAccount(new Account(Type.Savings));
            Assert.AreEqual(2, customer.Accounts.Count);
        }
    }
}