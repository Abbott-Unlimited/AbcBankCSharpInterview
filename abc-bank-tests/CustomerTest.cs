using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        public DateTime currentdate;
    [TestInitialize]
    public void Setup()
        {
            currentdate = DateProvider.getInstance().Now();
        }

        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0, currentdate);
            savingsAccount.Deposit(4000.0, currentdate);
            savingsAccount.Withdraw(200.0, currentdate);

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

            //"Statement for Henry\n\nChecking Account\n  deposit $%,.2f\nTotal $%,.2f\n\nSavings Account\n  deposit $%,.2f\n  withdrawal $%,.2f\nTotal $%,.2f\n\nTotal In All Accounts $%,.2f"
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
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
        [TestMethod]
        public void TestInternalDeposit()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0, currentdate);
            savingsAccount.Deposit(4000.0, currentdate);
            savingsAccount.Withdraw(200.0, currentdate);

            Assert.IsTrue(henry.InternalTransfer(checkingAccount, savingsAccount, 50));
            Assert.IsFalse(henry.InternalTransfer(checkingAccount, savingsAccount, 2000));
            Assert.IsTrue(henry.InternalTransfer(savingsAccount, checkingAccount, 200));
            Assert.IsFalse(henry.InternalTransfer(savingsAccount, checkingAccount, 5000));
        }
    }
}
