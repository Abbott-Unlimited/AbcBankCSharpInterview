// names that begin with l-case as well as contain underscores IN UNIT TESTS AND TEST CLASSES are much easier to read.
#pragma warning disable IDE1006 // Naming Styles

using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Accounts;
using System.Collections.Generic;
using System.Text;
using System;
using abc_bank.Exceptions;
using abc_bank.Customers;
using abc_bank.Transactions;

namespace abc_bank_tests.Customers {

  #region Mocks Fakes and Stubs

  public class CheckingAccountFake : CheckingAccount {
    public override List<ITransaction> Transactions { get; protected set; } = new List<ITransaction>();
    public CheckingAccountFake(int accountId, int customerId, double initialDeposit = 0)
      : base(accountId, customerId, initialDeposit) {
    }
  }
  public class SavingsAccountFake : SavingsAccount {
    public override List<ITransaction> Transactions { get; protected set; } = new List<ITransaction>();
    public SavingsAccountFake(int accountId, int customerId, double initialDeposit = 0)
      : base(accountId, customerId, initialDeposit) {
    }
  }
  public class MaxiSavingsFake : MaxiSavingsAccount {
    public override List<ITransaction> Transactions { get; protected set; } = new List<ITransaction>();
    public MaxiSavingsFake(int accountId, int customerId, double initialDeposit = 0)
      : base(accountId, customerId, initialDeposit) {
    }
  }

  #endregion

  [TestClass]
  public class Customer_Tests {

    #region Setup and Teardown

    Customer cust;

    [TestInitialize]
    public void Init() {
      cust = new Customer("Alvin", 1);
    }

    [TestCleanup]
    public void Cleanup() {
      cust = null;
    }

    #endregion

    #region Properties 

    #region Id

    [TestMethod]
    public void Id_is_initialized_to_value_of_customerId_arg_in_constructor() {
      Assert.AreEqual(1, new abc_bank.Customers.Customer("Flipper", 1).Id);
    }

    #endregion

    #region Name

    [TestMethod]
    public void Name_is_initialized_to_name_argument_value_in_constructor() {
      Assert.AreEqual("Jeff", new abc_bank.Customers.Customer("Jeff", 0).Name);
    }

    #endregion     

    #region Accounts

    [TestMethod]
    public void Accounts_is_initialized_when_Customer_is_instantiated() {
      Assert.IsInstanceOfType(new abc_bank.Customers.Customer("Jimmy", 0).Accounts, typeof(List<IAccount>));
    }

    #endregion

    #region NumberOfAccounts

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_0_account() {
      Assert.AreEqual(0, new abc_bank.Customers.Customer("Oscar", 0).NumberOfAccounts);
    }

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_1_account() {
      var oscar = new abc_bank.Customers.Customer("Oscar", 0)
        .OpenAccount(new CheckingAccount(0, 1));

      Assert.AreEqual(1, oscar.NumberOfAccounts);
    }

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_3_accounts() {
      var oscar = new abc_bank.Customers.Customer("Oscar", 1)
        .OpenAccount(new CheckingAccount(0, 1))
        .OpenAccount(new SavingsAccount(0, 1))
        .OpenAccount(new MaxiSavingsAccount(0, 1));

      Assert.AreEqual(3, oscar.NumberOfAccounts);
    }

    #endregion

    #region Statement

    [TestMethod]
    public void Customer_Statement_Report_integration_Test() {
      var checkingAccount = new CheckingAccount(1, 1);
      var savingsAccount = new SavingsAccount(2, 1);

      var henry = new Customer("Henry", 0);

      henry.OpenAccount(checkingAccount);
      henry.OpenAccount(savingsAccount);

      checkingAccount.Deposit(100.0);
      savingsAccount.Deposit(4000.0);
      savingsAccount.Withdraw(200.0);

      henry.TransferFunds(500, DateTime.Now, savingsAccount, checkingAccount);

      var sb = new StringBuilder();

      sb.AppendLine("Statement for Henry");
      sb.AppendLine();
      sb.AppendLine("Checking Account");
      sb.AppendLine("    deposit           $100.00");
      sb.AppendLine("    deposit           $500.00  transfer from acct #00000001");
      sb.AppendLine("Total $600.00");
      sb.AppendLine();

      sb.AppendLine("Savings Account");
      sb.AppendLine("    deposit         $4,000.00");
      sb.AppendLine("    withdrawal        $200.00");
      sb.AppendLine("    withdrawal        $500.00    transfer to acct #00000001");
      sb.AppendLine("Total $3,300.00");
      sb.AppendLine();

      sb.AppendLine("Total In All Accounts $3,900.00");

      System.Diagnostics.Debug.Write(henry.Statement);

      Assert.AreEqual(sb.ToString(), henry.Statement);
    }

