using abc_bank;
using abc_bank.Accounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.Accounts
{
    [TestClass]
    public class CheckingAccountTest
    {
        [TestMethod]
        public void InterestEarned_IsPointOnePercent()
        {
            Account checking = new CheckingAccount();
            checking.Deposit(3000.0);

            double result = checking.InterestEarned;

            Assert.AreEqual(3.0, result);
        }
    }
}
