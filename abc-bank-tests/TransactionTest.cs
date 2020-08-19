using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionConstructorShouldSetAmountAndTransactionDate()
        {
            var transAmt = 5;
            Transaction t = new Transaction(transAmt);
            Assert.AreEqual(transAmt, t.amount);
            Assert.IsTrue(DateTime.Now.AddSeconds(3) > t.transactionDate);
        }
    }
}
