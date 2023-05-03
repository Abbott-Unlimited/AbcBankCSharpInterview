using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void InterestEarned_WithZeroBalance_ReturnsZero()
        {
            var savings = new Account(Account.SAVINGS);
            var interest = savings.InterestEarned();
            Assert.AreEqual(0, interest);
        }

        [TestMethod]
        public void InterestEarned_ReturnsCorrectAmount()
        {
            var savings = new Account(Account.SAVINGS);
            savings.Deposit(1000);

            // set lastInterestDate to 1 day ago
            savings.GetType().GetProperty("lastInterestDate", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(savings, DateTime.Now.AddDays(-1));

            var expectedInterest = (1000 * 0.001 / 365) * 1; // daily interest rate * days since last interest

            var actualInterest = savings.InterestEarned();

            Assert.AreEqual(expectedInterest, actualInterest, 0.001);
        }

        [TestMethod]
        public void TestMaxiSavingsInterestEarned_5PercentInterest_NoWithdrawalIn10Days()
        {
            var maxiSavings = new Account(Account.MAXI_SAVINGS);
            maxiSavings.Deposit(10000);

            maxiSavings.GetType().GetProperty("lastInterestDate", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(maxiSavings, DateTime.Now.AddDays(-20));

            var expectedInterest = (10000 * 0.05 / 365) * 20; 

            var actualInterest = maxiSavings.InterestEarned();

            Assert.AreEqual(expectedInterest, actualInterest, 0.001);
        }

        [TestMethod]
        public void TestMaxiSavingsInterestEarned_5PercentInterest_WITHWithdrawalIn10Days()
        {
            var maxiSavings = new Account(Account.MAXI_SAVINGS);
            maxiSavings.Deposit(10000);

            maxiSavings.GetType().GetProperty("lastInterestDate", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(maxiSavings, DateTime.Now.AddDays(-20));

            maxiSavings.Withdraw(1000);

            var expectedInterest = (9000 * 0.001 / 365) * 20;
            var actualInterest = maxiSavings.InterestEarned();

            Assert.AreEqual(expectedInterest, actualInterest, 0.001);
        }
    }
}
