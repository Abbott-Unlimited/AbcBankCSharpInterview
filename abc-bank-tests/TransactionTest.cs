using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5M);
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }

        [TestMethod]
        public void TransactionGetDate()
        {
            DateTime now = DateProvider.GetInstance().Now();
            Transaction t = new Transaction(5M, now);
            Assert.IsTrue(t.GetType() == typeof(Transaction));
            Assert.IsTrue(t.GetDate() == now);
        }

        [TestMethod]
        public void TransactionGetAmount()
        {
            DateTime now = DateProvider.GetInstance().Now();
            Transaction t = new Transaction(5M, now);
            Assert.IsTrue(t.GetType() == typeof(Transaction));
            Assert.IsTrue(t.GetAmount() == 5m);
        }
    }
}
