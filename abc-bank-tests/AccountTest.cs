using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestCategory("Deposit"), TestMethod]
        public void DepositPositive()
        {
            Account account = new Account(Account.CHECKING);

            account.Deposit(1000m);

            Assert.AreEqual(1000m, account.sumTransactions());
        }

        [TestCategory("Deposit"), TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void DepositNegative()
        {
            Account account = new Account(Account.CHECKING);
            
            account.Deposit(-1000m);
        }

        [TestCategory("Withdraw"), TestMethod]
        public void WithdrawPositive()
        {
            Account account = new Account(Account.CHECKING);

            account.Withdraw(1000m);

            Assert.AreEqual(-1000m, account.sumTransactions());
        }

        [TestCategory("Withdraw"), TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void WithdrawNegative()
        {
            Account account = new Account(Account.CHECKING);
            
            account.Withdraw(-1000m);
        }
    }
}