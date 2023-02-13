using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Extensions.Formatting;


namespace abc_bank_tests.Extensions {

  [TestClass]
  public class Formatting {
    [TestMethod]
    public void ToDollars_formatting_extension_sanity_test() {
      var expected = "$1,000.00";

      Assert.AreEqual(expected, (1000.0).ToDollars());
    }
  }
}
