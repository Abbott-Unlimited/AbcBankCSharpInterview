using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class AddTransactionShould
    {
        [TestMethod]
        public void AddNewTransaction_IfAmountGreaterThanZero()
        {
            var transAmt = 1;
            var sut = new Account(AccountType.CHECKING);

            sut.AddTransaction(transAmt);

            Assert.AreEqual(transAmt, sut.transactions.First().amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Throw_IfAmountIsZero()
        {
            var transAmt = 0;
            var sut = new Account(AccountType.CHECKING);

            sut.AddTransaction(transAmt);
        }

        [TestMethod]
        public void Throw_IfAmountIsLessThanZero()
        {
            var transAmt = -1;
            var sut = new Account(AccountType.CHECKING);

            sut.AddTransaction(transAmt);
            Assert.AreEqual(transAmt, sut.transactions.First().amount);
        }
    }
}
