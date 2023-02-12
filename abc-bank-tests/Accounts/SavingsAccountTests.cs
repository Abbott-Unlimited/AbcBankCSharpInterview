using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;

namespace abc_bank_tests.Accounts {

  [TestClass]
  public class Savings_Account_Tests {

    [TestMethod]
    public void Interest_Earned_With_Balance_1000_Or_Less() {
      Assert.AreEqual(1, new SavingsAccount(0, 1000.00).InterestEarned, Constants.DOUBLE_DELTA);
    }

    [TestMethod]
    public void Interest_Earned_With_Balance_Greater_Than_1000() {
      Assert.AreEqual(4, new SavingsAccount(0, 2000).InterestEarned, Constants.DOUBLE_DELTA);
    }
  }
}
