using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using abc_bank;
using System.IO;
using System;
using System.Threading.Tasks;

namespace abc_bank_tests {
  [TestClass]
  public class SavingsAccountTests : BaseTest {
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

    [TestMethod]
    public void Test_Savings_Account_Under_1000_Interest() {
      acct.Deposit(100);
      Assert.AreEqual(100, acct.CurrentBalance);
    }

    [Ignore]
    [TestMethod]
    public void Test_Savings_Account_Over_1000_Interest() {

    }

    #endregion

  }
}
