using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class CalculateInterestEarnedShould
    {
        [TestMethod]
        public void ReturnPointOnePercentOfBalance_IfChecking()
        {
            var balance = 1000;
            var sut = new Account(AccountType.CHECKING);
            sut.AddTransaction(balance);

            var result = sut.CalculateInterestEarned();

            Assert.AreEqual(balance * .001, result);
        }

        [TestMethod]
        public void ReturnPointOnePercentOfBalance_IfSavings_AndBalanceLessThan1000()
        {
            var balance = 1000;
            var sut = new Account(AccountType.SAVINGS);
            sut.AddTransaction(balance);

            var result = sut.CalculateInterestEarned();

            Assert.AreEqual(balance * .001, result);
        }

        [TestMethod]
        public void ReturnTieredPercentOfBalance_IfSavings_AndBalanceMoreThan1000()
        {
            var balance = 10000;
            var expected = ((balance - 1000) * .002)+ (1000 * .001);
            var sut = new Account(AccountType.SAVINGS);
            sut.AddTransaction(balance);

            var result = sut.CalculateInterestEarned();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Return2PercentOfBalance_IfMaxiSavings_AndBalanceLessThan1000()
        {
            var balance = 1000;
            var expected = balance * .02;
            var sut = new Account(AccountType.MAXI_SAVINGS);
            sut.AddTransaction(balance);

            var result = sut.CalculateInterestEarned();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ReturnTieredPercentOfBalance_IfMaxiSavings_AndBalanceMoreThan1000()
        {
            var balance = 10000;
            var expected = ((balance - 2000) * .1) + (1000 * .05) + (1000 * .02);
            var sut = new Account(AccountType.MAXI_SAVINGS);
            sut.AddTransaction(balance);

            var result = sut.CalculateInterestEarned();

            Assert.AreEqual(expected, result);
        }
    }
}
