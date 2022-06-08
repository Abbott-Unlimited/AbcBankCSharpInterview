using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        // This DOUBLE_DELTA is unnecessary, decimals are precise enough

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

            checkingAccount.Deposit(100.0M);

            Assert.AreEqual(0.1M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0M);

            Assert.AreEqual(2.0M, bank.totalInterestPaid());
        }

        // Changing how Maxi-Savings accounts work changes the expected results from 170
        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));
            bank.AddCustomer(new Customer("Logan").OpenAccount(maxiAccount));

            checkingAccount.Deposit(3000.0M);
            maxiAccount.Deposit(1500.0M);
            maxiAccount.Withdraw(500.0M);

            // Expecting 150 from Bill's account and 1 from Logan's due to recent withdrawal
            Assert.AreEqual(151.0M, bank.totalInterestPaid());
        }
    }
}
