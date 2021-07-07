using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Helpers;


namespace abc_bank_tests
{
    [TestClass]
    public class LanguageTests
    {
        [TestMethod]
        public void FormatPluralTestPositive()
        {
            Assert.AreEqual(Language.FormatPlural(2, "Duck"), "2 Ducks");
        }

        [TestMethod]
        public void FormatPluralTestNegative()
        {
            Assert.AreEqual(Language.FormatPlural(1, "Duck"), "1 Duck");
        }

        [TestMethod]
        public void ToDollarsTest()
        {
            Assert.AreEqual(Language.ToDollars(456.10), "$456.10");
        }

    }
}
