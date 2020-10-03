using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestAppChecking()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.0);
            checkingAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total -$100.00\n" +
                    "\n" +
                    "Total In All Accounts -$100.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestAppSavings()
        {
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(savingsAccount);

            savingsAccount.Deposit(1000.0);
            savingsAccount.Withdraw(500.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $1,000.00\n" +
                    "  withdrawal $500.00\n" +
                    "Total $500.00\n" +
                    "\n" +
                    "Total In All Accounts $500.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestAppMaxiSavings()
        {
            Account maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(maxiSavingsAccount);

            maxiSavingsAccount.Deposit(5000.0);
            maxiSavingsAccount.Withdraw(6000.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Maxi Savings Account\n" +
                    "  deposit $5,000.00\n" +
                    "  withdrawal $6,000.00\n" +
                    "Total -$1,000.00\n" +
                    "\n" +
                    "Total In All Accounts -$1,000.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);
            Account maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount)
                .OpenAccount(savingsAccount)
                .OpenAccount(maxiSavingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);
            maxiSavingsAccount.Deposit(500.0);
            maxiSavingsAccount.Withdraw(600.0);

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
                    "Maxi Savings Account\n" +
                    "  deposit $500.00\n" +
                    "  withdrawal $600.00\n" +
                    "Total -$100.00\n" +
                    "\n" +
                    "Total In All Accounts $3,800.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestAppNoDupeCalculations()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Total In All Accounts $100.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(AccountType.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(AccountType.SAVINGS));
            oscar.OpenAccount(new Account(AccountType.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(AccountType.SAVINGS));
            oscar.OpenAccount(new Account(AccountType.CHECKING));
            oscar.OpenAccount(new Account(AccountType.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestNoDupeAccounts()
        {
            var account = new Account(AccountType.CHECKING);
            Customer oscar = new Customer("Oscar").OpenAccount(account);
            oscar.OpenAccount(account);
            oscar.OpenAccount(account);
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }
    }
}
