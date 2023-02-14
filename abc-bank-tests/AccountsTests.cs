using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Accounts;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountsTests
    {
        [TestMethod]
        public void CheckingAccountCanTotalTransactions()
        {

            var account = AccountFactory.OpenAccount(AccountTypes.CHECKING);
            account.Deposit(300.02);
            account.Withdraw(300.00);
            var total = account.SumTransactions();
            Assert.IsTrue(total == 0.02);
        }

        [TestMethod]
        public void SavingsAccountCanTotalTransactions()
        {
            var account = AccountFactory.OpenAccount(AccountTypes.SAVINGS);
            account.Deposit(300.02);
            account.Withdraw(300.00);
            var total = account.SumTransactions();
            Assert.IsTrue(total == 0.02);
        }
        [TestMethod]
        public void MaxiSavingsAccountCanTotalTransactions()
        {
            var account = AccountFactory.OpenAccount(AccountTypes.MAXI_SAVINGS);
            account.Deposit(300.02);
            account.Withdraw(300.00);
            var total = account.SumTransactions();
            Assert.IsTrue(total == 0.02);
        }
    }
}
