using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        private static double DEPOSIT_3000 = 3000.0;
        private static double DEPOSIT_1500 = 1500.0;
        private static double DEPOSIT_500 = 500.0;
        private static double TOTALINTERESTPAID_170 = 170.0;
        private static double INTERESTEARNED_3DOLLARS = 3.0;
        private static double INTERESTEARNED_45DOLLARS = 45.0;
        private static double INTERESTEARNED_10DOLLARS = 10.0;
        private static double INTERESTEARNED_2DOLLARS = 2.0;
        private static double INTERESTEARNED_50CENTS = .5;

        [TestMethod]
        public void testAccount()
        {
            Account account = new Account(Account.CHECKING);

            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void testInterestEarned_Checking()
        {
            Account account = new Account(Account.CHECKING);
            account.Deposit(DEPOSIT_3000);

            Assert.AreEqual(INTERESTEARNED_3DOLLARS, account.InterestEarned());
        }

        [TestMethod]
        public void testInterestEarned_Savings_LessThan1000()
        {
            Account account = new Account(Account.SAVINGS);
            account.Deposit(DEPOSIT_500);

            Assert.AreEqual(INTERESTEARNED_50CENTS, account.InterestEarned());
        }

        [TestMethod]
        public void testInterestEarned_Savings_GreaterThan1000()
        {
            Account account = new Account(Account.SAVINGS);
            account.Deposit(DEPOSIT_1500);

            Assert.AreEqual(INTERESTEARNED_2DOLLARS, account.InterestEarned());
        }

        [TestMethod]
        public void testInterestEarned_MaxiSavings_LessThan1000()
        {
            Account account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(DEPOSIT_500);

            Assert.AreEqual(INTERESTEARNED_10DOLLARS, account.InterestEarned());
        }

        [TestMethod]
        public void testInterestEarned_MaxiSavings_Between1000And2000()
        {
            Account account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(DEPOSIT_1500);

            Assert.AreEqual(INTERESTEARNED_45DOLLARS, account.InterestEarned());
        }

        [TestMethod]
        public void testInterestEarned_MaxiSavings_GreaterThan2000()
        {
            Account account = new Account(Account.MAXI_SAVINGS);
            account.Deposit(DEPOSIT_3000);

            Assert.AreEqual(TOTALINTERESTPAID_170, account.InterestEarned());
        }
    }
}
