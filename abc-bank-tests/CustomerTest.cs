using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
  [TestClass]
  public class CustomerTest
  {
    private static readonly double DOUBLE_DELTA = 1e-15;


    [TestMethod]
    public void TestStatement()
    {
      Account checkingAccount = new Account(Account.CHECKING);
      Account savingsAccount = new Account(Account.SAVINGS);

      Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

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
              "Total In All Accounts $3,900.00", henry.GetStatement());
    }

    [TestMethod]
    public void TestOneAccount()
    {
      Customer oscar = new Customer("Oscar");

      oscar.OpenAccount(new Account(Account.SAVINGS));

      Assert.AreEqual(1, oscar.GetNumberOfAccounts());
    }

    [TestMethod]
    public void TestTwoAccounts()
    {
      Customer oscar = new Customer("Oscar");

      oscar.OpenAccount(new Account(Account.SAVINGS));
      oscar.OpenAccount(new Account(Account.CHECKING));

      Assert.AreEqual(2, oscar.GetNumberOfAccounts());
    }

    [TestMethod]
    public void TestThreeAccounts()
    {
      Customer oscar = new Customer("Oscar");

      oscar.OpenAccount(new Account(Account.SAVINGS));
      oscar.OpenAccount(new Account(Account.CHECKING));
      oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));

      Assert.AreEqual(3, oscar.GetNumberOfAccounts());
    }

    [TestMethod]
    public void TestAccountTransfer()
    {
      Customer oscar = new Customer("Oscar");

      Account savings = new Account(Account.SAVINGS);
      Account checking = new Account(Account.CHECKING);
      oscar.OpenAccount(savings);
      oscar.OpenAccount(checking);

      savings.Deposit(200.0);
      checking.Deposit(350.0);

      savings.TransferToAccount(checking, 100.0);

      Assert.AreEqual(100.0, savings.sumTransactions(), DOUBLE_DELTA);
      Assert.AreEqual(450.0, checking.sumTransactions(), DOUBLE_DELTA);
    }
  }
}
