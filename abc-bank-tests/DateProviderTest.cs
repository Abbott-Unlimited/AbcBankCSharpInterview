using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class DateProviderTest
    {
        [TestInitialize] public void Init() {
            DateProvider.Reset();
        }
        [TestMethod]
        public void TestDateProvider()
        {
            Assert.AreEqual(DateTime.Now.Date, DateProvider.Now.Date);
            DateProvider.AdjustDateByDays(5);
            Assert.AreEqual(DateTime.Now.Date.AddDays(5), DateProvider.Now  .Date);
            DateProvider.AdjustDateByDays(-5);
            Assert.AreEqual(DateTime.Now.Date, DateProvider.Now.Date);
        }
    }
}
