using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

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
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestRecentWithdrawals()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount);
            
            bool hasRecent;
            int daysToCheckfor = 10;
            checkingAccount.Deposit(1000.0);

            checkingAccount.Withdraw(200.0, DateTime.Now.AddDays(-15));          
            hasRecent = checkingAccount.hasRecentWithdrawals(daysToCheckfor);
            Assert.AreEqual(hasRecent, false);

            //check edge cases
            checkingAccount.Withdraw(200.0, DateTime.Now.AddDays(-11));
            hasRecent = checkingAccount.hasRecentWithdrawals(daysToCheckfor);
            Assert.AreEqual(hasRecent, false);

            checkingAccount.Withdraw(200.0, DateTime.Now.AddDays(-10));
            hasRecent = checkingAccount.hasRecentWithdrawals(daysToCheckfor);
            Assert.AreEqual(hasRecent, true);

            checkingAccount.Withdraw(200.0);
            hasRecent = checkingAccount.hasRecentWithdrawals(daysToCheckfor);
            Assert.AreEqual(hasRecent, true);


        }

        [TestMethod]
        public void TestTransfer()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(1000.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            var beforeChecking = checkingAccount.sumTransactions();
            var beforeSavings = savingsAccount.sumTransactions();

            Assert.AreEqual(1000, beforeChecking, DOUBLE_DELTA);
            Assert.AreEqual(3800, beforeSavings, DOUBLE_DELTA);

            henry.Transfer(checkingAccount, savingsAccount, 150);

            var afterChecking = checkingAccount.sumTransactions();
            var afterSavings = savingsAccount.sumTransactions();

            Assert.AreEqual(850, afterChecking, DOUBLE_DELTA);
            Assert.AreEqual(3950, afterSavings, DOUBLE_DELTA);

        }
    }
}
