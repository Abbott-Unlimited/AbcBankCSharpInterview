using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Entities;
using abc_bank.Helpers;
using abc_bank.Enums;

namespace abc_bank_tests
{
    [TestClass]
    public class InterestCalculationsTests
    {

        private static readonly double DOUBLE_DELTA = 1e-15;
        [TestMethod]
        public void TotalInterestedEarnedTest()
        {
            Assert.AreEqual(InterestCalculations.TotalInterestEarned(GetCustomerWithAccountForTest()), 75.9, DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedCheckingTest()
        {
            Assert.AreEqual(InterestCalculations.InterestEarned(GetCheckingAccountForTest()), .3, DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedSavingsTest()
        {
            Assert.AreEqual(InterestCalculations.InterestEarned(GetSavingsAccountForTest()), .6, DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedMaxiSavingsTest()
        {
            Assert.AreEqual(InterestCalculations.InterestEarned(GetMaxiSavingsAccountForTest()), 75, DOUBLE_DELTA);
        }

        [TestMethod]
        public void SumTransactionsTest()
        {
            Assert.AreEqual(InterestCalculations.SumTransactions(GetCheckingAccountForTest()), 300, DOUBLE_DELTA);
        }

        private Account GetCheckingAccountForTest()
        {
            var a = new Account(AccountTypeEnum.CHECKING);
            a.Transactions.Add(new Transaction(450.00));
            a.Transactions.Add(new Transaction(-150.00));
            return a;
        }

        private Account GetSavingsAccountForTest()
        {
            var a = new Account(AccountTypeEnum.SAVINGS);
            a.Transactions.Add(new Transaction(750.00));
            a.Transactions.Add(new Transaction(-150.00));
            return a;
        }

        private Account GetMaxiSavingsAccountForTest()
        {
            var a = new Account(AccountTypeEnum.MAXI_SAVINGS);
            a.Transactions.Add(new Transaction(2200.00));
            a.Transactions.Add(new Transaction(-150.00));
            return a;
        }

        private Customer GetCustomerWithAccountForTest()
        {
            var c = new Customer("Jim");
            c.Accounts.Add(GetCheckingAccountForTest());
            c.Accounts.Add(GetSavingsAccountForTest());
            c.Accounts.Add(GetMaxiSavingsAccountForTest());
            return c;
        }

        // The below test scenario is negated by using an Enum for the Type//
        //[TestMethod]
        //public void Transaction()
        //{
        //    Transaction t = new Transaction(5);
        //    //t instanceOf Transaction
        //    Assert.IsTrue(t.GetType() == typeof(Transaction));
        //}
    }
}
