using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void Deposit_AddsTransactionIncreasingAccountBalance()
        {
            Account savings = new Account(Account.CHECKING);

            savings.Deposit(2000.0);

            Assert.AreEqual(2000.0, savings.sumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deposit_RejectsNegativeAmount()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(1000.0);

            checking.Deposit(-500.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Deposit_RejectsAmountOfZero()
        {
            Account savings = new Account(Account.SAVINGS);
            savings.Deposit(500.0);

            savings.Deposit(0);
        }

        [TestMethod]
        public void Withdraw_AddsTransactionDecreasingAccountBalance()
        {
            Account savings = new Account(Account.SAVINGS);
            savings.Deposit(500.0);

            savings.Withdraw(250.0);

            Assert.AreEqual(250.0, savings.sumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Withdraw_RejectsNegativeAmount()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(250.0);

            checking.Withdraw(-100.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Withdraw_RejectsAmountOfZero()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(500.0);

            checking.Withdraw(0.0);
        }

        [TestMethod]
        public void InterestEarned_ForCheckingAccount_IsPointOnePercent()
        {
            Account checking = new Account(Account.CHECKING);
            checking.Deposit(3000.0);

            double result = checking.InterestEarned();

            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void InterestEarned_ForSavingsAccount_IsPointOnePercentForAmountsOfOneThousandOrLess()
        {
            Account savings = new Account(Account.SAVINGS);
            savings.Deposit(1000.0);

            double result = savings.InterestEarned();

            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void InterestEarned_ForSavingsAccount_IsPointTwoPercentForAnyAmountOverTheFirstThousand()
        {
            Account savings = new Account(Account.SAVINGS);
            savings.Deposit(2000.0);

            double result = savings.InterestEarned();

            Assert.AreEqual(3.0, result);
        }

        [TestMethod]
        public void InterestEarned_ForMaxiSavings_IsTwoPercentForAmountsOfOneThousandOrLess()
        {
            Account maxi = new Account(Account.MAXI_SAVINGS);
            maxi.Deposit(100.0);

            double result = maxi.InterestEarned();

            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void InterestEarned_ForMaxiSavings_IsFivePercentForTheSecondThousand()
        {
            Account maxi = new Account(Account.MAXI_SAVINGS);
            maxi.Deposit(1200.0);

            double result = maxi.InterestEarned();

            // 2% of the first $1000 = $20, plus
            // 5% of the additional $200 = $10
            Assert.AreEqual(30.0, result);
        }

        [TestMethod]
        public void InterestEarned_ForMaxiSavings_IsTenPercentForTheAmountAfterTheFirstTwoThousand()
        {
            Account maxi = new Account(Account.MAXI_SAVINGS);
            maxi.Deposit(2300.0);

            double result = maxi.InterestEarned();

            // 2% of the first $1000 = $20, plus
            // 5% of the second $1000 = $50, plus
            // 10% of the additional $300 = $30
            Assert.AreEqual(100.0, result);
        }
    }
}
