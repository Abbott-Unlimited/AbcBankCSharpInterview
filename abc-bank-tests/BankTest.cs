using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;
using System.Security.Principal;

namespace abc_bank_tests.BankTests {
  [TestClass]
  public class BankTest {

    //  used for ensuring truncation does not cause rounding issues, etc - I assume?
    private static readonly double DOUBLE_DELTA = 1e-15; // 1e-15 = 0.000000000000001

    [TestMethod]
    public void HasCustomers_Test() {
      var bank = new Bank();

      Assert.AreEqual(false, bank.HasCustomers);

      bank.AddCustomer("Jack");

      Assert.AreEqual(true, bank.HasCustomers);
    }

    [TestMethod]
    public void First_Bank_Customer_Does_Not_Error_When_No_Customers() {
      var bank = new Bank();

      Assert.AreEqual(Messages.NO_CUSTOMERS_MSG, bank.FirstCustomer);
    }

    [TestMethod]
    public void First_Bank_Customer_Returns_First_Customer_Name() {
      var bank = new Bank();
      bank.AddCustomer("Jenny");
      bank.AddCustomer("Bob");
      bank.AddCustomer("Jack");

      Assert.AreEqual("Jenny", bank.FirstCustomer);

    }

    [TestMethod]
    public void Customer_Summary_With_No_Customers() {
      var bank = new Bank();

      Assert.AreEqual(Messages.NO_CUSTOMERS_MSG, bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_1_Customer_1_account() {
      var bank = new Bank();
      var john = new Customer("John");
      var acct = new CheckingAccount();

      bank.AddCustomer(john);
      john.OpenAccount(acct);

      Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_1_Customer_2_accounts() {
      var bank = new Bank();
      var john = bank.AddCustomer("John");

      john.OpenAccount(AccountType.CHECKING);
      john.OpenAccount(AccountType.SAVINGS);

      Assert.AreEqual("Customer Summary\n - John (2 accounts)", bank.CustomerSummary);
    }

    [TestMethod]
    public void Customer_Summary_With_3_Customers_1_account_each() {
      var bank = new Bank();
      bank.AddCustomer("John").OpenAccount(AccountType.CHECKING);
      bank.AddCustomer("Jimmy").OpenAccount(AccountType.SAVINGS);
      bank.AddCustomer("Jack").OpenAccount(AccountType.CHECKING);

      string expected = "Customer Summary\n";
      expected += " - John (1 account)\n";
      expected += " - Jimmy (1 account)\n";
      expected += " - Jack (1 account)";

      Assert.AreEqual(expected, bank.CustomerSummary);
    }

    [TestMethod]
    public void CheckingAccount() {
      var bank = new Bank();
      var bill = new Customer("Bill");
      var account = new CheckingAccount();

      bill.OpenAccount(account);
      bank.AddCustomer(bill);
      account.Deposit(100.0);

      Assert.AreEqual(0.1, bank.TotalInterestPaid, DOUBLE_DELTA);
    }

    [TestMethod]
    public void AddCustomer_By_Customer_Name() { 
      
    }

    #region Broken Tests

    [Ignore]
    [TestMethod]
    public void Savings_account() {
      var bank = new Bank();
      var account = new SavingsAccount();
      var bill = new Customer("Bill");

      bank.AddCustomer(bill);
      bill.OpenAccount(account);
      account.Deposit(1500.0);

      Assert.AreEqual(2.0, bank.TotalInterestPaid, DOUBLE_DELTA);
    }

    [Ignore]
    [TestMethod]
    public void Maxi_savings_account() {
      Assert.Fail("MaxiSavingsAccount accrued interest is not yet implemented properly");

      var bank = new Bank();
      var account = new MaxiSavingsAccount();
      var bill = new Customer("Bill");

      bank.AddCustomer(bill);
      bill.OpenAccount(account);
      account.Deposit(3000.0);

      Assert.AreEqual(170.0, bank.TotalInterestPaid, DOUBLE_DELTA);
    }

    #endregion
  }
}
