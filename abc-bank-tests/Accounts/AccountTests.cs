using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Exceptions;

/*
 *  We are only testing the AccountBase methods and properties that have actually been
 *  implemented within the base-class here to reduce repeated code (specifically, tests)
 *  
 *  The abstract methods and any overrides MUST BE tested within the concrete implementation
 */
namespace abc_bank_tests.Accounts {

  #region AccountBase Implementation Fixture

  public class AccountBaseMock : AccountBase {

    public override double InterestEarned => throw new System.NotImplementedException();
    public override string ReportLabel => throw new System.NotImplementedException();

    public AccountBaseMock(AccountType accountType, int accountId, int customerId, double initialDeposit = 0.00)
      : base(accountType, accountId, customerId, initialDeposit) { }
  }

  #endregion

  [TestClass]
  public class Account_Common_Behaviors_Tests {

    #region Setup and Teardown

    IAccount acct;

    [TestInitialize]
    public void Init() {
      acct = new AccountBaseMock(AccountType.CHECKING, 1, 0);
    }

    [TestCleanup]
    public void Cleanup() {
      acct = null;
    }

    #endregion

    #region Constructor

    [TestMethod]
    public void CTOR_No_deposit_attempt_if_initialDeposit_amount_is_0_or_less() {
      Assert.IsFalse(new AccountBaseMock(AccountType.CHECKING, 1, 0).HasTransactions);
    }

    [TestMethod]
    public void CTOR_Deposit_made_if_initialDeposit_amount_greater_than_0() {
      Assert.IsTrue(new AccountBaseMock(AccountType.CHECKING, 0, 1, 0.01).HasTransactions);
    }

    #endregion

    #region CurrentBalance & AccountType Properties

    [TestMethod]
    public void Current_Balance_Property_Returns_Accurate_Current_Balance() {
      acct.Deposit(100);
      acct.Deposit(200.75);
      acct.Withdraw(52.75);

      Assert.AreEqual(248.00, acct.CurrentBalance);
    }

    [TestMethod]
    public void Account_Type_Property_Returns_Correct_Type() {
      acct = new AccountBaseMock(AccountType.CHECKING, 1, 0);
      Assert.AreEqual(AccountType.CHECKING, acct.AccountType);

      acct = new AccountBaseMock(AccountType.SAVINGS, 1, 0);
      Assert.AreEqual(AccountType.SAVINGS, acct.AccountType);

      acct = new AccountBaseMock(AccountType.MAXI_SAVINGS, 1, 0);
      Assert.AreEqual(AccountType.MAXI_SAVINGS, acct.AccountType);
    }

    #endregion

    #region Deposit

    [TestMethod]
    public void Account_Positive_Amount_Deposit_Works() {
      acct.Deposit(100);
      Assert.IsTrue(acct.CurrentBalance == 100);

      acct.Deposit(150);
      Assert.IsTrue(acct.CurrentBalance == 250);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Account_Zero_Dollar_Deposit_Does_Not_Work() {
      acct.Deposit(0.0);
      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Account_Negative_Amount_Deposit_Does_Not_Work() {
      acct.Deposit(-1);
      Assert.Fail();
    }

    #endregion

    #region Withdraw

    [TestMethod]
    [ExpectedException(typeof(InsufficientFundsException))]
    public void Account_Withdraw_Amount_Cannot_Exceed_Current_Balance() {
      acct.Withdraw(100);
      Assert.Fail();
    }

    [TestMethod]
    public void Account_Positive_Amount_Withdraw_Works() {
      acct.Deposit(100);
      acct.Withdraw(50);

      Assert.IsTrue(acct.CurrentBalance == 50);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Account_Zero_Dollar_Withdraw_Does_Not_Work() {
      acct.Withdraw(0.0);
      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Account_Negative_Amount_Withdraw_Does_Not_Work() {
      acct.Withdraw(-1.0);
      Assert.Fail();
    }

    #endregion   

    #region Transactions

    [TestMethod]
    public void Account_Has_No_Transactions() {
      Assert.IsFalse(acct.HasTransactions);
    }

    [TestMethod]
    public void Account_Has_Transactions() {
      acct.Deposit(100);

      Assert.IsTrue(acct.HasTransactions);
    }

    #endregion
  }
}
