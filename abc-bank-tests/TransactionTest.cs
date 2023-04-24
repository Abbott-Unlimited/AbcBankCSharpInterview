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
            
            Transaction transaction = new Transaction(5);

            Assert.IsTrue(transaction.GetType() == typeof(Transaction));
        }
    }
}
