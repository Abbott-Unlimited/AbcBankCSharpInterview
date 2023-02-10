using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests.BankTests {

  [TestClass]
  public class BankTest {

    #region Setup and Teardown

    Bank bank;

    [TestInitialize]
    public void Init() {
      bank = new Bank();
    }

    [TestCleanup]
    public void Cleanup() {
      bank = null;
    }

    #endregion

    #region HasCustomers

    [TestMethod]
    public void HasCustomers_is_true_Test() {
      bank.AddCustomer("Jack");

      Assert.IsTrue(bank.HasCustomers);
    }

    [TestMethod]
    public void HasCustomers_is_false_Test() {
      Assert.IsFalse(bank.HasCustomers);
    }

    #endregion

    #region First Bank Customer

    [TestMethod]
    public void First_Bank_Customer_Returns_No_Customers_Message_When_No_Customers() {
      Assert.AreEqual(Messages.NO_CUSTOMERS_MSG, bank.FirstCustomer);
    }

    [TestMethod]
    public void First_Bank_Customer_Returns_First_Customer_Name() {
      bank.AddCustomer("Jenny");
      bank.AddCustomer("Bob");
      bank.AddCustomer("Jack");

      Assert.AreEqual("Jenny", bank.FirstCustomer);
    }

    #endregion

    #region Customer Summary

    [TestMethod]
    public void Customer_Summary_With_No_Customers() {
      Assert.AreEqual(Messages.NO_CUSTOMERS_MSG, bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_1_Customer_1_account() {
      bank.AddCustomer("John").OpenAccount(AccountType.CHECKING);

      Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_1_Customer_2_accounts() {
      bank.AddCustomer("John")
        .OpenAccount(AccountType.CHECKING)
        .OpenAccount(AccountType.SAVINGS);

      Assert.AreEqual("Customer Summary\n - John (2 accounts)", bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_3_Customers_1_account_each() {
      bank.AddCustomer("John").OpenAccount(AccountType.CHECKING);
      bank.AddCustomer("Jimmy").OpenAccount(AccountType.SAVINGS);
      bank.AddCustomer("Jack").OpenAccount(AccountType.CHECKING);

      string expected = "Customer Summary\n";
      expected += " - John (1 account)\n";
      expected += " - Jimmy (1 account)\n";
      expected += " - Jack (1 account)";

      Assert.AreEqual(expected, bank.CustomerSummary);
    }

    #endregion

    #region Add Customer

    [TestMethod]
    public void AddCustomer_By_New_Customer_Instance() {
      bank.AddCustomer(new Customer("Bob"));

      Assert.IsTrue(bank.HasCustomers);
    }

    [TestMethod]
    public void AddCustomer_By_Customer_Name() {
      bank.AddCustomer("Bob");

      Assert.IsTrue(bank.HasCustomers);
    }

    #endregion

    #region Total Interest Paid

    [TestMethod]
    public void Total_interest_paid_single_checking_account() {
      bank.AddCustomer("Bill").OpenAccount(new CheckingAccount(100.00));

      Assert.AreEqual(0.10, bank.TotalInterestPaid, Constants.DOUBLE_DELTA);
    }

    [TestMethod]
    public void Total_interest_paid_single_savings_account() {
      bank.AddCustomer("Bill").OpenAccount(new SavingsAccount(1000.00));

      Assert.AreEqual(1.00, bank.TotalInterestPaid, Constants.DOUBLE_DELTA);
    }

    [TestMethod]
    public void Total_interest_paid_single_maxi_savings_account() {
      bank.AddCustomer("Bill").OpenAccount(new MaxiSavingsAccount(2000.00));

      Assert.AreEqual(100.00, bank.TotalInterestPaid, Constants.DOUBLE_DELTA);
    }

    #endregion
  
  }
}
