using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;
using abc_bank.Customers;

namespace abc_bank_tests.Bank {

  [TestClass]
  public class Bank_Tests {

    #region Setup and Teardown

    abc_bank.Bank bank;
    StringBuilder sb;

    [TestInitialize]
    public void Init() {
      bank = new abc_bank.Bank();
      sb = new StringBuilder();
    }

    [TestCleanup]
    public void Cleanup() {
      bank = null;
      sb = null;
    }

    #endregion

    #region NumberOfCustomers

    [TestMethod]
    public void NumberOfCustomers_returns_0_when_no_customers_have_been_added() {
      Assert.AreEqual(0, bank.NumberOfCustomers);
    }

    [TestMethod]
    public void NumberOfCustomers_returns_accurate_count_of_bank_customers() {
      Assert.AreEqual(0, bank.NumberOfCustomers);

      bank.AddCustomer(new Customer("Bobby", bank.NumberOfCustomers));
      Assert.AreEqual(1, bank.NumberOfCustomers);

      bank.AddCustomer(new Customer("Timmy", bank.NumberOfCustomers));
      bank.AddCustomer(new Customer("Tommy", bank.NumberOfCustomers));
      bank.AddCustomer(new Customer("Terry", bank.NumberOfCustomers));
      bank.AddCustomer(new Customer("Randy", bank.NumberOfCustomers));
      bank.AddCustomer(new Customer("Rusty", bank.NumberOfCustomers));

      Assert.AreEqual(6, bank.NumberOfCustomers);
    }

    #endregion

    #region HasCustomers

    [TestMethod]
    public void HasCustomers_is_true_Test() {
      bank.AddCustomer(new Customer("Jack", bank.NumberOfCustomers));

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
      bank.AddCustomer(new Customer("Jenny", bank.NumberOfCustomers));
      bank.AddCustomer(new Customer("Bob", bank.NumberOfCustomers));
      bank.AddCustomer(new Customer("Jack", bank.NumberOfCustomers));

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
      bank.AddCustomer(new Customer("John", bank.NumberOfCustomers + 1).OpenAccount(new CheckingAccount(0, 1)));

      sb.AppendLine("Customer Summary");
      sb.AppendLine(" - John (1 account)");

      Assert.AreEqual(sb.ToString(), bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_1_Customer_2_accounts() {
      bank.AddCustomer(new Customer("John", bank.NumberOfCustomers)
          .OpenAccount(new CheckingAccount(0, 1))
          .OpenAccount(new SavingsAccount(0, 1)));

      sb.AppendLine("Customer Summary");
      sb.AppendLine(" - John (2 accounts)");

      Assert.AreEqual(sb.ToString(), bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_3_Customers_1_account_each() {
      bank.AddCustomer(new Customer("John", bank.NumberOfCustomers).OpenAccount(new CheckingAccount(1, 1)));
      bank.AddCustomer(new Customer("Jimmy", bank.NumberOfCustomers).OpenAccount(new SavingsAccount(1, 1)));
      bank.AddCustomer(new Customer("Jack", bank.NumberOfCustomers).OpenAccount(new SavingsAccount(1, 1)));

      sb.AppendLine("Customer Summary");
      sb.AppendLine(" - John (1 account)");
      sb.AppendLine(" - Jimmy (1 account)");
      sb.AppendLine(" - Jack (1 account)");

      Assert.AreEqual(sb.ToString(), bank.CustomerSummary);
    }

    #endregion

    #region Add Customer

    [TestMethod]
    public void AddCustomer_By_New_Customer_Instance() {
      bank.AddCustomer(new Customer("Bob", bank.NumberOfCustomers));

      Assert.IsTrue(bank.HasCustomers);
    }

    [TestMethod]
    public void AddCustomer_By_Customer_Name() {
      bank.AddCustomer(new Customer("Bob", bank.NumberOfCustomers));

      Assert.IsTrue(bank.HasCustomers);
    }

    #endregion

    #region Total Interest Paid

    [TestMethod]
    public void Total_interest_paid_single_checking_account() {
      bank.AddCustomer(new Customer("Bill", bank.NumberOfCustomers).OpenAccount(new CheckingAccount(0, 1, 100)));

      Assert.AreEqual(0.10M, bank.TotalInterestPaid);
    }

    [TestMethod]
    public void Total_interest_paid_single_savings_account() {
      bank.AddCustomer(new Customer("Bill", bank.NumberOfCustomers).OpenAccount(new SavingsAccount(0, 1, 1000)));

      Assert.AreEqual(1, bank.TotalInterestPaid);
    }

    [TestMethod]
    public void Total_interest_paid_single_maxi_savings_account() {
      bank.AddCustomer(new Customer("Bill", bank.NumberOfCustomers).OpenAccount(new MaxiSavingsAccount(0, 1, 2000)));

      Assert.AreEqual(100, bank.TotalInterestPaid);
    }

    #endregion

  }
}
