using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    /// <summary>
    /// Transaction Test Object
    /// </summary>
    [TestClass]
    public class TransactionTest
    {
        /// <summary>
        /// Transaction instance.
        /// </summary>
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
