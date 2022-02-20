using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(MockDateProvider.Instance, 5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
