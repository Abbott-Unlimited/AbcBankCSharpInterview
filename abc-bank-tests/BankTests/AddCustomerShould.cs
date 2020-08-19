using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests.BankTests
{
    [TestClass]
    public class AddCustomerShould
    {
        [TestMethod]
        public void OnboardNewCustomerToExistingBank()
        {
            var bank = new Bank();

            Assert.AreEqual(0, bank.customers.Count);

            bank.AddCustomer(new Customer("FirstCust"));

            Assert.AreEqual(1, bank.customers.Count);
        }
    }
}
