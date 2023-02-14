using System;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;
using abc_bank.Customers;
using abc_bank.Reports;
using abc_bank.Transactions;


namespace abc_bank_tests.Reports {
  [TestClass]
  public class Statements_Tests {

    [TestMethod]
    public void AccountStatement_checking_account_with_1_deposit_Test() {
      var checkingAccount = new CheckingAccount(0, 1);
      checkingAccount.Deposit(100.0);

      var sb = new StringBuilder();

      sb.AppendLine("Checking Account");
      sb.AppendLine("    deposit           $100.00");
      sb.AppendLine("Total $100.00");

      Assert.AreEqual(sb.ToString(), Statements.AccountStatement(checkingAccount));
    }

    [TestMethod]
    public void AccountStatement_checking_account_with_1_deposit_and_1_withdrawal_Test() {
      var checkingAccount = new CheckingAccount(0, 1);

      checkingAccount.Deposit(100.0);
      checkingAccount.Withdraw(75.00);

      var sb = new StringBuilder();

      sb.AppendLine("Checking Account");
      sb.AppendLine("    deposit           $100.00");
      sb.AppendLine("    withdrawal         $75.00");
      sb.AppendLine("Total $25.00");

      Assert.AreEqual(sb.ToString(), Statements.AccountStatement(checkingAccount));
    }


    [TestMethod]
    public void CustomerStatement_Test() {
      var checkingAccount = new CheckingAccount(0, 1);
      var savingsAccount = new SavingsAccount(0, 1);

      var henry = new Customer("Henry", 0);

      henry.OpenAccount(checkingAccount);
      henry.OpenAccount(savingsAccount);

      checkingAccount.Deposit(100.0);
      savingsAccount.Deposit(4000.0);
      savingsAccount.Withdraw(200.0);

      var sb = new StringBuilder();

      sb.AppendLine("Statement for Henry");
      sb.AppendLine();
      sb.AppendLine("Checking Account");
      sb.AppendLine("    deposit           $100.00");
      sb.AppendLine("Total $100.00");
      sb.AppendLine();
      sb.AppendLine("Savings Account");
      sb.AppendLine("    deposit         $4,000.00");
      sb.AppendLine("    withdrawal        $200.00");
      sb.AppendLine("Total $3,800.00");
      sb.AppendLine();
      sb.AppendLine("Total In All Accounts $3,900.00");


      Assert.AreEqual(sb.ToString(), henry.Statement);
    }

  }
}
