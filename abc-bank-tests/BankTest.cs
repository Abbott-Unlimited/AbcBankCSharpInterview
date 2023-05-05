using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0M);
            //The Amount is too small to put here in a readable fashion
            Assert.AreEqual(100.0M * (0.001m / 365), bank.totalInterestPaid());
        }

        [TestMethod]
        public void Savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0M);

            Assert.AreEqual(1 + ((1500.0M - 1000) * (0.002M / 365)), bank.totalInterestPaid());
        }

        [TestMethod]
        public void Maxi_savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0M);

            Assert.AreEqual(3000.0M * (0.001m / 365), bank.totalInterestPaid());
        }

        [TestMethod]
        public void Maxi_savings_tendays_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0M, DateTime.Now.AddDays(-12));

            Assert.AreEqual(3000.0M * (0.05m / 365), bank.totalInterestPaid());
        }
    }
}