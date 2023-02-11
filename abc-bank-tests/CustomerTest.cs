using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;
using System.Collections.Generic;

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
        .OpenAccount(AccountCreator.GetAccount(AccountType.CHECKING, 0));

      Assert.AreEqual(1, oscar.NumberOfAccounts);
    }

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_3_accounts() {
      var oscar = new Customer("Oscar", 0)
        .OpenAccount(AccountCreator.GetAccount(AccountType.CHECKING, 0))
        .OpenAccount(AccountCreator.GetAccount(AccountType.SAVINGS, 0))
        .OpenAccount(AccountCreator.GetAccount(AccountType.MAXI_SAVINGS, 0));

      Assert.AreEqual(3, oscar.NumberOfAccounts);
    }

    #endregion

    #region Statement

    [TestMethod]
    public void Customer_Statement_Report_Test() {
      // observation:   It's kinda weird we can open an account for a customer
      //                without having the customer belong to a bank...
      var checkingAccount = new CheckingAccount(0);
      var savingsAccount = new SavingsAccount(0);

      var henry = new Customer("Henry", 0);

      henry.OpenAccount(checkingAccount);
      henry.OpenAccount(savingsAccount);

      checkingAccount.Deposit(100.0);
      savingsAccount.Deposit(4000.0);
      savingsAccount.Withdraw(200.0);

      Assert.AreEqual("Statement for Henry\n" +
              "\n" +
              "Checking Account\n" +
              "  deposit $100.00\n" +
              "Total $100.00\n" +
              "\n" +
              "Savings Account\n" +
              "  deposit $4,000.00\n" +
              "  withdrawal $200.00\n" +
              "Total $3,800.00\n" +
              "\n" +
              "Total In All Accounts $3,900.00", henry.Statement);
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
      var acct = AccountCreator.GetAccount(AccountType.CHECKING, cust.NumberOfAccounts);
      Assert.AreEqual(1, cust.OpenAccount(acct).NumberOfAccounts);
    }

    [TestMethod]
    public void OpenAccount_by_AccountType_lastAccountId_and_maybe_initialDeposit() {
      cust.OpenAccount(AccountType.CHECKING, cust.NumberOfAccounts);
      Assert.AreEqual(1, cust.NumberOfAccounts);

      //cust.OpenAccount(AccountType.CHECKING, cust.NumberOfAccounts);
      //Assert.AreEqual(1, cust.NumberOfAccounts);
    }

    #endregion

    #region TransferFunds

    [Ignore]
    [TestMethod]
    public void TransferFunds_fromAccountId_arg_value_must___Aaaand___I_FORGET_WHAT_IT_MUST_getting_too_dang_late___() { }

    #endregion

    #endregion

    #region Protected Methods (Stubs)

    #region StatementForAccount

    [Ignore]
    [TestMethod]
    public void StatementForAccount_Stub() { }

    #endregion

    #region ToDollars

    [Ignore]
    [TestMethod]
    public void ToDollars_Stub() { }

    #endregion

    #endregion

  }
}
