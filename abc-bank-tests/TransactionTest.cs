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
            var t = new Transaction(5, new DateTime(), abc_bank.Transaction.TransactionTypeEnum.Deposit);
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
