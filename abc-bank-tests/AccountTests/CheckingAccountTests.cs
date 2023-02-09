using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using abc_bank;

namespace abc_bank_tests {
  [TestClass]
  public class CheckingAccountTests : BaseTest {
    #region Setup and Teardown

    Account acct;

    [TestInitialize]
    public void Init() {
      acct = new Account(AccountType.CHECKING);
    }

    [TestCleanup]
    public void Cleanup() {
      acct = null;
    }

    #endregion


    #region Interest Earned

    [Ignore]
    [TestMethod]
    public void Test_Checking_Account_Interest() {
      Assert.Fail();
    }

    #endregion
  }
}
