using System.Collections.Generic;

using abc_bank.Accounts;
using abc_bank.Exceptions;
using abc_bank.Utilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.Utilities {

  [TestClass]
  public class Validator_Tests {

    #region AccountIsInCollection

    [TestMethod]
    [ExpectedException(typeof(InvalidAccountException))]
    public void AccountIsInCollection_throws_error_if_account_collection_has_no_accounts() {
      var accounts = new List<IAccount>();
      var account = new CheckingAccount(1, 1);
      var account2 = new CheckingAccount(2, 1);

      accounts.Add(account2);
      Validators.AccountIsInCollection(account, accounts);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidAccountException");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAccountException))]
    public void AccountIsInCollection_throws_error_if_account_not_in_IAccounts_collection() {
      var account = new CheckingAccount(1, 1);
      var accounts = new List<IAccount>();

      Validators.AccountIsInCollection(account, accounts);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidAccountException");
    }

    [TestMethod]
    public void AccountIsInCollection_does_not_throw_error_when_account_is_found() {
      var accounts = new List<IAccount>();

      for (var i = 1; i < 25; i++) {
        accounts.Add(new CheckingAccount(i, 1));
      }
      var account = accounts[10];

      Validators.AccountIsInCollection(account, accounts);
    }

    #endregion

    #region FundsAvailableForWithdraw

    [TestMethod]
    [ExpectedException(typeof(InsufficientFundsException))]
    public void FundsAvailableForWithdraw_throws_error_if_amount_arg_is_negative() {
      Validators.FundsAvailableForWithdraw(0, 1000);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InsufficientFundsException");
    }

    #endregion

    #region TransactionAmount

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void TransactionAmount_throws_error_if_amount_arg_is_negative() {
      Validators.TransactionAmount(-0.01M);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidTransactionAmountException");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void TransactionAmount_throws_error_if_amount_arg_is_0() {
      Validators.TransactionAmount(0);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidTransactionAmountException");
    }

    #endregion

  }
}
