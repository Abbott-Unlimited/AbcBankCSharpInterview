using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;
using System.Collections.Generic;
using System.Text;
using System;

namespace abc_bank_tests.Customers {

  [TestClass]
  public class CustomerTest {

    #region Setup and Teardown

    Customer cust;

    [TestInitialize]
    public void Init() {
      cust = new Customer("Alvin", 0);
    }

    [TestCleanup]
    public void Cleanup() {
      cust = null;
    }

    #endregion

    #region Properties 

    #region Id

    [TestMethod]
    public void Id_is_initialized_to_value_of_customerId_plus_one_arg_in_constructor() {
      Assert.AreEqual(1, new Customer("Flipper", 0).Id);
    }

    #endregion

    #region Name

    [TestMethod]
    public void Name_is_initialized_to_name_argument_value_in_constructor() {
      Assert.AreEqual("Jeff", new Customer("Jeff", 0).Name);
    }

    #endregion     

    #region Accounts

    [TestMethod]
    public void Accounts_is_initialized_when_Customer_is_instantiated() {
      Assert.IsInstanceOfType(new Customer("Jimmy", 0).Accounts, typeof(List<IAccount>));
    }

    #endregion

    #region NumberOfAccounts

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_0_account() {
      Assert.AreEqual(0, new Customer("Oscar", 0).NumberOfAccounts);
    }

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_1_account() {
      var oscar = new Customer("Oscar", 0)
        .OpenAccount(new CheckingAccount(0));

      Assert.AreEqual(1, oscar.NumberOfAccounts);
    }

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_3_accounts() {
      var oscar = new Customer("Oscar", 0)
        .OpenAccount(new CheckingAccount(0))
        .OpenAccount(new SavingsAccount(0))
        .OpenAccount(new MaxiSavingsAccount(0));

      Assert.AreEqual(3, oscar.NumberOfAccounts);
    }

    #endregion

    #region Statement

    [TestMethod]
    public void Customer_Statement_Report_Test() {
      var checkingAccount = new CheckingAccount(0);
      var savingsAccount = new SavingsAccount(0);

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
      sb.AppendLine("  deposit $100.00");
      sb.AppendLine("Total $100.00");
      sb.AppendLine();
      sb.AppendLine("Savings Account");
      sb.AppendLine("  deposit $4,000.00");
      sb.AppendLine("  withdrawal $200.00");
      sb.AppendLine("Total $3,800.00");
      sb.AppendLine();
      sb.AppendLine("Total In All Accounts $3,900.00");


      Assert.AreEqual(sb.ToString(), henry.Statement);
    }

    #endregion 

    #region Total Interest Earned

    [Ignore]
    [TestMethod]
    public void TotalInterestEarned_Stub() { }

    #endregion

    #endregion

    #region Public Methods

    #region OpenAccount

    [TestMethod]
    public void OpenAccount_by_IAccount_Instance() {
      var acct = new CheckingAccount(cust.NumberOfAccounts);
      Assert.AreEqual(1, cust.OpenAccount(acct).NumberOfAccounts);
    }

    [TestMethod]
    public void OpenAccount_by_AccountType_lastAccountId_and_maybe_initialDeposit() {
      cust.OpenAccount(new CheckingAccount(cust.NumberOfAccounts));
      Assert.AreEqual(1, cust.NumberOfAccounts);
    }

    #endregion

    #region TransferFunds

    [Ignore]
    [TestMethod]
    public void TransferFunds_fromAccountId_NOT_IMPLEMENTED() => throw new NotImplementedException();

    #endregion

    #endregion

    #region Protected Methods (Stubs)

    #region StatementForAccount

    [Ignore]
    [TestMethod]
    public void StatementForAccount_Stub() { }

    #endregion

    #endregion

  }
}
