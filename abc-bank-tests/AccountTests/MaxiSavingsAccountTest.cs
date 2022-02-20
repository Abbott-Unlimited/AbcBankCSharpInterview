using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class MaxiSavingsAccountTest
    {
        private static readonly double DOUBLE_DELTA = 1e-2;

        [TestMethod]
        public void InterestEarned_OneYearWithNoWithdrawals_ReturnsBalancePlusMAXRATEDailyAccrual()
        {
            // arrange
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);

            // act
            acct.Deposit(10000.00);
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31));

            // assert
            Assert.AreEqual(500.0, acct.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarned_TenDaysWithWithdrawalOnFirstDay_ReturnsBalancePlusMINRATEAccrual()
        {
            // arrange
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);

            // act
            acct.Deposit(20000.0);
            acct.Withdraw(10000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 10)); // earns .27 at MIN_RATE on 10000.00 over 10 days

            // assert
            Assert.AreEqual(0.27, acct.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarned_180Days_MAXRATEDailyAccrual()
        {
            // arrange
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);

            // act
            acct.Deposit(10000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 6, 30)); // accrue max rate over 181 days

            // assert
            Assert.AreEqual(247.95, acct.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarned_10Days_MINRATEDailyAccrual()
        {
            // arrange
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 7, 1));
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);

            // act
            acct.Deposit(10000.0);
            acct.Withdraw(5000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 7, 10)); // accrue min rate over 10 days

            // assert
            Assert.AreEqual(0.14, acct.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarned_174Days_MAXRATEDailyAccrual()
        {
            // arrange
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 7, 11));
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);

            // act
            acct.Deposit(5000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31)); // accrue max rate over 174 days

            // assert
            Assert.AreEqual(119.18, acct.InterestEarned(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void InterestEarned_OneYear_SpecialAccrual()
        {
            // arrange
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);

            // act
            acct.Deposit(10000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 7, 1)); // should earn ~247.95 at MAX_RATE on 10000.0 over 181 days
            acct.Withdraw(5000.0); // should earn ~.14 at MIN_RATE on 5000.00 over 10 days

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31)); // should earn ~119.18 at MAX_RATE on 5000.0 over 174 days

            // assert
            Assert.AreEqual(367.27, acct.InterestEarned(), DOUBLE_DELTA);
        }
    }
}
