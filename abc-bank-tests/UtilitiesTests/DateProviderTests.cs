using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using abc_bank.Utilities.Date;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.UtilitiesTests
{
    [TestClass]
    public class DateProviderTests
    {
        [TestMethod]
        public void Instance_AskForTwoInstances_TrueIfObjectsAreEqual()
        {
            object dateProvider1 = DateProvider.Instance;
            object dateProvider2 = DateProvider.Instance;

            Assert.AreEqual(dateProvider1, dateProvider2);
        }

        [TestMethod]
        public void Instance_GetInstance_TrueIfInstanceIsIDateProvider()
        {
            DateProvider dateProvider = DateProvider.Instance;

            Assert.IsTrue(dateProvider is IDateProvider);
        }

        [TestMethod]
        public void Now_CheckCurrentDateTimeNowMilliseconds_TrueIfWithinOneMillisecond()
        {
            double datetime1 = DateProvider.Instance.Now().Millisecond;
            double datetime2 = DateTime.Now.Millisecond;

            Assert.AreEqual(datetime1, datetime2, 1);
        }
    }
}
