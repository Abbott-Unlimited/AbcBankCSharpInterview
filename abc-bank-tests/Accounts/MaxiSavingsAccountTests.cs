using abc_bank.Accounts;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.Accounts {

  [TestClass]
  public class MaxiSavingsAccountTests {

    [TestMethod]
    public void Account_1000_Or_Less_Interest() {
      Assert.AreEqual(20M, new MaxiSavingsAccount(0, 1, 1000).InterestEarned);
    }

    [TestMethod]
    public void Account_Over_1000_Interest() {
      Assert.AreEqual(100M, new MaxiSavingsAccount(0, 1, 2000).InterestEarned);
    }
  }
}
