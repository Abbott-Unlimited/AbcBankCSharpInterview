using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

using abc_bank.Exceptions;
using abc_bank.Accounts;

/*
 *  We are only testing the AccountBase methods and properties that have actually been
 *  implemented within the base-class here to reduce repeated code (specifically, tests)
 *  
 *  The abstract methods and any overrides MUST BE tested within the concrete implementation
 */
namespace abc_bank_tests.Accounts {

  #region AccountBase Implementation Structure

  public class AccountBaseMock : AccountBase {
    public override double InterestEarned => throw new System.NotImplementedException();

    public AccountBaseMock(AccountType accountType) : base(accountType) { }
  }

  #endregion

  [TestClass]
  public class Account_Common_Behaviors_Tests : BaseTest {
    #region Setup and Teardown

    IAccount acct;

    [TestInitialize]
    public void Init() {
      acct = new AccountBaseMock(AccountType.CHECKING);
    }

    [TestCleanup]
    public void Cleanup() {
      acct = null;
    }

    #endregion

    #region CurrentBalance & AccountType Properties

    [TestMethod]
    public void Test_Current_Balance_Property_Returns_Accurate_Current_Balance() {
      acct.Deposit(100);
      acct.Deposit(200.75);
      acct.Withdraw(52.75);

      Assert.AreEqual(248.00, acct.CurrentBalance);
    }

    [TestMethod]
    // TODO:  Bonus points for eventually moving to a generic test for 'all' account types
    // using reflection to find and consume all AccountTypes (Pretty simple at the moment, using the enum)
    public void Test_Account_Type_Property_Returns_Correct_Type() {
      acct = new AccountBaseMock(AccountType.CHECKING);
      Assert.AreEqual(AccountType.CHECKING, acct.AccountType);

      acct = new AccountBaseMock(AccountType.SAVINGS);
      Assert.AreEqual(AccountType.SAVINGS, acct.AccountType);

      acct = new AccountBaseMock(AccountType.MAXI_SAVINGS);
      Assert.AreEqual(AccountType.MAXI_SAVINGS, acct.AccountType);
    }

    #endregion

    #region Deposit

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

    #region Withdraw

    [TestMethod]
    public void Test_Account_Withdraw_Amount_Cannot_Exceed_Current_Balance() {
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
    public void Test_Account_Has_No_Transactions() {
      Assert.IsFalse(acct.HasTransactions);
    }

    [TestMethod]
    public void Test_Account_Has_Transactions() {
      acct.Deposit(100);

      Assert.IsTrue(acct.HasTransactions);
    }

    #endregion
  }
}
