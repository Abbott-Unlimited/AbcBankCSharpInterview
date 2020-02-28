using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void DepositNonZeroPositiveAddsToTransactions()
        {
            IAccount account = new CheckingAccount();

            account.Deposit(200.00);

            Assert.AreEqual(200.00, account.GetTransactions()[0].GetAmount());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositZeroThrowsException()
        {
            IAccount account = new CheckingAccount();

            account.Deposit(0.00);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositNegativeThrowsException()
        {
            IAccount account = new CheckingAccount();

            account.Deposit(-200.00);

            Assert.Fail();
        }

        [TestMethod]
        public void WithdrawPositiveAddsToTransactions()
        {
            IAccount account = new CheckingAccount();

            account.Withdraw(200.00);

            Assert.AreEqual(-200.00, account.GetTransactions()[0].GetAmount());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawZeroThrowsException()
        {
            IAccount account = new CheckingAccount();

            account.Withdraw(0.00);

            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WithdrawNegativeThrowsException()
        {
            IAccount account = new CheckingAccount();

            account.Withdraw(-200.00);

            Assert.Fail();
        }

        //TODO: Functionality will change with added features
        [TestMethod]
        public void InterestEarnedCheckingIsOneTenthPercent()
        {
            IAccount account = new CheckingAccount();
            double amount = 200.00;

            account.Deposit(amount);

            Assert.AreEqual(amount * .001, account.GetInterestEarned());
        }

        //TODO: Functionality will change with added features
        [TestMethod]
        public void InterestEarnedSavingsIsOneTenthPercentForFirstThousand()
        {
            IAccount account = new SavingsAccount();
            double amount = 200.00;

            account.Deposit(amount);

            Assert.AreEqual(200.00 * .001, account.GetInterestEarned());
        }

        [TestMethod]
        public void InterestEarnedSavingsIsOneTenthPercentForFirstThousandThenTwoTenthsPercent()
        {
            IAccount account = new SavingsAccount();
            double amount = 2000.00;

            account.Deposit(amount);

            Assert.AreEqual(
                (1000.00 * .001)
                + (1000.00 * .002), account.GetInterestEarned());
        }

        [TestMethod]
        public void InterestEarnedMaxiSavingsIsTwoPercentForFirstThousand()
        {
            IAccount account = new MaxiSavingsAccount();
            double amount = 200.00;

            account.Deposit(amount);

            Assert.AreEqual(200.00 * .02, account.GetInterestEarned());
        }

        [TestMethod]
        public void InterestEarnedMaxiSavingsIsTwoPercentForFirstThousandThenFivePercentForNextThousand()
        {
            IAccount account = new MaxiSavingsAccount();
            double amount = 1800.00;

            account.Deposit(amount);

            Assert.AreEqual(
                (1000.00 * .02)
                + (800.00 * .05), account.GetInterestEarned());
        }

        [TestMethod]
        public void InterestEarnedMaxiSavingsIsTwoPercentForFirstThousandThenFivePercentForNextThousandThenTenPercent()
        {
            IAccount account = new MaxiSavingsAccount();
            double amount = 18000.00;

            account.Deposit(amount);

            Assert.AreEqual(
                (1000.00 * .02)
                + (1000.00 * .05)
                + (16000.00 * .10), account.GetInterestEarned());
        }


    }
}
