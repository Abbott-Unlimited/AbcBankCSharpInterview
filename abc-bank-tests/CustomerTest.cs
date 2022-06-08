using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            // I simplified the Assert by creating variables for the two arguments
            const string TestData = "Statement for Henry\n" +
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
                    "Total In All Accounts $3,900.00";

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);
            savingsAccount.Withdraw(200.0M);

            var henryStatement = henry.GetStatement();

            Assert.AreEqual(TestData, henryStatement);
        }

        [TestMethod]
        public void TestTransfer()
        {
            const string TestData = "Statement for Logan\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  deposit $400.00\n" +
                    "Total $500.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $400.00\n" +
                    "Total $3,600.00\n" +
                    "\n" +
                    "Total In All Accounts $4,100.00";

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer logan = new Customer("Logan").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);
            logan.Transfer(savingsAccount, checkingAccount, 400.0M);

            var loganStatement = logan.GetStatement();

            Assert.AreEqual(TestData, loganStatement);
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [Ignore]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
