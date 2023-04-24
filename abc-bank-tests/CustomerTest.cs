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
            // Arrange
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingsAccount();

            Customer henry = new Customer("Henry")
                .OpenAccount(checkingAccount)
                .OpenAccount(savingsAccount);

            checkingAccount.RequestDeposit(100.0m);
            savingsAccount.RequestDeposit(4000.0m);
            savingsAccount.RequestWithdrawal(200.0m);

            string expected = "Statement for Henry\n" +
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
                "Total In All Accounts $3,900.00";

            // Act
            string actual = henry.GetStatementForAllAcounts();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestZeroAccounts()
        {
            // Arrange
            Customer oscar = new Customer("Oscar");
            int expected = 0;

            // Act
            int actual = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            // Arrange
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new SavingsAccount())
                 .OpenAccount(new CheckingAccount());
            int expected = 2;

            // Act
            int actual = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
