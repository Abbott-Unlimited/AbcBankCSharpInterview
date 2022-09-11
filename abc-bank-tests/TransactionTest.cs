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
            //Transaction t = new Transaction(5);
            var acct = new Account(Account.SAVINGS);
            Transaction t = new Deposit(DateTime.Now, acct, 5.00);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Deposit));  // Gets type at COMPILATION time
        }
    }
}
