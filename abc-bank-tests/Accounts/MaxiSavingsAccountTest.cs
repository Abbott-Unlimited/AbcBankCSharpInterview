using abc_bank;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.Accounts
{
    [TestClass]
    public class MaxiSavingsAccountTest
    {
        [TestMethod]
        public void InterestEarned_IsTwoPercentForAmountsOfOneThousandOrLess()
        {
            Account maxi = new MaxiSavingsAccount();
            maxi.Deposit(100.0);

            double result = maxi.InterestEarned;

            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void InterestEarned_IsFivePercentForTheSecondThousand()
        {
            Account maxi = new MaxiSavingsAccount();
            maxi.Deposit(1200.0);

            double result = maxi.InterestEarned;

            // 2% of the first $1000 = $20, plus
            // 5% of the additional $200 = $10
            Assert.AreEqual(30.0, result);
        }

        [TestMethod]
        public void InterestEarned_IsTenPercentForTheAmountAfterTheFirstTwoThousand()
        {
            Account maxi = new MaxiSavingsAccount();
            maxi.Deposit(2300.0);

            double result = maxi.InterestEarned;

            // 2% of the first $1000 = $20, plus
            // 5% of the second $1000 = $50, plus
            // 10% of the additional $300 = $30
            Assert.AreEqual(100.0, result);
        }
    }
}