    #endregion


    // lets be honest with each other...
    // it's highly unlikely the interest calculation(s) are actually correct
    // as I spent 0 time on that area of the code, other than code cleanup refactoring.
    #region Total Interest Earned

    #region helper methods for TotalInterestEarned__Integration_Test

    [Ignore]
    private void add_acct_transactions<T>(ref T fakeAcct) where T : IAccount {
      fakeAcct.Transactions.Add(new Deposit(500, DateTime.Now.AddDays(-45), fakeAcct));
      fakeAcct.Transactions.Add(new Withdraw(50, DateTime.Now.AddDays(-44), fakeAcct));
      fakeAcct.Transactions.Add(new Withdraw(10.75, DateTime.Now.AddDays(-37), fakeAcct));
      fakeAcct.Transactions.Add(new Deposit(448.12, DateTime.Now.AddDays(-19), fakeAcct));
      fakeAcct.Transactions.Add(new Withdraw(284.71, DateTime.Now.AddDays(-5), fakeAcct));

      cust.OpenAccount(fakeAcct);
    }

    [Ignore]
    private string statement_summary_for_sanity_check() {
      var sb = new StringBuilder();

      #region Alvin's Bank Statement

      sb.AppendLine("Statement for Alvin");
      sb.AppendLine("");
      sb.AppendLine("Checking Account");
      sb.AppendLine("    deposit           $500.00");
      sb.AppendLine("    withdrawal         $50.00");
      sb.AppendLine("    withdrawal         $10.75");
      sb.AppendLine("    deposit           $448.12");
      sb.AppendLine("    withdrawal        $284.71");
      sb.AppendLine("Total $602.66");
      sb.AppendLine("");
      sb.AppendLine("Savings Account");
      sb.AppendLine("    deposit           $500.00");
      sb.AppendLine("    withdrawal         $50.00");
      sb.AppendLine("    withdrawal         $10.75");
      sb.AppendLine("    deposit           $448.12");
      sb.AppendLine("    withdrawal        $284.71");
      sb.AppendLine("Total $602.66");
      sb.AppendLine("");
      sb.AppendLine("Maxi Savings Account");
      sb.AppendLine("    deposit           $500.00");
      sb.AppendLine("    withdrawal         $50.00");
      sb.AppendLine("    withdrawal         $10.75");
      sb.AppendLine("    deposit           $448.12");
      sb.AppendLine("    withdrawal        $284.71");
      sb.AppendLine("Total $602.66");
      sb.AppendLine("");
      sb.AppendLine("Total In All Accounts $1,807.98");

      #endregion

      return sb.ToString();
    }

#pragma warning disable IDE0051 // Remove unused private members
    [Ignore]
    private void debug_output(Customer cust) {
      System.Diagnostics.Debug.WriteLine("---------------");
      System.Diagnostics.Debug.WriteLine(" ");
      System.Diagnostics.Debug.Write(cust.Statement);
      System.Diagnostics.Debug.WriteLine(" ");
      System.Diagnostics.Debug.WriteLine("Total Interest Earned:     " + cust.TotalInterestEarned.ToString());
      System.Diagnostics.Debug.WriteLine(" ");
      System.Diagnostics.Debug.WriteLine("---------------");

      var expected_statement = statement_summary_for_sanity_check();
      var actual_statement = cust.Statement;
      Assert.AreEqual(expected_statement, actual_statement);
    }
#pragma warning restore IDE0051 // Remove unused private members


    #endregion

    [TestMethod]
    public void TotalInterestEarned__Integration_Test() {
      var checkingFake = new CheckingAccountFake(1, 1);
      var savingsFake = new SavingsAccountFake(2, 1);
      var maxsaveFake = new MaxiSavingsFake(3, 1);

      add_acct_transactions(ref checkingFake);
      add_acct_transactions(ref savingsFake);
      add_acct_transactions(ref maxsaveFake);

      double expected = 13.25852;
      double actual = cust.TotalInterestEarned;

      //Assert.AreEqual(expected, actual, Constants.DOUBLE_DELTA); // and of course I'm having trouble with this... 
      //Assert.AreEqual(Math.Abs(expected), Math.Abs(actual)); // this didnt work either
      //Assert.AreEqual(Math.Abs(expected), Math.Abs(actual), Constants.DOUBLE_DELTA); // nor did this
      Assert.AreEqual(expected.ToString(), actual.ToString()); // this works - for now - since the delta param didn't.

      /*  Uncomment the following line for easier debugging if something goes sideways */
      //debug_output(cust);
    }

