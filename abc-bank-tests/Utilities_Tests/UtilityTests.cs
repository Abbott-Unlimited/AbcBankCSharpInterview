using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;
using abc_bank.Exceptions;

namespace abc_bank_tests.Utilities_Tests {

  [TestClass]
  public class Utilities_Tests {

    #region ValidateAccountInCollection

    [TestMethod]
    [ExpectedException(typeof(InvalidAccountException))]
    public void ValidateAccountInCollection_throws_error_if_account_collection_has_no_accounts() {
      var accounts = new List<IAccount>();
      var account = new CheckingAccount(1, 1);
      var account2 = new CheckingAccount(2, 1);
      
      accounts.Add(account2);
      abc_bank.Utilities.ValidateAccountInCollection(account, accounts);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidAccountException");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAccountException))]
    public void ValidateAccountInCollection_throws_error_if_account_not_in_IAccounts_collection() {
      var account = new CheckingAccount(1, 1);
      var accounts = new List<IAccount>();

      Utilities.ValidateAccountInCollection(account, accounts);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidAccountException");
    }

    [TestMethod]
    public void ValidateAccountInCollection_does_not_throw_error_when_account_is_found() {
      var accounts = new List<IAccount>();

      for (var i = 1; i < 25; i++) {
        accounts.Add(new CheckingAccount(i, 1));
      }
      var account = accounts[10];

      Utilities.ValidateAccountInCollection(account, accounts);
    }

    #endregion

    #region ValidateFundsAvailable

    [TestMethod]
    [ExpectedException(typeof(InsufficientFundsException))]
    public void ValidateFundsAvailable_throws_error_if_amount_arg_is_negative() {
      Utilities.ValidateFundsAvailable(0, 1000);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InsufficientFundsException");
    }

    #endregion

    #region ValidateTransactionAmount

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void ValidateTransactionAmount_throws_error_if_amount_arg_is_negative() {
      Utilities.ValidateTransactionAmount(-0.01);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidTransactionAmountException");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void ValidateTransactionAmount_throws_error_if_amount_arg_is_0() {
      Utilities.ValidateTransactionAmount(0.0);

      Assert.Fail("Expected exception not thrown: abc_bank.Exceptions.InvalidTransactionAmountException");
    }

    #endregion

  }
}
