using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void AccountType()
        {
            var account = new Account(Account.SAVINGS);

            Assert.AreEqual(Account.SAVINGS, account.GetAccountType());
        }

        [TestMethod]
        public void DepositWithdrawlTotals()
        {
            var account = new Account(Account.SAVINGS);
            account.Deposit(100);
            account.Deposit(100);
            account.Withdraw(25);

            Assert.AreEqual(175, account.GetBalance());
        }

        [TestMethod]
        public void DepositWithdrawlTotalNegative()
        {
            var account = new Account(Account.SAVINGS);
            account.Deposit(100);
            account.Withdraw(120);

            Assert.AreEqual(-20, account.GetBalance());
        }

        [TestMethod]
        public void InterestEarnedChecking()
        {
            var account = new Account(Account.CHECKING);
            account.Deposit(100);
            account.Deposit(50);

            Assert.AreEqual(0.15, account.InterestEarned());
        }

        [TestMethod]
        public void InterestEarnedSavingsBelow1000()
        {
            var account = new Account(Account.SAVINGS);
            account.Deposit(1000);

            Assert.AreEqual(1.0, account.InterestEarned());
        }

        [TestMethod]
        public void InterestEarnedSavingsOver1000()
        {
            var account = new Account(Account.SAVINGS);
            account.Deposit(1000);
            account.Deposit(1000);

            Assert.AreEqual(3.0, account.InterestEarned());
        }

/*        [TestMethod]
        [Ignore]
        public void InterestEarnedMaxiBelow1000()
        {
            var account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(1000);

            Assert.AreEqual(20.0, account.InterestEarned());
        }*/

/*        [TestMethod]
        [Ignore]
        public void InterestEarnedMaxiAbove1000Below2000()
        {
            var account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(1000);
            account.Deposit(500);

            Assert.AreEqual(20.0 + 25.0, account.InterestEarned());
        }*/

/*        [TestMethod]
        [Ignore]
        public void InterestEarnedMaxiAbout2000()
        {
            var account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(1000);
            account.Deposit(500);
            account.Deposit(1000);

            Assert.AreEqual(20.0 + 50.0 + 50.0, account.InterestEarned());
        }*/

        [TestMethod]
        public void InterestEarnedMaxiWithdralInPast10Days()
        {
            var account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(1100);
            account.Withdraw(100);

            Assert.AreEqual(1.0, account.InterestEarned());
        }

        [TestMethod]
        public void InterestEarnedMaxiNoWithdralInPast10Days()
        {
            var account = new Account(Account.MAXI_SAVINGS);
            var date = DateTime.Now.AddDays(-11);
            var deposit = new Transaction(1000.0, date);
            account.AddTransaction(deposit);

            var date2 = DateTime.Now.AddDays(-11);
            var withdrawl = new Transaction(-500.0, date2);
            account.AddTransaction(withdrawl);

            Assert.AreEqual(25.0, account.InterestEarned());
        }

        [TestMethod]
        public void InterestEarnedNoTransactions()
        {
            var account = new Account(Account.MAXI_SAVINGS);

            Assert.AreEqual(0, account.InterestEarned());
        }

    }
}
