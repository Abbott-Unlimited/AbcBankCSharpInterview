using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using abc_bank;

namespace abc_bank_tests {

  [TestClass]
  public class AccountTests : BaseTest {
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

    #region Deposits

    [TestMethod]
    public void Test_Account_Positive_Amount_Deposit_Works() {
      acct.Deposit(100);
      Assert.IsTrue(acct.CurrentBalance == 100);
      acct.Deposit(150);
      Assert.IsTrue(acct.CurrentBalance == 250);
    }

    [TestMethod]
    public void Test_Account_Zero_Dollar_Deposit_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Deposit(0.0));
    }

    [TestMethod]
    public void Test_Account_Negative_Amount_Deposit_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Deposit(-1));
    }

    #endregion

    #region Withdraws

    [TestMethod]
    // not really liking this test name...
    public void Test_Account_Withdraw_Amount_No_Overdraft() {
      Assert.Throws<InsufficientFundsException>(() => acct.Withdraw(100));
    }

    [TestMethod]
    public void Test_Account_Positive_Amount_Withdraw_Works() {
      acct.Deposit(100);
      acct.Withdraw(50);
      Assert.IsTrue(acct.CurrentBalance == 50);
    }

    [TestMethod]
    public void Test_Account_Zero_Dollar_Withdraw_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Withdraw(0.0));
    }

    [TestMethod]
    public void Test_Account_Negative_Amount_Withdraw_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Withdraw(-1.0));
    }


    #endregion   

    #region Transactions

    [TestMethod]
    public void Test_Account_Has_No_Transactions() => Assert.IsFalse(acct.HasTransactions);

    [TestMethod]
    public void Test_Account_Has_Transactions() {
      acct.Deposit(100);

      Assert.IsTrue(acct.HasTransactions);
    }

    #endregion

    #region Properties

    [TestMethod]
    public void Test_Current_Balance_Property_Returns_Accurate_Current_Balance() {
      acct.Deposit(100);
      acct.Deposit(200.75);
      acct.Withdraw(52.75);
      Assert.AreEqual(248.00, acct.CurrentBalance);
    }

    [TestMethod]
    public void Test_Account_Type_Property_Returns_Correct_Type() {
      // tests initialize acct.AccountType as checking.
      Assert.AreEqual(AccountType.CHECKING, acct.AccountType);

      acct = new Account(AccountType.SAVINGS);
      Assert.AreEqual(AccountType.SAVINGS, acct.AccountType);

      acct = new Account(AccountType.MAXI_SAVINGS);
      Assert.AreEqual(AccountType.MAXI_SAVINGS, acct.AccountType);
    }

    #endregion
  }
}
