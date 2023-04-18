using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace abc_bank_tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BankTest
    {
        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void Customer_Summary_OneAccount() 
        {
            // Arrange
            Customer john = new Customer("John")
                .OpenAccount(new Account(Account.CHECKING));

            Bank bank = new Bank().AddCustomer(john);

            const String expected = "Customer Summary\n - John (1 account)";

            // Act
            String actual = bank.CustomerSummary();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Customer_Summary_TwoAccounts()
        {
            // Arrange
            Customer john = new Customer("John")
                .OpenAccount(new Account(Account.CHECKING))
                .OpenAccount(new Account(Account.SAVINGS));

            Bank bank = new Bank().AddCustomer(john);

            const String expected = "Customer Summary\n - John (2 accounts)";

            // Act
            String actual = bank.CustomerSummary();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Customer_Summary_TwoCustomers()
        {
            // Arrange
            Customer john = new Customer("John")
                .OpenAccount(new Account(Account.CHECKING))
                .OpenAccount(new Account(Account.SAVINGS));
            Customer henry = new Customer("Henry")
                .OpenAccount(new Account(Account.MAXI_SAVINGS));

            Bank bank = new Bank()
                .AddCustomer(john)
                .AddCustomer(henry);

            const String expected =
                "Customer Summary\n" +
                " - John (2 accounts)\n" +
                " - Henry (1 account)";

            // Act
            String actual = bank.CustomerSummary();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Checking_Account()
        {
            // Arrange
            Account account = new Account(Account.CHECKING);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(100.0);

            Double expected = 0.1;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }
        
        [TestMethod]
        public void Savings_Account_LessThan_1000()
        {
            // Arrange
            Account account = new Account(Account.SAVINGS);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(500.0);
            
            Double expected = 0.5;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_Account_GreaterThan_1000()
        {
            // Arrange
            Account account = new Account(Account.SAVINGS);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(1500.0);

            Double expected = 2.0;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_Savings_Account_LessThan_1000()
        {
            // Arrange
            Account account = new Account(Account.MAXI_SAVINGS);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(500.0);

            Double expected = 25.0;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_Savings_Account_Between_1000_2000()
        {
            // Arrange
            Account account = new Account(Account.MAXI_SAVINGS);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(1500.0);

            Double expected = 75.0;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_Savings_Account_GreaterThan_2000()
        {
            // Arrange
            Account account = new Account(Account.MAXI_SAVINGS);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(3000.0);

            Double expected = 150.0;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_Savings_Account_RecentWithdraw()
        {
            // Arrange
            Account account = new Account(Account.MAXI_SAVINGS);

            Customer customer = new Customer("Bill")
                .OpenAccount(account);

            Bank bank = new Bank()
                .AddCustomer(customer);

            account.Deposit(3000.0);
            account.Withdraw(1000.0);

            Double expected = 2.0;

            // Act
            Double actual = bank.TotalInterestPaid();

            // Assert
            Assert.AreEqual(expected, actual, DOUBLE_DELTA);
        }

        [TestMethod]
        public void GetFirstCustomer()
        {
            // Arrange
            Bank bank = new Bank()
                .AddCustomer(new Customer("John"))
                .AddCustomer(new Customer("Bill"))
                .AddCustomer(new Customer("Oscar"));

            String expected = "John";

            // Act
            String actual = bank.GetFirstCustomer();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetFirstCustomer_NoCustomers()
        {
            // Arrange
            Bank bank = new Bank();

            String expected = "Error";

            // Act
            String actual = bank.GetFirstCustomer();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}