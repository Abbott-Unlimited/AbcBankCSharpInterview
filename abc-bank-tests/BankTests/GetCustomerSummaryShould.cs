using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests.BankTests
{
    [TestClass]
    public class GetCustomerSummaryShould
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void ReturnCustomerSummary_SINGULAR_IfOnlyOneAccount()
        {
            var sut = new Bank();
            var customer = new Customer("John");
            customer.OpenAccount(new Account(AccountType.CHECKING));
            sut.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - John (1 account)", sut.CustomerSummary());
        }

        [TestMethod]
        public void IfMultipleAccounts_UsePluralityInCustomerSummary()
        {
            var sut = new Bank();
            var customer = new Customer("John");
            customer.OpenAccount(new Account(AccountType.SAVINGS));
            customer.OpenAccount(new Account(AccountType.CHECKING));
            sut.AddCustomer(customer);

            Assert.AreEqual("Customer Summary\n - John (2 accounts)", sut.CustomerSummary());
        }

        [TestMethod]
        public void MaxiSavingsAcctInterestRateIsCorrect()
        {
            var sut = new Bank();
            var svgsAcct = new Account(AccountType.MAXI_SAVINGS);
            sut.AddCustomer(new Customer("Bill").OpenAccount(svgsAcct));

            svgsAcct.AddTransaction(3000.0);

            var result = sut.CalculateTotalInterestPaid();

            Assert.AreEqual(170.0, result);
        }
    }
}
