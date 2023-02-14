using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Exceptions;
using abc_bank.Transactions;

namespace abc_bank_tests.Transactions {

  [TestClass]
  public class Transfer_Tests {

    #region Setup and Teardown

    IAccount originAccount;
    IAccount destinationAccount;
    DateTime transDate;

    [TestInitialize]
    public void Init() {
      originAccount = new SavingsAccount(0, 1, 100);
      destinationAccount = new CheckingAccount(0, 1, 1);
      transDate = DateTime.Now;
    }

    [TestCleanup]
    public void Cleanup() {
      originAccount = null;
      destinationAccount = null;
    }

    #endregion

    #region Properties

    [TestMethod]
    public void Amount_property_is_initialized_to_transferAmount_in_constructor() {
      var transfer = new Transfer(1, transDate, originAccount, destinationAccount);
      Assert.AreEqual(transDate, transfer.Date);
    }

    [TestMethod]
    public void Date_property_is_initialized_to_transactionDate_in_constructor() {
      var transfer = new Transfer(1, transDate, originAccount, destinationAccount);
      Assert.AreEqual(transDate, transfer.Date);
    }

    [TestMethod]
    public void OriginAccountId_property_is_initialized_to_originAccount_Id_in_constructor() {
      var transfer = new Transfer(1, transDate, originAccount, destinationAccount);
      Assert.AreEqual(transDate, transfer.Date);
    }

    [TestMethod]
    public void DestinationAccountId_property_is_initialized_to_destinationAccount_Id_in_constructor() {
      var transfer = new Transfer(1, transDate, originAccount, destinationAccount);
      Assert.AreEqual(transDate, transfer.Date);
    }

    #endregion

    #region CTOR args validation

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionRequestException))]
    public void Transfer_constructor_throws_error_if_origin_and_destination_accounts_do_not_belong_to_same_Customer() {
      var originAccount = new SavingsAccount(1, 1, 1000);
      var destinationAccount = new CheckingAccount(1, 2);
      new Transfer(500, DateTime.Now, originAccount, destinationAccount);

      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InsufficientFundsException))]
    public void Transfer_constructor_throws_error_if_transfer_amount_exceeds_origin_account_balance() {
      new Transfer(150, DateTime.Now, originAccount, destinationAccount);

      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Transfer_constructor_throws_error_if_transfer_amount_is_negative() {
      new Transfer(-0.01M, DateTime.Now, originAccount, destinationAccount);

      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidTransactionAmountException))]
    public void Transfer_constructor_throws_error_if_transfer_amount_is_0() {
      new Transfer(0, transDate, originAccount, destinationAccount);

      Assert.Fail();
    }

    [TestMethod]
    public void Transfer_constructor_instantiates_when_amount_is_positive_and_less_than_origin_balance() {
      var trans = new Transfer(1, transDate, originAccount, destinationAccount);

      Assert.IsInstanceOfType(trans, typeof(Transfer));
    }

    #endregion

  }
}
