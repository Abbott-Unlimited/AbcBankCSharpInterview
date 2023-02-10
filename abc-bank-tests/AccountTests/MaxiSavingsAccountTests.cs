using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;

namespace abc_bank_tests.Accounts {

  [TestClass]
  public class MaxiSavingsAccountTests {

    [Ignore]
    [TestMethod]
    public void Account_1000_Or_Less_Interest() {
      Assert.Fail("This is the savings calculation, not maxi-savings");

      IAccount acct = new MaxiSavingsAccount();
      //acct.Deposit(1000);

      //Assert.AreEqual(1, acct.InterestEarned);
    }

    [Ignore]
    [TestMethod]
    public void Account_Over_1000_Interest() {
      Assert.Fail("This is the savings calculation, not maxi-savings");

      IAccount acct = new MaxiSavingsAccount();
      //acct.Deposit(2000);

      //Assert.AreEqual(4, acct.InterestEarned);
    }

    // todo:  Could probably use a few more tests here to cover
    //        No balance, negative balance, etc.  Can wait however.
  }
}
