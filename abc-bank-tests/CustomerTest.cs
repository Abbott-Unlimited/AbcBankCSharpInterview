using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        Account checkingAccount;
        Account savingsAccount;
        Account maxiSavingsAccount;
        Customer customer;

        [TestInitialize]
        public void Setup()
        {
            customer = new Customer("Henry");
            checkingAccount = new Account(AccountType.CHECKING, customer);
            savingsAccount = new Account(AccountType.SAVINGS, customer);
            maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, customer);
        }

        [TestMethod]
        public void TestApp()
        {
            customer.OpenAccount(checkingAccount).OpenAccount(savingsAccount);

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
        public void TestOneAccount()
        {
            customer.OpenAccount(savingsAccount);
            Assert.AreEqual(1, customer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(checkingAccount);
            Assert.AreEqual(2, customer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            customer.OpenAccount(savingsAccount);
            customer.OpenAccount(checkingAccount);
            customer.OpenAccount(maxiSavingsAccount);
            Assert.AreEqual(3, customer.GetNumberOfAccounts());
        }
    }
}
