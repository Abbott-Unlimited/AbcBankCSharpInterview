using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        private const double DoubleDelta = 1e-15;

        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.Checking));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        // Modify tests with yearly rates.
        [TestMethod]
        public void CheckingAccount()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.Checking);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0, DateTime.Now);
            checkingAccount.SetAccountAge(100);

            Assert.AreEqual(0.27, bank.TotalInterestPaid(), DoubleDelta);
        }

        [TestMethod]
        public void MaxiSavings()
        {
            var bank = new Bank();
            var maxiAccount = new Account(AccountType.MaxiSavings);
            var bill = new Customer("Bill").OpenAccount(maxiAccount);
            bank.AddCustomer(bill);
            // Account will now always be 10 days old for testing. 
            // I'm making the assumption that interest will accrue all the time no matter what.
            // This would be a business logic question for the client about how we should handle exact times.

            maxiAccount.SetAccountAge(10);
            maxiAccount.Deposit(100, DateTime.Now);
            Assert.AreEqual(0.14, bank.TotalInterestPaid(), DoubleDelta);
        }

        [TestMethod]
        public void Savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.SetAccountAge(75);
            checkingAccount.Deposit(1500.0, DateTime.Now);

            Assert.AreEqual(4.0875, bank.TotalInterestPaid(), DoubleDelta);
        }

        /* Test in AccountTest class supersedes this test as it now tracks accrual per year.
        [TestMethod]
        public void Maxi_savings_account()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.MaxiSavings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0, DateTime.Now);
            // Not correct, not taking into account the 0.05 interest rate for not withdrawals 10 days.
            Assert.AreEqual(150.0, bank.totalInterestPaid(), DoubleDelta);
        }
        */
    }
}