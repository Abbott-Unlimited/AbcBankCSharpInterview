using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void GetNameForOscarIsOscar()
        {
            ICustomer oscar = new Customer("Oscar");

            Assert.AreEqual("Oscar", oscar.GetName());
        }

        [TestMethod]
        public void OpenAccountCheckingAccountAddsCheckingAccount()
        {
            ICustomer oscar = new Customer("Oscar");

            oscar.OpenAccount(new CheckingAccount());

            Assert.IsTrue(oscar.GetAccounts()[0].GetType() == typeof(CheckingAccount));
        }

        [TestMethod]
        public void OpenAccountSavingsAccountAddsSavingsAccount()
        {
            ICustomer oscar = new Customer("Oscar");

            oscar.OpenAccount(new SavingsAccount());

            Assert.IsTrue(oscar.GetAccounts()[0].GetType() == typeof(SavingsAccount));
        }

        [TestMethod]
        public void OpenAccountMaxiSavingsAccountAddsMaxiSavingsAccount()
        {
            ICustomer oscar = new Customer("Oscar");

            oscar.OpenAccount(new MaxiSavingsAccount());

            Assert.IsTrue(oscar.GetAccounts()[0].GetType() == typeof(MaxiSavingsAccount));
        }

        [TestMethod]
        public void GetNumberOfAccountsOneAccountShowsOne()
        {
            ICustomer oscar = new Customer("Oscar");

            oscar.OpenAccount(new CheckingAccount());

            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void GetNumberOfAccountsTwoAccountsShowsTwo()
        {
            ICustomer oscar = new Customer("Oscar");

            oscar.OpenAccount(new CheckingAccount());
            oscar.OpenAccount(new SavingsAccount());

            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void GetStatementShowsTransactionsAndTotals()
        {
            IAccount checkingAccount = new CheckingAccount();
            IAccount savingsAccount = new SavingsAccount();

            ICustomer henry = new Customer("Henry");
            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);

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
    }
}
