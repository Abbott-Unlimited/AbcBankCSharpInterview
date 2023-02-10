using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests.Customers {
  
  [TestClass]
  public class CustomerTest {

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

  }
}
