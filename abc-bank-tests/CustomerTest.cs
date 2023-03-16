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
        public void TestTransfer()
        {
            Account sourceAccount = new Account(Account.CHECKING);
            Account destinationAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(sourceAccount).OpenAccount(destinationAccount);

            // Deposit funds into the source account
            sourceAccount.Deposit(1000);

            // Transfer funds from the source account to the destination account
            sourceAccount.Transfer(destinationAccount, 500);

            // Check that the correct amounts are in each account
            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $1,000.00\n" +
                    "  withdrawal $500.00\n" +
                    "Total $500.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $500.00\n" +
                    "Total $500.00\n" +
                    "\n" +
                    "Total In All Accounts $1,000.00", henry.GetStatement());
        }


        [TestMethod]
        public void Transfer_DifferentCustomers_ThrowsArgumentException()
        {
            Account sourceAccount = new Account(Account.CHECKING);
            Account destinationAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(sourceAccount);
            Customer john = new Customer("John").OpenAccount(destinationAccount);
            // Deposit funds into the source account
            sourceAccount.Deposit(1000);

            // Transfer funds from the source account to the destination account
           
            try
            {
                sourceAccount.Transfer(destinationAccount, 500);
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Destination account must belong to the same customer as this account.", ex.Message);
            }
        }


}
}
