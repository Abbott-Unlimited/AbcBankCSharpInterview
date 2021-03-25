using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        /// <summary>
        /// Tests that the transaction amount is stored correctly.
        /// </summary>
        [TestMethod]
        public void TestTransaction()
        {
            Transaction t = new Transaction(5);
            Assert.AreEqual(5, t.Amount);
        }
    }
}
