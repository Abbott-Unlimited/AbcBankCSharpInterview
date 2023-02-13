using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Transactions;
using abc_bank.Extensions.Reports;

namespace abc_bank_tests.Extensions {
  [TestClass]
  public class Statement {

    #region GetStatementAmount

    [TestMethod]
    public void GetStatementAmount_extention_for_deposit_transaction_returns_Amount_property_value() {
      var deposit = new Deposit(1000.00, DateTime.Now, new CheckingAccount(1));
      Assert.AreEqual(1000.00, deposit.GetStatementAmount());
    }

    [TestMethod]
    public void GetStatementAmount_extention_for_withdraw_transaction_returns_Amount_property_value_as_negative_value() {
      var deposit = new Withdraw(1000.00, DateTime.Now, new CheckingAccount(1, 1000));
      Assert.AreEqual(-1000.00, deposit.GetStatementAmount());
    }

    [Ignore]
    [TestMethod]
    public void GetStatementAmount_extention_for_transfer_transaction() => throw new NotImplementedException();


    #endregion

    #region GetLineItem 

    [TestMethod]
    public void GetLineItem_for_deposit() {
      var deposit = new Deposit(100, DateTime.Now, new SavingsAccount(0));
      var expected = "  deposit $100.00";

      Assert.AreEqual(expected, deposit.GetLineItem());
    }

    [TestMethod]
    public void GetLineItem_for_withdrawal() {
      var deposit = new Withdraw(100, DateTime.Now, new SavingsAccount(0, 100));
      var expected = "  withdrawal $100.00";

      Assert.AreEqual(expected, deposit.GetLineItem());
    }

    #endregion
  }
}
