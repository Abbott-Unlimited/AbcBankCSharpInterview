using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Transactions;
using abc_bank.Reports;

namespace abc_bank_tests.Extensions {
  [TestClass]
  public class Statement {

    #region Formatting Helpers & debugging

    public static void LineItemDebugVisualizer(string expected, string actual) {
      System.Diagnostics.Debug.WriteLine("-----------------------------");
      System.Diagnostics.Debug.WriteLine($"Expected:   | {expected} |");
      System.Diagnostics.Debug.WriteLine($"Actual:     | {actual} |");
    }

    #endregion

    #region GetStatementAmount

    [TestMethod]
    public void GetStatementAmount_extention_for_deposit_transaction_returns_Amount_property_value() {
      var deposit = new Deposit(1000.00, DateTime.Now, new CheckingAccount(1, 1, 0));
      Assert.AreEqual(1000.00, deposit.GetStatementAmount());
    }

    [TestMethod]
    public void GetStatementAmount_extention_for_withdraw_transaction_returns_Amount_property_value_as_negative_value() {
      var deposit = new Withdraw(1000.00, DateTime.Now, new CheckingAccount(1, 1, 1000));
      Assert.AreEqual(-1000.00, deposit.GetStatementAmount());
    }

    #endregion

    #region GetLineItem 

    [TestMethod]
    public void GetLineItem_for_deposit() {
      var deposit = new Deposit(100, DateTime.Now, new SavingsAccount(1, 1, 1));
      var expected = "deposit           $100.00";
      var actual = deposit.GetLineItem();

      LineItemDebugVisualizer(expected, actual);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetLineItem_for_withdrawal() {
      var withdrawal = new Withdraw(100, DateTime.Now, new SavingsAccount(0, 1, 100));
      var expected = "withdrawal        $100.00";
      var actual = withdrawal.GetLineItem();

      LineItemDebugVisualizer(expected, actual);
      Assert.AreEqual(expected, actual);
    }

    #endregion

    #region GetLineItem Funds Transfer

    [TestMethod]
    public void GetLineItem_for_transfer_deposit() {
      var originAccount = new SavingsAccount(1, 1, 10000);
      var destinationAccount = new CheckingAccount(2, 1);

      new Transfer(1428.23, DateTime.Now, originAccount, destinationAccount);

      var expected = "deposit         $1,428.23  transfer from acct #00000002";
      var actual = destinationAccount.Transactions[0].GetLineItem();

      LineItemDebugVisualizer(expected, actual);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetLineItem_for_transfer_withdrawal() {
      var originAccount = new SavingsAccount(1, 1, 10000);
      var destionationAccount = new CheckingAccount(2, 1);

      new Transfer(1428.23, DateTime.Now, originAccount, destionationAccount);

      var expected = "withdrawal      $1,428.23    transfer to acct #00000002";
      var actual = originAccount.Transactions[1].GetLineItem();

      LineItemDebugVisualizer(expected, actual);
      Assert.AreEqual(expected, actual);
    }

    #endregion

    #region Extension Methods

    [TestMethod]
    public void ToDollars_formatting_extension_sanity_test() {
      var expected = "$1,000.00";

      Assert.AreEqual(expected, (1000.0).ToDollars());
    }

    #endregion
  }
}
