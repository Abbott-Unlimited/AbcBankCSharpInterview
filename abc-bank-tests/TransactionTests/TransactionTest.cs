using AbcCompanyEstablishmentApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbcCompanyEstablishmentAppTests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
    }
}
