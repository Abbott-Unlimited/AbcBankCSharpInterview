using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-3;

        [TestMethod]
        public void CanDepositToAccount()
        {
            Account account = new CheckingAccount();
            account.Deposit(100.555, false);
            Assert.AreEqual(100.555, account.Ballance);
        }
        [TestMethod]
        public void CanWithdrawFromAccount()
        {
            Account account = new CheckingAccount();
            account.Deposit(200, false);
            account.Withdraw(100, false);
            Assert.AreEqual(100, account.Ballance);
        }
        [TestMethod]
        public void InterestEarnedCheckingTest()
        {
            Account account = new CheckingAccount();
            account.Deposit(1000, false);
            Assert.AreEqual(1.00, account.InterestEarned());
        }

        //[TestMethod]
        //public void InterestEarnedSavingTest()
        //{
        //    Account account = new SavingAccount();
        //    account.Deposit(2000, false);
        //    Assert.AreEqual(3.00, account.InterestEarned());
        //}

        //[TestMethod]
        //public void InterestEarnedMaxiSavingTest()
        //{
        //    Account account = new MaxiSavingAccount();
        //    account.Deposit(3000, false);
        //    Assert.AreEqual(170.00, account.InterestEarned());
        //}

        [TestMethod]
        public void InterestEarnedMaxiSavingNewTest()
        {
            Account account = new MaxiSavingAccount();
            account.Deposit(3000, false);
            Assert.AreEqual(3.00, account.InterestEarned());
        }

        [TestMethod]
        public void NoTransactionsPast10DaysInterestEarnedMaxiSavingNewTest()
        {
            Account account = new MaxiSavingAccount();
            account.Deposit(3000, false);
            account.transactions[0].transactionDate = DateTime.Now.AddDays(-11);
            Assert.AreEqual(150, account.InterestEarned());
        }


        [TestMethod]
        public void CompoundSavingInterest()
        {
            Account account = new SavingAccount();
            account.Deposit(3000, false);
            account.transactions[0].transactionDate = DateTime.Now.AddDays(-100);
            Assert.AreEqual(16.483, account.InterestEarned(), DOUBLE_DELTA);
        }
    }
}
