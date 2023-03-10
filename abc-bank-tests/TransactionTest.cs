using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Deposit()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.AreEqual(t.Type, "deposit");
        }

        [TestMethod]
        public void Withdrawal()
        {
            Transaction t = new Transaction(-5);
            //t instanceOf Transaction
            Assert.AreEqual(t.Type, "withdrawal");
        }
    }
}
