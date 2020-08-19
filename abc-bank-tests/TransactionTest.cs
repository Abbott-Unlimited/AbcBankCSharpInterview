using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Constructor_AcceptsAmount()
        {
            Transaction gas = new Transaction(23.0);

            Assert.AreEqual(23, gas.Amount);
        }

        [TestMethod]
        public void Constructor_AcceptsDateTime()
        {
            DateTime yesterday = DateTime.Now.AddDays(-1);

            Transaction lunch = new Transaction(11.50, yesterday);

            Assert.AreEqual(yesterday, lunch.Date);
        }

        [TestMethod]
        public void Constructor_DefaultsDateToNow()
        {
            Transaction coffee = new Transaction(2.75);

            // Without mocking DateTime.Now, trying to make a DateTime comparison
            // to the time when the transaction was instantiated (even if it was just
            // the line above) could lead to test flakiness. To try and avoid this,
            // the comparsion here is broadened to just date. (The best solution would
            // still be to mock DateTime.Now so there is full control over it.)
            Assert.AreEqual(DateTime.Now.Date, coffee.Date.Date);
        }
    }
}
