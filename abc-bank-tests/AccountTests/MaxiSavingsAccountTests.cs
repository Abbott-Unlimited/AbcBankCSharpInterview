using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using abc_bank.Accounts;
using abc_bank;

namespace abc_bank_tests {
  [TestClass]
  public class MaxiSavingsAccountTests : BaseTest {
    #region Setup and Teardown

    MaxiSavingsAccount acct;

    [TestInitialize]
    public void Init() {
      acct = new MaxiSavingsAccount();
    }

    [TestCleanup]
    public void Cleanup() {
      acct = null;
    }

    #endregion

    [TestMethod]
    public void Account_Type_Property_Returns_Correct_Type() {
      Assert.AreEqual(abc_bank.Accounts.AccountType.MAXI_SAVINGS, acct.AccountType);
    }

    [TestMethod]
    public void Current_Balance_Returns_Accurate_Current_Balance() {
      acct.Deposit(100);
      acct.Deposit(200.75);
      acct.Withdraw(52.75);
      Assert.AreEqual(248.00, acct.CurrentBalance);
    }

    #region Deposits

    [TestMethod]
    public void Account_Positive_Amount_Deposit_Works() {
      acct.Deposit(100);
      Assert.IsTrue(acct.CurrentBalance == 100);
      acct.Deposit(150);
      Assert.IsTrue(acct.CurrentBalance == 250);
    }

    [TestMethod]
    public void Account_Zero_Amount_Deposit_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Deposit(0.0));
    }

    [TestMethod]
    public void Account_Negative_Amount_Deposit_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Deposit(-1));
    }

    #endregion

    #region Withdraws

    [TestMethod]
    public void Account_Withdraw_Amount_No_Overdraft() {
      Assert.Throws<InsufficientFundsException>(() => acct.Withdraw(100));
    }

    [TestMethod]
    public void Account_Positive_Amount_Withdraw_Works() {
      acct.Deposit(100);
      acct.Withdraw(50);
      Assert.IsTrue(acct.CurrentBalance == 50);
    }

    [TestMethod]
    public void Account_Zero_Dollar_Withdraw_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Withdraw(0.0));
    }

    [TestMethod]
    public void Account_Negative_Amount_Withdraw_Does_Not_Work() {
      Assert.Throws<InvalidTransactionAmountException>(() => acct.Withdraw(-1.0));
    }


    #endregion

    #region Transactions

    [TestMethod]
    public void Account_Has_No_Transactions() => Assert.IsFalse(acct.HasTransactions);

    [TestMethod]
    public void Account_Has_Transactions() {
      acct.Deposit(100);

      Assert.IsTrue(acct.HasTransactions);
    }

    #endregion

    #region Interest Earned

    [TestMethod]
    public void Account_1000_Or_Less_Interest() {
      acct.Deposit(1000);
      Assert.AreEqual(1, acct.InterestEarned);
    }

    [Ignore]
    [TestMethod]
    public void Account_Over_1000_Interest() {
      acct.Deposit(2000);
      Assert.AreEqual(1, acct.InterestEarned);
    }

    #endregion
  }
}
