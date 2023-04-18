using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace abc_bank_tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void GetStatement_TwoAccounts()
        {
            // Arrange
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry")
                .OpenAccount(checkingAccount)
                .OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            const String expected =
                "Statement for Henry\n" +
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
            String actual = henry.GetStatement();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStatement_ThreeAccounts()
        {
            // Arrange
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);

            Customer henry = new Customer("John")
                .OpenAccount(checkingAccount)
                .OpenAccount(savingsAccount)
                .OpenAccount(maxiSavingsAccount);

            checkingAccount.Deposit(300.0);
            checkingAccount.Withdraw(50.0);
            savingsAccount.Deposit(2000.0);
            savingsAccount.Withdraw(500.0);
            maxiSavingsAccount.Deposit(7000.0);
            maxiSavingsAccount.Withdraw(2500.0);

            const String expected =
                "Statement for John\n" +
                "\n" +
                "Checking Account\n" +
                "  deposit $300.00\n" +
                "  withdrawal $50.00\n" +
                "Total $250.00\n" +
                "\n" +
                "Savings Account\n" +
                "  deposit $2,000.00\n" +
                "  withdrawal $500.00\n" +
                "Total $1,500.00\n" +
                "\n" +
                "Maxi Savings Account\n" +
                "  deposit $7,000.00\n" +
                "  withdrawal $2,500.00\n" +
                "Total $4,500.00\n" +
                "\n" +
                "Total In All Accounts $6,250.00";

            // Act
            String actual = henry.GetStatement();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOneAccount()
        {
            // Arrange
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));

            const int expected = 1;

            // Act
            int actual = oscar.GetNumberOfAccounts();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestTwoAccounts()
        {
            // Arrange
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.SAVINGS))
                 .OpenAccount(new Account(Account.CHECKING));

            const int expected = 2;

            // Act
            int actual = oscar.GetNumberOfAccounts();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            // Arrange
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS))
                    .OpenAccount(new Account(Account.CHECKING))
                    .OpenAccount(new Account(Account.MAXI_SAVINGS));

            const int expected = 3;

            // Act
            int actual = oscar.GetNumberOfAccounts();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}