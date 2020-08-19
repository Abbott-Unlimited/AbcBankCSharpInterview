using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class CalculateBalanceShould
    {
        [TestMethod]
        public void ReturnNetSumOfAllTransactions()
        {
            var transAmt = 1000;
            var sut = new Account(AccountType.CHECKING);

            sut.AddTransaction(transAmt);
            sut.AddTransaction(-transAmt);
            sut.AddTransaction(transAmt);

            Assert.AreEqual(1000, sut.CalculateBalance());
        }
    }
}
