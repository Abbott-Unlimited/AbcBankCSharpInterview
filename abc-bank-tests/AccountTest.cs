using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {

        [TestMethod]
        public void TestMaxiInterestRates()
        {
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);

            maxiAccount.Deposit(100.0);

            Assert.AreEqual(5, maxiAccount.InterestEarned());
        }

        [TestMethod]
        public void TestSavingsAbove1000InterestRates()
        {
            Account savingsAccount = new Account(Account.SAVINGS);

            savingsAccount.Deposit(2000.0);

            Assert.AreEqual(3, savingsAccount.InterestEarned());
        }

        [TestMethod]
        public void TestSavingsUnder1000InterestRates()
        {
            Account savingsAccount = new Account(Account.SAVINGS);

            savingsAccount.Deposit(1000.0);

            Assert.AreEqual(1, savingsAccount.InterestEarned());
        }

        [TestMethod]
        public void TestCheckingInterestRates()
        {
            Account checkingAccount = new Account(Account.CHECKING);

            checkingAccount.Deposit(1000.0);

            Assert.AreEqual(1, checkingAccount.InterestEarned());
        }

        [TestMethod]
        public void TestAccountNegativeDepositError()
        {
            Account checkingAccount = new Account(Account.CHECKING);

            try
            {
                checkingAccount.Deposit(-10.0);
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("amount must be greater than zero", ex.Message);
            }
        }

        [TestMethod]
        public void TestAccountNegativeWithdrawError()
        {
            Account checkingAccount = new Account(Account.CHECKING);

            try
            {
                checkingAccount.Withdraw(-10.0);
                Assert.Fail(); // If it gets to this line, no exception was thrown
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("amount must be greater than zero", ex.Message);
            }
        }
    }
}
