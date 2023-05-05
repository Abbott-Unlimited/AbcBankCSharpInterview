using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TestTransactionDepositType()
        {
            Transaction t = new Transaction(5, Transaction.DEPOSIT);
            int expectedTransactionType = Transaction.DEPOSIT;
            int actualTransactionType = t.GetTransactionType();
            Assert.IsTrue(expectedTransactionType == actualTransactionType);
        }

        [TestMethod]
        public void TestTransactionWithdrawalType()
        {
            Transaction t = new Transaction(5, Transaction.WITHDRAWAL);
            int expectedTransactionType = Transaction.WITHDRAWAL;
            int actualTransactionType = t.GetTransactionType();
            Assert.IsTrue(expectedTransactionType == actualTransactionType);
        }

    }
}
