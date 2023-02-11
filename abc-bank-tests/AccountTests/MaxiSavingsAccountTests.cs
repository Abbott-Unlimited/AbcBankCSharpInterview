using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;

namespace abc_bank_tests.Accounts {

  [TestClass]
  public class MaxiSavingsAccountTests {

    [TestMethod]
    public void Account_1000_Or_Less_Interest() {
      Assert.AreEqual(20, new MaxiSavingsAccount(0, 1000.00).InterestEarned, Constants.DOUBLE_DELTA);
    }

    [TestMethod]
    public void Account_Over_1000_Interest() {
      Assert.AreEqual(100, new MaxiSavingsAccount(0, 2000.00).InterestEarned, Constants.DOUBLE_DELTA);
    }
  }
}
