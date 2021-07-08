using System;
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
            Assert.AreEqual(2.95, InterestCalculations.TotalInterestEarned(GetCustomerWithAccountForTest()), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedCheckingTest()
        {
            Assert.AreEqual(.3, InterestCalculations.InterestEarned(GetCheckingAccountForTest()), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedSavingsTest()
        {
            Assert.AreEqual(.6, InterestCalculations.InterestEarned(GetSavingsAccountForTest()), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedMaxiSavingsTest()
        {
            Assert.AreEqual(2.05, InterestCalculations.InterestEarned(GetMaxiSavingsAccountForTest()), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedMaxiSavingsWithdrawTenDaysOrLessTest()
        {
            var maxi = GetMaxiSavingsAccountForAgingTest();

            var tran = new Transaction(-200.00);

            var newTran = new PrivateObject(tran);

            newTran.SetField("transactionDate", DateTime.Now.AddDays(-10));

            maxi.Transactions.Add(tran);

            Assert.AreEqual(2, InterestCalculations.InterestEarned(maxi), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestedEarnedMaxiSavingsWithdrawGreaterThanTenDaysTest()
        {
            var maxi = GetMaxiSavingsAccountForAgingTest();

            var tran = new Transaction(-200.00);

            var newTran = new PrivateObject(tran);

            newTran.SetField("transactionDate", DateTime.Now.AddDays(-11));

            maxi.Transactions.Add(tran);

            Assert.AreEqual(100, InterestCalculations.InterestEarned(maxi), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SumTransactionsTest()
        {
            Assert.AreEqual(300, InterestCalculations.SumTransactions(GetCheckingAccountForTest()),  DOUBLE_DELTA);
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

        private Account GetMaxiSavingsAccountForAgingTest()
        {
            var a = new Account(AccountTypeEnum.MAXI_SAVINGS);
            a.Transactions.Add(new Transaction(2200.00));
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
