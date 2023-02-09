using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;

namespace abc_bank_tests {

  [TestClass]
  public class AccountTests {
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

    #region Account Has Transactions

    [TestMethod]
    public void Test_Account_Has_No_Transactions() => Assert.IsFalse(acct.HasTransactions);

    [TestMethod]
    public void Test_Account_Has_Transactions() {
      acct.Deposit(100);

      Assert.IsTrue(acct.HasTransactions);
    }

    #endregion

    #region Deposits

    [TestMethod]
    public void Test_Account_Positive_Amount_Deposit_Works() {
      acct.Deposit(100);
      Assert.IsTrue(acct.CurrentBalance == 100);
      acct.Deposit(150);
      Assert.IsTrue(acct.CurrentBalance == 250);
    }

    [Ignore]
    [TestMethod]
    public void Test_Account_Zero_Dollar_Deposit_Does_Not_Work() {
      //Assert.OnNo(acct.Deposit(0.0));
    }

    [Ignore]
    [TestMethod]
    public void Test_Account_Negative_Amount_Deposit_Does_Not_Work() { }

    #endregion

    #region Withdraws

    [Ignore]
    [TestMethod]
    public void Test_Zero_Balance_Account_Withdraw_Does_Not_Work() { }

    [Ignore]
    [TestMethod]
    public void Test_Account_Positive_Amount_Withdraw_Works() { }

    [Ignore]
    [TestMethod]
    public void Test_Account_Zero_Dollar_Withdraw_Does_Not_Work() { }

    [Ignore]
    [TestMethod]
    public void Test_Account_Negative_Amount_Withdraw_Does_Not_Work() { }


    #endregion
  }
}
