using abc_bank;
using abc_bank.Services.BankServices;
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
    public class GetBankInterestPaidServiceTest
    {
        private GetBankInterestPaidService getBankInterestPaidService;
        private AddCustomerTransactionService addCustomerTransactionService;

        [TestInitialize]
        public void Initialize()
        {
            this.getBankInterestPaidService = new GetBankInterestPaidService();
            this.addCustomerTransactionService = new AddCustomerTransactionService();
        }

        [TestMethod]
        public void Get_StubABankWithNoCustomers_VerifyResults()
        {
            // Arrange
            var bank = new Bank();

            // Act
            var interestPaid = this.getBankInterestPaidService.Get(bank);

            // Assert
            Assert.AreEqual(0, interestPaid);
        }

        [TestMethod]
        public void Get_StubABankWithOneCustomerAndOneCheckingAccount_VerifyResults()
        {
            // Arrange
            var bank = new Bank();

            var customer = new Customer("John Doe");
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            customer.OpenAccount(checkingAccount);
            this.addCustomerTransactionService.Add(customer, "John Doe Checking Account", 100, TransactionType.DEPOSIT);

            bank.AddCustomer(customer);

            // Act
            var interestPaid = this.getBankInterestPaidService.Get(bank);

            // Assert
            Assert.AreEqual(0.1m, interestPaid);
        }

        [TestMethod]
        public void Get_StubABankWithMultipleCustomersAndMultipleAccounts_VerifyResults()
        {
            // Arrange
            var bank = new Bank();

            var customer1 = new Customer("John Doe");

            var customer1CheckingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            customer1.OpenAccount(customer1CheckingAccount);
            this.addCustomerTransactionService.Add(customer1, "John Doe Checking Account", 100, TransactionType.DEPOSIT);

            var customer2 = new Customer("John Doe");

            var customer2checkingAccount = new Account(AccountType.SAVINGS, "Jane Doe Savings Account");
            customer2.OpenAccount(customer2checkingAccount);
            this.addCustomerTransactionService.Add(customer2, "Jane Doe Savings Account", 500, TransactionType.DEPOSIT);

            var customer3 = new Customer("Jack Doe");

            var customer3checkingAccount = new Account(AccountType.MAXI_SAVINGS, "Jack Doe Maxi Savings Account");
            customer3.OpenAccount(customer3checkingAccount);
            this.addCustomerTransactionService.Add(customer3, "Jack Doe Maxi Savings Account", 1000, TransactionType.DEPOSIT);

            bank.AddCustomer(customer1);
            bank.AddCustomer(customer2);
            bank.AddCustomer(customer3);

            // Act
            var interestPaid = this.getBankInterestPaidService.Get(bank);

            // Assert
            Assert.AreEqual(50.6m, interestPaid);
        }
    }
}
