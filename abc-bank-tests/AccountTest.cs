using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void CreateChecking() {
            Account a = new Account(Account.AccountType.CHECKING);
            Assert.IsTrue(a.GetAccountType() == Account.AccountType.CHECKING);
        }

        [TestMethod]
        public void CreateSavings() {
            Account a = new Account(Account.AccountType.SAVINGS);
            Assert.IsTrue(a.GetAccountType() == Account.AccountType.SAVINGS);
        }

        [TestMethod]
        public void CreateMaxiSavings() {
            Account a = new Account(Account.AccountType.MAXI_SAVINGS);
            Assert.IsTrue(a.GetAccountType() == Account.AccountType.MAXI_SAVINGS);
        }

        [TestMethod]
        public void CheckingInterest() {
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            checkingAccount.Deposit(100.0M);

            Assert.AreEqual(0.1M, checkingAccount.InterestEarned());
        }

        [TestMethod]
        public void SavingsInterest() {
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);
            savingsAccount.Deposit(1500.0M);

            Assert.AreEqual(2.0M, savingsAccount.InterestEarned());
        }

        [TestMethod]
        public void MaxiSavingsInterest() {
            Account maxiSavingsAccount = new Account(Account.AccountType.MAXI_SAVINGS);
            maxiSavingsAccount.Deposit(3000.0M);

            Assert.AreEqual(150.0M, maxiSavingsAccount.InterestEarned());
        }
        

        [TestMethod]
        public void MaxiSavingsInterestRecentWithdrawl() {
            Account maxiSavingsAccount = new Account(Account.AccountType.MAXI_SAVINGS);

            maxiSavingsAccount.Deposit(3000.0M);
            maxiSavingsAccount.Withdraw(1000.0M);

            Assert.AreEqual(2.0M, maxiSavingsAccount.InterestEarned());
        }

        [TestMethod]
        public void RecentWithdrawl() {
            Account a = new Account(Account.AccountType.CHECKING);
            a.Deposit(1000M);
            a.Withdraw(100M);

            Assert.IsTrue(a.WithdrawlWithinGivenDays(10));
        }

        [TestMethod]
        public void NoRecentWithdrawls() {
            Account a = new Account(Account.AccountType.CHECKING);

            Assert.IsFalse(a.WithdrawlWithinGivenDays(10));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void DepositAmountUnderZero() {
            Account a = new Account(Account.AccountType.CHECKING);
            a.Deposit(-5M);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void WithdrawlAmountUnderZero(){
            Account a = new Account(Account.AccountType.CHECKING);
            a.Withdraw(-5M);
        }
    }
}
