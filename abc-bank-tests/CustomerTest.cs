using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABC_bank;

namespace ABC_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual("Statement for Henry" + Environment.NewLine +
                    Environment.NewLine +
                    "Checking Account" + Environment.NewLine +
                    "  deposit $100.00" + Environment.NewLine +
                    "Total $100.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Savings Account" + Environment.NewLine +
                    "  deposit $4,000.00" + Environment.NewLine +
                    "  withdrawal $200.00" + Environment.NewLine +
                    "Total $3,800.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [Ignore]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
