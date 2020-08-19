using abc_bank;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.Accounts
{
    [TestClass]
    public class SavingsAccountTest
    {
        [TestMethod]
        public void InterestEarned_IsPointOnePercentForAmountsOfOneThousandOrLess()
        {
            Account savings = new SavingsAccount();
            savings.Deposit(1000.0);

            double result = savings.InterestEarned;

            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void InterestEarned_IsPointTwoPercentForAnyAmountOverTheFirstThousand()
        {
            Account savings = new SavingsAccount();
            savings.Deposit(2000.0);

            double result = savings.InterestEarned;

            Assert.AreEqual(3.0, result);
        }
    }
}
