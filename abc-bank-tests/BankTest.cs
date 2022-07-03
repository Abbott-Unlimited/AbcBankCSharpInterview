﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    /// <summary>
    /// Bank Test Class
    /// </summary>
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        /// <summary>
        /// Test Customer Summary.
        /// </summary>
        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        /// <summary>
        /// Test the Checking account
        /// </summary>
        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        /// <summary>
        /// Test the Savings Account.
        /// </summary>
        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        /// <summary>
        /// Test the Maxi Savings Account. 
        /// </summary>
        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(130, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
