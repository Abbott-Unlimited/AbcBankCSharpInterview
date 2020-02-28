using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void DepositIsDeposit()
        {
            ITransaction t = new Transaction(5);

            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }


        [TestMethod]
        public void ToStringDepositFormattedAbsoluteValueWithDollarsign()
        {
            ITransaction t = new Transaction(20D);

            string dollars = t.ToString();

            Assert.AreEqual("  deposit $20.00\n", dollars);
        }

        [TestMethod]
        public void ToStringWithdrawalFormattedAbsoluteValueWithDollarsign()
        {
            ITransaction t = new Transaction(-20D);

            string dollars = t.ToString();

            Assert.AreEqual("  withdrawal $20.00\n", dollars);
        }
    }
}
