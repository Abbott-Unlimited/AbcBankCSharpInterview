using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.BankTests
{
    [TestClass]
    public class CalculateTotalInterestPaidShould
    {
        [TestMethod]
        public void CalculateTotalInterestOnAllAccountsHeldByCustomer()
        {
            var sut = new Bank();
            var checkingAccount = new Account(AccountType.CHECKING);
            var svgs = new Account(AccountType.SAVINGS);
            var customer = new Customer("Bill").OpenAccount(checkingAccount);
            var startingbal = 10000;

            checkingAccount.AddTransaction(startingbal);
            svgs.AddTransaction(startingbal);
            var expected = svgs.CalculateInterestEarned() + checkingAccount.CalculateInterestEarned();

            customer.OpenAccount(svgs);
            sut.AddCustomer(customer);
            
            var result = sut.CalculateTotalInterestPaid();

            Assert.AreEqual(expected, result);
        }
    }
}
