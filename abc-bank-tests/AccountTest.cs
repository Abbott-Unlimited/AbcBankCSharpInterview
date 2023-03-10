using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestOverdraft()
        {
            var a = new CheckingAccount();
            a.Deposit(100);
            a.Withdraw(101);
        }

        [TestMethod]
        public void TestDeposit()
        {
            var a = new CheckingAccount();
            a.Deposit(101);
            a.Deposit(101);
            a.Deposit(101);

            Assert.AreEqual(303, a.CurrentBalance);
        }

        [TestMethod]
        public void TestWithdrawal()
        {
            var a = new CheckingAccount();
            a.Deposit(100);
            a.Withdraw(99);

            Assert.AreEqual(1, a.CurrentBalance);
        }

        [TestMethod]
        public void TestAccountWithNoTransactions()
        {
            var a = new CheckingAccount();
            Assert.AreEqual(0, a.CurrentBalance);
            Assert.AreEqual(0, a.CalculateSimpleInterest());
        }
    }
}
