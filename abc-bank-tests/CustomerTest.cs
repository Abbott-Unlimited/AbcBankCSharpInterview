using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests {
  [TestClass]
  public class CustomerTest {
    [TestMethod]
    public void TestApp() {
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

    [TestMethod]
    public void TestOneAccount() {
      var oscar = new Customer("Oscar");
      oscar.OpenAccount(new SavingsAccount());

      Assert.AreEqual(1, oscar.NumberOfAccounts);
    }

    [TestMethod]
    public void TestTwoAccount() {
      var oscar = new Customer("Oscar");
      oscar.OpenAccount(new CheckingAccount());
      oscar.OpenAccount(new SavingsAccount());

      Assert.AreEqual(2, oscar.NumberOfAccounts);
    }

    [TestMethod]
    public void TestThreeAccounts() {
      var oscar = new Customer("Oscar");
      oscar.OpenAccount(new CheckingAccount());
      oscar.OpenAccount(new SavingsAccount());
      oscar.OpenAccount(new MaxiSavingsAccount());

      Assert.AreEqual(3, oscar.NumberOfAccounts);
    }
  }
}
