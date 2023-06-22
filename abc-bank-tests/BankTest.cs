using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            //Arrange
            Bank bank = new Bank();
            Customer john = new Customer("John");
            Account checkingAccount = new Account(Account.AccountType.Checking);
            john.OpenAccount(checkingAccount);
            bank.AddCustomer(john);

            //Act
            string summary = bank.CustomerSummary();

            //Assert
            Assert.AreEqual("Customer Summary\n - John (1 account)", summary);
        }

        [TestMethod]
        public void CheckingAccount() {
            //Arrange
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.AccountType.Checking);
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            bill.OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.0);

            //Act
            double interest = bank.totalInterestPaid();

            //Assert
            Assert.AreEqual(0.1 / 365, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            //Arrange
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.AccountType.Savings);
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            bill.OpenAccount(savingsAccount);

            savingsAccount.Deposit(1500.0);

            //Act
            var interest = bank.totalInterestPaid();

            //Assert
            Assert.AreEqual(2.0/365, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            //Arrange
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.AccountType.MaxiSavings);
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            bill.OpenAccount(maxiSavingsAccount);

            maxiSavingsAccount.Deposit(3000.0);

            //Act
            double interest = bank.totalInterestPaid();

            //Assert
            Assert.AreEqual(150.0 / 365, interest, DOUBLE_DELTA);
        }
    }
}
