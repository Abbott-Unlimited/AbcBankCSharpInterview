using Microsoft.VisualStudio.TestTools.UnitTesting;

using abc_bank.Accounts;

namespace abc_bank_tests.Accounts {
  [TestClass]
  public class CheckingAccountTests {

    [TestMethod]
    public void Interest_Accrued() {
      IAccount acct = new CheckingAccount(0);
      acct.Deposit(1000);

      Assert.AreEqual(1, acct.InterestEarned);
    }

    // todo:  Could probably use a few more tests here to cover
    //        No balance, negative balance, etc.  Can wait however.
  }
}
