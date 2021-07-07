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
            Assert.AreEqual("2 Ducks", Language.FormatPlural(2, "Duck"));
        }

        [TestMethod]
        public void FormatPluralTestNegative()
        {
            Assert.AreEqual("1 Duck", Language.FormatPlural(1, "Duck"));
        }

        [TestMethod]
        public void ToDollarsTest()
        {
            Assert.AreEqual("$456.10", Language.ToDollars(456.10));
        }

    }
}
