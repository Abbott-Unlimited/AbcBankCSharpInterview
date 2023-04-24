using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class MaxiSavingsAccountTest
    {
        [TestMethod]
        public void Interest_Earned_Should_Be_Correct() 
        {
            // Arrange
            var maxiSavingsAccount = new MaxiSavingsAccount();
            maxiSavingsAccount.RequestDeposit(4000.00m);
            
            decimal expected = 27.00m;

            // Act
            decimal actual = maxiSavingsAccount.GetInterestEarned();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
