using abc_bank.Accounts;
using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
    [TestClass]
    public class TestManager
    {
        [TestMethod]
        public void TestTotalInterestPaid()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            Account billChecking = bill.OpenAccount(AccountType.Checking);

            billChecking.Deposit(100.00);

            Customer bob = new Customer("Bob");
            bank.AddCustomer(bob);
            Account bobChecking = bob.OpenAccount(AccountType.Checking);

            bobChecking.Deposit(100.00);

            Manager bossMan = new Manager(bank);

            DateProvider.AdjustDateByDays(1);

            Assert.AreEqual("Total Interest Paid: $0.20", bossMan.GetTotalInterestPaidReport());
        }
    }
}
