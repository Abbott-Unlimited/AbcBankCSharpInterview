using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.TransactionTests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction_CreateInstance_InstanceIsTransactionType()
        {
            Transaction t = new Transaction(MockDateProvider.Instance, 5, Transaction.TransactionType.Deposit);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
