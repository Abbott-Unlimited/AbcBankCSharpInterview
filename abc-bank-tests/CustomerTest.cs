using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
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
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void Transfer_Successful()
        {
            var savings = new Account(Account.SAVINGS);
            var checking = new Account(Account.CHECKING);
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(savings);
            oscar.OpenAccount(checking);

            checking.Deposit(1000);
            savings.Deposit(500);

            oscar.Transfer(checking, savings, 500);

            Assert.AreEqual(500, checking.sumTransactions());
            Assert.AreEqual(1000, savings.sumTransactions());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transfer_ThrowsException_InvalidAccounts()
        {
            var savings = new Account(Account.SAVINGS);
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(savings);

            savings.Deposit(500);

            oscar.Transfer(null, savings, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transfer_ThrowsException_InvalidAccounts2()
        {
            var savings = new Account(Account.SAVINGS);
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(savings);

            savings.Deposit(500);

            oscar.Transfer(savings, null, 500);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transfer_ThrowsException_InvalidAmount()
        {
            var savings = new Account(Account.SAVINGS);
            var checking = new Account(Account.CHECKING);
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(savings);
            oscar.OpenAccount(checking);

            checking.Deposit(1000);
            savings.Deposit(500);
            
            oscar.Transfer(checking, savings, -100);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Transfer_ThrowsException_SameAccount()
        {
            var savings = new Account(Account.SAVINGS);
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(savings);

            savings.Deposit(500);

            oscar.Transfer(savings, savings, 500);
        }
    }
}
