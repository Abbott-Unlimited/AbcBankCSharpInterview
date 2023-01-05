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
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);
            savingsAccount.Withdraw(200.0M);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $-200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
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
        [TestMethod]
        public void TestTransfer()
        {
            Account checkingAccountBob = new Account(Account.CHECKING);
            Account savingsAccountBob = new Account(Account.SAVINGS);
            Customer bob = new Customer("Bob").OpenAccount(checkingAccountBob);
            bob.OpenAccount(savingsAccountBob);

            checkingAccountBob.Deposit(100.0M);
            savingsAccountBob.Deposit(4000.0M);
            bob.Transfer(100M, savingsAccountBob, checkingAccountBob);

            Assert.AreEqual("Statement for Bob\n" +
             "\n" +
             "Checking Account\n" +
             "  deposit $100.00\n" +
             "  deposit $100.00\n" +
             "Total $200.00\n" +
             "\n" +
             "Savings Account\n" +
             "  deposit $4,000.00\n" +
             "  withdrawal $-100.00\n" +
             "Total $3,900.00\n" +
             "\n" +
             "Total In All Accounts $4,100.00", bob.GetStatement());
        }
    }
}
