using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Customer henry = new Customer("Henry");

            Account checkingAccount = henry.OpenAccount(AccountType.Checking);
            Account savingsAccount = henry.OpenAccount(AccountType.Savings);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            var result = henry.GetStatement();
            var expected = "Statement for Henry\r\n" +
                    "\r\n" +
                    "Checking Account\r\n" +
                    "  deposit $100.00\r\n" +
                    "Total $100.00\r\n" +
                    "\r\n" +
                    "Savings Account\r\n" +
                    "  deposit $4,000.00\r\n" +
                    "  withdrawal $200.00\r\n" +
                    "Total $3,800.00\r\n" +
                    "\r\n" +
                    "Total In All Accounts $3,900.00";

            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(AccountType.Savings);
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(AccountType.Savings);
            oscar.OpenAccount(AccountType.Checking);
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(AccountType.Savings);
            oscar.OpenAccount(AccountType.Checking);
            oscar.OpenAccount(AccountType.Maxi);
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
