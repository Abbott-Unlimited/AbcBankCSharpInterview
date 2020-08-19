using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests.CustomerTests
{
    [TestClass]
    public class OpenAcountShould
    {
        [TestMethod]
        public void TestOneAccount()
        {
            var customer = new Customer("Oscar").OpenAccount(new Account(AccountType.SAVINGS));
            Assert.AreEqual(1, customer.accounts.Count);
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            var customer = new Customer("Oscar").OpenAccount(new Account(AccountType.SAVINGS));
            customer.OpenAccount(new Account(AccountType.CHECKING));
            Assert.AreEqual(2, customer.accounts.Count);
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            var customer = new Customer("Oscar")
                    .OpenAccount(new Account(AccountType.SAVINGS));
            customer.OpenAccount(new Account(AccountType.CHECKING));
            customer.OpenAccount(new Account(AccountType.MAXI_SAVINGS));

            Assert.AreEqual(3, customer.accounts.Count);
        }
    }
}
