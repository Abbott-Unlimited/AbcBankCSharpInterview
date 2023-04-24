using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class SavingsAccountTest
    {
        [TestMethod]
        public void Interest_Earned_Should_Be_Correct() 
        {
            // Arrange
            var savingsAccount = new SavingsAccount();
            savingsAccount.RequestDeposit(3000.00m);
            
            decimal expected = 5.00m;

            // Act
            decimal actual = savingsAccount.GetInterestEarned();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
