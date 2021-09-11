using System;
using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        private const double DoubleDelta = 1e-15;

        [TestMethod]
        public void MaxiSaverAccountInterestFail()
        {
            var customer = new Customer("Fred");
            var account = new Account(AccountType.MaxiSavings);
            customer.OpenAccount(account);
            account.Deposit(100.0, DateTime.Now);
            account.Deposit(100.0, DateTime.Now);
            account.Withdraw(100.0, DateTime.Now);
            Assert.AreNotEqual(5.0, account.InterestEarned());
        }

        /* Test for annual accrual below supersedes this test.
        [TestMethod]
        public void MaxiSaverAccountInterestSucceed()
        {
            var customer = new Customer("Fred");
            var account = new Account(AccountType.MaxiSavings);
            customer.OpenAccount(account);
            account.Deposit(100.0, DateTime.Now);
            account.Deposit(100.0, DateTime.Now);
            account.Withdraw(100.0, DateTime.MinValue);

            Assert.AreEqual(5.0, account.InterestEarned());
        } */

        [TestMethod]
        public void AccountAgeFail()
        {
            var customer = new Customer("Fred");
            var account = new Account(AccountType.Savings);
            var openDate = account.GetAccountAge().Days;
            customer.OpenAccount(account);
            Assert.AreNotEqual(openDate, 365);
        }

        [TestMethod]
        public void InterestAccrue()
        {
            var customer = new Customer("Tom");
            var account = new Account(AccountType.MaxiSavings);
            customer.OpenAccount(account);
            // This way we should always have the account opened yesterday for testing.
            account.SetAccountAge(1);
            account.Deposit(100, DateTime.Parse("09-10-2021"));
            Assert.AreEqual(0.014, account.InterestEarned(), DoubleDelta);
        }
    }
}