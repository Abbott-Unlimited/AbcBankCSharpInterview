using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountWithdrawalTest
    {
        [TestMethod]
        public void Should_Withdraw_Money_From_Account()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(2000m);
            checkingAccount.RequestWithdrawal(200m);

            decimal expected = 1800m;

            // Act
            decimal actual = checkingAccount.SumTransactions();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Not_Withdraw_Money_in_Account_For_Zero_Amount()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(2000m);
            checkingAccount.RequestWithdrawal(0m);

            decimal expected = 2000m;

            // Act
            decimal actual = checkingAccount.SumTransactions();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Fail_When_Withdrawing_Zero_Money_From_Account()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(2000m);

            bool expected = false;

            // Act
            var transactionResult = checkingAccount.RequestWithdrawal(0m);
            bool actual = transactionResult.Success;

            // Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Should_Not_Withdraw_Money_in_Account_When_Withdrawal_Greater_Than_Balance()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(1000m);
            checkingAccount.RequestWithdrawal(1001m);

            decimal expected = 1000m;

            // Act
            decimal actual = checkingAccount.SumTransactions();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Fail_When_Withdrawing_More_Money_Than_In_Account()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(1000m);

            bool expected = false;

            // Act
            var transactionResult = checkingAccount.RequestWithdrawal(1001m);
            bool actual = transactionResult.Success;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
