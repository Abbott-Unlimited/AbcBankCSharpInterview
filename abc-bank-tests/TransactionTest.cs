using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABC_bank;

namespace ABC_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
