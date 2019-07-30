using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void AddTransactionTest()
        {
            var transaction = new Transaction(500);
            Assert.IsInstanceOfType(transaction, typeof(Transaction));
        }
    }
}
