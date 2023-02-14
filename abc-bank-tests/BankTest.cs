using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            Account account = AccountFactory.OpenAccount(AccountTypes.CHECKING);
            john.AddAccount(account);
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = AccountFactory.OpenAccount(AccountTypes.CHECKING);
            Customer bill = new Customer("Bill");
            bill.AddAccount(checkingAccount);
            bank.AddCustomer(bill);

            var customer = bank.GetCustomer("Bill");

            var account = customer.GetAccount(AccountTypes.CHECKING);

            account.Deposit(100.0);

            Assert.AreEqual(0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = AccountFactory.OpenAccount(AccountTypes.SAVINGS);
            Customer bill = new Customer("Bill");
            bill.AddAccount(savingsAccount);
            bank.AddCustomer(bill);

            var customer = bank.GetCustomer("Bill");

            var account = customer.GetAccount(AccountTypes.SAVINGS);

            account.Deposit(1500.0);
            // savingsAccount.Deposit(1500.0);

            Assert.AreEqual(0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = AccountFactory.OpenAccount(AccountTypes.MAXI_SAVINGS);
            Customer bill = new Customer("Bill");
            bill.AddAccount(maxiSavingsAccount);
            bank.AddCustomer(bill);

            var customer = bank.GetCustomer("Bill");

            var account = customer.GetAccount(AccountTypes.MAXI_SAVINGS);

            account.Deposit(3000.0);
            var interestPaid = bank.totalInterestPaid();

            Assert.AreEqual(0, interestPaid, DOUBLE_DELTA);
        }
    }
}
