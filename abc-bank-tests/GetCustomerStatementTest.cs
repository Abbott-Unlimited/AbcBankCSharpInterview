using abc_bank;
using abc_bank.Services.CustomerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank_tests
{
    [TestClass]
    public class GetCustomerStatementTest
    {
        private GetCustomerStatementService getCustomerStatementService;
        private AddCustomerTransactionService addCustomerTransactionService;

        [TestInitialize]
        public void Initialize()
        {
            this.getCustomerStatementService = new GetCustomerStatementService();
            this.addCustomerTransactionService = new AddCustomerTransactionService();
        }

        [TestMethod]
        public void Get_StubACustomerWithNoAccounts_VerifyResults()
        {
            // Arrange
            var customer = new Customer("John Doe");

            // Act
            var statement = this.getCustomerStatementService.Get(customer);

            // Assert
            Assert.AreEqual("Customer John Doe has no accounts.", statement);
        }

        [TestMethod]
        public void Get_StubACustomerWithOneCheckingAccountAndNoTransactions_VerifyResults()
        {
            // Arrange
            var customer = new Customer("John Doe");
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            customer.OpenAccount(checkingAccount);

            var expectedStatement =
                "Statement for John Doe\n" +
                "Checking Account\n" +
                "Total $0.00" +
                "\nTotal In All Accounts $0.00";

            // Act
            var statement = this.getCustomerStatementService.Get(customer);

            // Assert
            Assert.AreEqual(expectedStatement, statement);
        }

        [TestMethod]
        public void Get_StubACustomerWithOneCheckingAccountAndOneTransaction_VerifyResults()
        {
            // Arrange
            var customer = new Customer("John Doe");
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            customer.OpenAccount(checkingAccount);

            this.addCustomerTransactionService.Add(customer, "John Doe Checking Account", 5.25m, TransactionType.DEPOSIT);

            var expectedStatement =
                "Statement for John Doe\n" +
                "Checking Account\n" +
                "   deposit $5.25\n" + 
                "Total $5.25" +
                "\nTotal In All Accounts $5.25";

            // Act
            var statement = this.getCustomerStatementService.Get(customer);

            // Assert
            Assert.AreEqual(expectedStatement, statement);
        }

        [TestMethod]
        public void Get_StubACustomerWithMultipleAccountsAndMultipleTransactions_VerifyResults()
        {
            // Arrange
            var customer = new Customer("John Doe");
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            customer.OpenAccount(checkingAccount);

            this.addCustomerTransactionService.Add(customer, "John Doe Checking Account", 5.25m, TransactionType.DEPOSIT);
            this.addCustomerTransactionService.Add(customer, "John Doe Checking Account", -10m, TransactionType.WITHDRAWAL);

            var savingsAccount = new Account(AccountType.SAVINGS, "John Doe Savings Account");
            customer.OpenAccount(savingsAccount);

            this.addCustomerTransactionService.Add(customer, "John Doe Savings Account", 20m, TransactionType.DEPOSIT);

            var maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, "John Doe Maxi Savings Account");
            customer.OpenAccount(maxiSavingsAccount);

            this.addCustomerTransactionService.Add(customer, "John Doe Maxi Savings Account", 15.25m, TransactionType.DEPOSIT);

            var expectedStatement =
                "Statement for John Doe\n" +
                "Checking Account\n" +
                "   deposit $5.25\n" +
                "   withdrawal ($10.00)\n" +
                "Total ($4.75)" +
                "Savings Account\n" +
                "   deposit $20.00\n" +
                "Total $20.00" +
                "Maxi Savings Account\n" +
                "   deposit $15.25\n" +
                "Total $15.25" +
                "\nTotal In All Accounts $30.50";

            // Act
            var statement = this.getCustomerStatementService.Get(customer);

            // Assert
            Assert.AreEqual(expectedStatement, statement);
        }
    }
}