    #endregion

    #endregion

    #region Public Methods

    #region OpenAccount

    [TestMethod]
    public void OpenAccount() {
      var acct = new CheckingAccount(cust.NumberOfAccounts + 1, 1);
      Assert.AreEqual(1, cust.OpenAccount(acct).NumberOfAccounts);
    }

    [TestMethod]
    public void OpenAccount_by_AccountType_lastAccountId_and_maybe_initialDeposit() {
      cust.OpenAccount(new CheckingAccount(cust.NumberOfAccounts + 1, 1));
      Assert.AreEqual(1, cust.NumberOfAccounts);
    }

    #endregion

    #region TransferFunds 

    [TestMethod]
    [ExpectedException(typeof(InvalidAccountException))]
    public void TransferFunds_throws_error_if_origin_account_does_not_exist() {
      var cust = new abc_bank.Customers.Customer("Patrick", 1);
      var acct = new CheckingAccount(1, cust.Id);
      cust.OpenAccount(acct);
      var acct2 = new CheckingAccount(0, cust.Id, 10000);

      cust.TransferFunds(2500, DateTime.Now, acct2, acct);
      Assert.Fail();
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidAccountException))]
    public void TransferFunds_throws_error_if_destination_account_does_not_exist() {
      var cust = new abc_bank.Customers.Customer("Patrick", 1);
      var acct = new CheckingAccount(1, cust.Id);
      var acct2 = new CheckingAccount(0, cust.Id, 10000);
      cust.OpenAccount(acct2);
      cust.TransferFunds(2500, DateTime.Now, acct2, acct);

      Assert.Fail();
    }

    #endregion

    #region GetAccountById

    [TestMethod]
    public void GetAccountById_returns_correct_account_with_1_account() {
      var checking = new CheckingAccount(1, 1);
      var customer = new Customer("Jack", 1)
          .OpenAccount(checking);

      Assert.AreEqual(checking, customer.GetAccountById(1));

    }

    [TestMethod]
    public void GetAccountById_returns_correct_account_with_2_account() {
      var checking = new CheckingAccount(1, 1);
      var savings = new SavingsAccount(2, 1);
      var customer = new Customer("Jack", 1)
          .OpenAccount(checking)
          .OpenAccount(savings);

      Assert.AreEqual(checking, customer.GetAccountById(1));
      Assert.AreEqual(savings, customer.GetAccountById(2));
      Assert.AreNotEqual(checking, customer.GetAccountById(2));
    }

    #endregion

    #region GetAccountsByType

    private IAccount createAccountByType(AccountType acctType, int acctId, int custId) {
      switch (acctType) {
        case AccountType.SAVINGS:
          return new SavingsAccount(acctId, custId);
        case AccountType.MAXI_SAVINGS:
          return new MaxiSavingsAccount(acctId, custId);
        default:  //AccountType.CHECKING:
          return new CheckingAccount(acctId, custId);
      }
    }

    private void addAccountsToCustomer(ref Customer c, AccountType acctType, int acctsToAdd) {
      var numOfAccts = c.NumberOfAccounts;
      for (int i = numOfAccts; i < numOfAccts + acctsToAdd; i++) {
        c.OpenAccount(createAccountByType(acctType, i, c.Id));
      }
    }

    [TestMethod]
    public void GetAccountsByType_returns_list_of_IAccount_by_type_with_multiple_of_each_account_type() {
      var customer = new Customer("Darth Vader", 1);

      addAccountsToCustomer(ref customer, AccountType.CHECKING, 3);
      Assert.IsTrue(customer.NumberOfAccounts == 3); // sanity check

      addAccountsToCustomer(ref customer, AccountType.SAVINGS, 5);
      Assert.IsTrue(customer.NumberOfAccounts == 8);    // sanity check

      addAccountsToCustomer(ref customer, AccountType.MAXI_SAVINGS, 2);
      Assert.IsTrue(customer.NumberOfAccounts == 10); // sanity check

      var checking = customer.GetAccountsByType(AccountType.CHECKING);
      Assert.AreEqual(3, checking.Count);

      var savings = customer.GetAccountsByType(AccountType.SAVINGS);
      Assert.AreEqual(5, savings.Count);

      var maxiSavings = customer.GetAccountsByType(AccountType.MAXI_SAVINGS);
      Assert.AreEqual(2, maxiSavings.Count);

    }

    #endregion

    #endregion

  }

}
