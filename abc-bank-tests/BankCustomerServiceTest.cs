using abc_bank;
using abc_bank.Services.BankServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank_tests
{
    [TestClass]
    public class BankCustomerServiceTest
    {
        private BankCustomerService bankCustomerService;

        [TestInitialize]
        public void Initialize()
        {
            this.bankCustomerService = new BankCustomerService();
        }

        [TestMethod]
        public void GetCustomerSummary_StubABankAndNoCustomers_VerifyResutls()
        {
            // Arrange
            var bank = new Bank();

            // Act
            var customerSummary = this.bankCustomerService.GetCustomerSummary(bank);

            // Assert
            Assert.AreEqual("This bank has no customers.", customerSummary);
        }

        [TestMethod]
        public void GetCustomerSummary_StubABankAndOneCustomerWithNoAccounts_VerifyResutls()
        {
            // Arrange
            var bank = new Bank();
            var customer = new Customer("John Doe");

            bank.AddCustomer(customer);

            // Act
            var customerSummary = this.bankCustomerService.GetCustomerSummary(bank);

            // Assert
            Assert.AreEqual("Customer Summary\n - John Doe (0 accounts)", customerSummary);
        }

        [TestMethod]
        public void GetCustomerSummary_StubABankAndOneCustomerWithOneAccounts_VerifyResutls()
        {
            // Arrange
            var bank = new Bank();
            var customer = new Customer("John Doe");

            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            customer.OpenAccount(checkingAccount);

            bank.AddCustomer(customer);

            // Act
            var customerSummary = this.bankCustomerService.GetCustomerSummary(bank);

            // Assert
            Assert.AreEqual("Customer Summary\n - John Doe (1 account)", customerSummary);
        }

        [TestMethod]
        public void GetCustomerSummary_StubABankAndTwoCustomersWithTwoAccounts_VerifyResutls()
        {
            // Arrange
            var bank = new Bank();

            var customer1 = new Customer("John Doe");

            var customer1CheckingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            var customer1CavingsAccount = new Account(AccountType.SAVINGS, "John Doe Savings Account");

            customer1.OpenAccount(customer1CheckingAccount);
            customer1.OpenAccount(customer1CavingsAccount);

            var customer2 = new Customer("Jane Doe");

            var customer2CheckingAccount = new Account(AccountType.CHECKING, "Jane Doe Checking Account");
            var customer2SavingsAccount = new Account(AccountType.SAVINGS, "Jane Doe Savings Account");

            customer2.OpenAccount(customer2CheckingAccount);
            customer2.OpenAccount(customer2SavingsAccount);

            bank.AddCustomer(customer1);
            bank.AddCustomer(customer2);

            // Act
            var customerSummary = this.bankCustomerService.GetCustomerSummary(bank);

            // Assert
            Assert.AreEqual("Customer Summary\n - John Doe (2 accounts)\n - Jane Doe (2 accounts)", customerSummary);
        }

        [TestMethod]
        public void GetFirstCustomer_StubNoCustomers_VerifyResults()
        {
            // Arrange
            var bank = new Bank();

            // Act
            var firstCustomer = this.bankCustomerService.GetFirstCustomer(bank);

            // Assert
            Assert.AreEqual("There are no customers at this bank.", firstCustomer);
        }

        [TestMethod]
        public void GetFirstCustomer_StubThreeCustomers_VerifyResults()
        {
            // Arrange
            var bank = new Bank();

            var customerJohnDoe = new Customer("John Doe", DateTime.Now);
            var customerJaneDoe = new Customer("Jane Doe", DateTime.Now.AddDays(-30));
            var customerJackDoe = new Customer("Jack Doe", DateTime.Now.AddDays(-60));

            bank.AddCustomer(customerJohnDoe);
            bank.AddCustomer(customerJaneDoe);
            bank.AddCustomer(customerJackDoe);

            // Act
            var firstCustomer = this.bankCustomerService.GetFirstCustomer(bank);

            // Assert
            Assert.AreEqual("Jack Doe", firstCustomer);
        }
    }
}
