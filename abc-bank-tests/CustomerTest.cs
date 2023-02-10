using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests.Customers {

  #region Fakes, Mocks and Stubs

  public class CustomerTestFake : Customer {
    public CustomerTestFake(string name) : base(name) { }
  }

  #endregion

  [TestClass]
  public class CustomerTest {

    #region Properties 

    #region Name

    [Ignore]
    [TestMethod]
    public void Name_Stub() { }

    #endregion     

    #region Accounts

    [Ignore]
    [TestMethod]
    public void Accounts_Stub() { }

    #endregion

    #region NumberOfAccounts

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_1_account() {
      var oscar = new Customer("Oscar").OpenAccount(AccountCreator.GetAccount(AccountType.CHECKING));

      Assert.AreEqual(1, oscar.NumberOfAccounts);
    }

    [TestMethod]
    public void Customer_NumberOfAccounts_result_with_3_accounts() {
      var oscar = new Customer("Oscar")
        .OpenAccount(AccountCreator.GetAccount(AccountType.CHECKING))
        .OpenAccount(AccountCreator.GetAccount(AccountType.SAVINGS))
        .OpenAccount(AccountCreator.GetAccount(AccountType.MAXI_SAVINGS));

      Assert.AreEqual(3, oscar.NumberOfAccounts);
    }

    #endregion

    #region Statement

    [TestMethod]
    public void Customer_Statement_Report_Test() {
      // observation:   It's kinda weird we can open an account for a customer
      //                without having the customer belong to a bank...
      var checkingAccount = new CheckingAccount();
      var savingsAccount = new SavingsAccount();

      var henry = new Customer("Henry");

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

    [Ignore]
    [TestMethod]
    public void OpenAccount_IAccount_Instance() { }

    [Ignore]
    [TestMethod]
    public void OpenAccount_IAccount_AccountType_and_initialDeposit() { }

    #endregion

    #region TransferFunds

    [Ignore]
    [TestMethod]
    public void TransferFunds_Stub() { }

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
