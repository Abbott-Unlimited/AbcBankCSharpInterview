using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountDepositTest
    {
        [TestMethod]
        public void Should_Deposit_Money_in_Account()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(100.12m);

            decimal expected = 100.12m;

            // Act
            decimal actual = checkingAccount.SumTransactions();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Not_Deposit_Money_in_Account_For_Zero_Amount()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(0m);

            decimal expected = 0m;

            // Act
            decimal actual = checkingAccount.SumTransactions();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Should_Fail_When_Depositing_Zero_Money_in_Account()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();

            bool expected = false;

            // Act
            var transactionResult = checkingAccount.RequestDeposit(0m);
            bool actual = transactionResult.Success;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
