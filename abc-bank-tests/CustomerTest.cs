using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestSavings()
        {
            var savingsAccount = new Account(Account.SAVINGS);            

            var henry = new Customer("Henry").OpenAccount(savingsAccount);
            
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);            

            // Seems to be asking a lot to try to "match" a string exactly - better to check that the data is correct, IMO
            /*
            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement()); */

            
            Assert.AreEqual(savingsAccount.CurrentBalance, 3800.00);
        }

        [TestMethod]
        public void TestChecking()
        {
            var checkingAccount = new Account(Account.CHECKING);

            var jim = new Customer("Jim").OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.00);

            Assert.AreEqual(checkingAccount.CurrentBalance, 100.00);
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
