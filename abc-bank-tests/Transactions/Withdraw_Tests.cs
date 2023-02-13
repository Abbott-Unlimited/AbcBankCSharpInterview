using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Exceptions;
using abc_bank.Transactions;

namespace abc_bank_tests.Transactions {
  [TestClass]
  public class Withdraw_Tests {

    #region Properties

    #region Id 

    [Ignore]
    [TestMethod]
    public void Id_Property__NOT_IMPLEMENTED() => throw new NotImplementedException();

    #endregion

    #region AccountId

    [TestMethod]
    public void AccountId_Property_is_set_to_withdrawAccount_Id_constructor_arg() {
      var acct = new SavingsAccount(0, 1000.00);
      var transaction = acct.Withdraw(1000);

      Assert.AreEqual(acct.Id, transaction.AccountId);
    }

    #endregion

    #region Date 

    [TestMethod]
    public void Date_Property_is_set_to_transactionDate_constructor_arg() {
      var acct = new SavingsAccount(0, 1000);
      var tDate = acct.Withdraw(100).Date;
      var dt = DateTime.Now;

      // not an exact check - dead-hit check will be off by some milliseconds.
      Assert.AreEqual($"{dt.ToShortDateString()} {dt.ToShortTimeString()}", $"{tDate.ToShortDateString()} {tDate.ToShortTimeString()}");
    }

    #endregion

    #region Amount   

    [TestMethod]
    public void Amount_Property_is_set_to_transactionDate_constructor_arg() {
      var acct = new SavingsAccount(0, 1000.00);
      var transaction = acct.Withdraw(500.50);

      Assert.AreEqual(500.50, transaction.Amount);
    }

    #endregion

    #endregion

    #region CTOR

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Withdraw_constructor_throws_error_if_amount_arg_is_negative() {
      new Withdraw(-0.01, DateTime.Now, new SavingsAccount(0));
      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Withdraw_constructor_throws_error_if_amount_arg_is_0() {
      new Withdraw(0, DateTime.Now, new SavingsAccount(0));
      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientFundsException))]
    public void Withdraw_constructor_throws_error_if_amount_exceeds_current_balance() {
      new Withdraw(0.01, DateTime.Now, new SavingsAccount(0));

      Assert.Fail();
    }

    #endregion  

  }
}
