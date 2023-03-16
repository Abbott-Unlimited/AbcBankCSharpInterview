using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-3;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        //[TestMethod]
        //public void Maxi_savings_account() {
        //    Bank bank = new Bank();
        //    Account checkingAccount = new Account(Account.MAXI_SAVINGS);
        //    bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

        //    checkingAccount.Deposit(3000.0);

        //    Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        //}


        [TestMethod]
        public void Maxi_savings_interestearnedTest()
        {
            // Arrange
            var account = new abc_bank.Account(abc_bank.Account.MAXI_SAVINGS);
            var customer = new abc_bank.Customer("John");
            customer.OpenAccount(account);

            // Act
            account.Deposit(10000);
            var interestRate1 = account.InterestEarned(); // Expected: 500.00 (5% of 10000)


            // Assert
            Assert.AreEqual(10000 * (Math.Pow(1 + 0.05 / 365, 365) - 1), interestRate1, 0.001);
        }


    }
}
