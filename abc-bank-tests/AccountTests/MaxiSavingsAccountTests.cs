using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using abc_bank;

namespace abc_bank_tests {
  [TestClass]
  public class MaxiSavingsAccountTests : BaseTest {
    #region Setup and Teardown

    Account acct;

    [TestInitialize]
    public void Init() {
      acct = new Account(AccountType.SAVINGS);
    }

    [TestCleanup]
    public void Cleanup() {
      acct = null;
    }

    #endregion


    #region Interest Earned

    [Ignore]
    [TestMethod]
    public void Test_Maxi_Savings_Account_Under_1000_Interest() {
      Assert.Fail();
    }

    [Ignore]
    [TestMethod]
    public void Test_Maxi_Savings_Account_Over_1000_Interest() {
      Assert.Fail();
    }

    #endregion
  }
}
