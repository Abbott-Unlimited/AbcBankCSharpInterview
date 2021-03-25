using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        /// <summary>
        /// Tests that balance is correctly calculated when a deposit and withdrawal are made.
        /// </summary>
        [TestMethod]
        public void Balance()
        {
            Account account = new Account(AccountType.CHECKING);
            Assert.AreEqual(account.Balance, 0.00m);

            account.Deposit(100.00m);
            Assert.AreEqual(account.Balance, 100.00m);

            account.Withdraw(30.00m);
            Assert.AreEqual(account.Balance, 70.00m);
        }

        /// <summary>
        /// Test that a withdrawal for more than the account balance is not allowed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RejectWithdrawal()
        {
            Account account = new Account(AccountType.CHECKING);
            account.Deposit(100.00m);
            account.Withdraw(150.00m);
        }
    }
}
