using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CheckingAccountTest
    {
        [TestMethod]
        public void Interest_Earned_Should_Be_Correct_When_Balance_Under_1000() 
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(500.00m);
            
            decimal expected = 0.5m;

            // Act
            decimal actual = checkingAccount.GetInterestEarned();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Interest_Earned_Should_Be_Correct_When_Balance_Over_1000()
        {
            // Arrange
            var checkingAccount = new CheckingAccount();
            checkingAccount.RequestDeposit(1500.00m);

            decimal expected = 1.5m;

            // Act
            decimal actual = checkingAccount.GetInterestEarned();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
