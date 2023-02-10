using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;

namespace abc_bank_tests.Accounts {

  [TestClass]
  public class SavingsAccountTests {

    [TestMethod]
    public void Accrued_Interest_With_Balance_1000_Or_Less() {
      IAccount acct = new SavingsAccount();
      acct.Deposit(1000);

      Assert.AreEqual(1, acct.InterestEarned);
    }

    [TestMethod]
    public void Accrued_Interest_With_Balance_Greater_Than_1000() {
      IAccount acct = new SavingsAccount();
      acct.Deposit(2000);

      Assert.AreEqual(4, acct.InterestEarned);
    }
  }
}
