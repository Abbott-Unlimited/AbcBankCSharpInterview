using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Transactions;
using abc_bank.Extensions.Reports;
using System.Text;

namespace abc_bank_tests.Extensions {
  [TestClass]
  public class Statement {

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
      var deposit = new Deposit(100, DateTime.Now, new SavingsAccount(0, 1, 1));
      var sb = new StringBuilder();
      var expected = sb.AppendLine("  deposit $100.00").ToString();

      sb.Clear();
      deposit.GetLineItem(ref sb);
      var actual = sb.ToString();

      Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void GetLineItem_for_withdrawal() {
      var withdrawal = new Withdraw(100, DateTime.Now, new SavingsAccount(0, 1, 100));
      var sb = new StringBuilder();
      var expected = sb.AppendLine("  withdrawal $100.00").ToString();

      sb.Clear();
      withdrawal.GetLineItem(ref sb);
      var actual = sb.ToString();

      Assert.AreEqual(expected, actual);
    }

    #endregion
  }
}
