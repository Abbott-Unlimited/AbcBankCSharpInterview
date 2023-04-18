using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace abc_bank_tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deposit()
        {
            // Arrange
            Account account = new Account(Account.CHECKING);

            // Act
            account.Deposit(-10.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Withdraw()
        {
            // Arrange
            Account account = new Account(Account.CHECKING);

            // Act
            account.Withdraw(-10.0);
        }
    }
}