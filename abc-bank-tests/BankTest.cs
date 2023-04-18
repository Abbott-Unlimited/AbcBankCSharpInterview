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
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(3, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

            maxiAccount.Deposit(3000.0);

            Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void CheckingDailyInterestAccrual()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            /* timeline
             * Day 1 - 100.1
             * Day 2 - 150.2501
             * Day 3 - 150.4003501
             * Day 4 - 170.5707504501
             * Day 5 - 170.7413212005501
             * Day 6 - 180.9220625217506501
             */

            checkingAccount.Deposit(100.0);
            checkingAccount.Deposit(50.0, DateTime.Now.AddDays(1));
            checkingAccount.Deposit(20.0, DateTime.Now.AddDays(3));
            checkingAccount.Deposit(10.0, DateTime.Now.AddDays(5));

            Assert.AreEqual(0.92, Math.Round(bank.totalInterestPaid(), 2), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsDailyInterestAccrual()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer bill = new Customer("Bill").OpenAccount(savingsAccount);
            bank.AddCustomer(bill);

            savingsAccount.Deposit(100.0);
            savingsAccount.Deposit(50.0, DateTime.Now.AddDays(1));
            savingsAccount.Deposit(20.0, DateTime.Now.AddDays(3));
            savingsAccount.Deposit(10.0, DateTime.Now.AddDays(5));

            Assert.AreEqual(0.92, Math.Round(bank.totalInterestPaid(), 2), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsDailyInterestAccrual()
        {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            Customer bill = new Customer("Bill").OpenAccount(maxiAccount);
            bank.AddCustomer(bill);

            /* timeline
             * Day 1 - 105
             * Day 2 - 162.75
             * Day 3 - 170.8875
             * Day 4 - 200.431875
             * Day 5 - 210.45346875
             * Day 6 - 231.4761421875
             */

            maxiAccount.Deposit(100.0);
            maxiAccount.Deposit(50.0, DateTime.Now.AddDays(1));
            maxiAccount.Deposit(20.0, DateTime.Now.AddDays(3));
            maxiAccount.Deposit(10.0, DateTime.Now.AddDays(5));

            Assert.AreEqual(51.48, Math.Round(bank.totalInterestPaid(), 2), DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsPrePost1000()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer bill = new Customer("Bill").OpenAccount(savingsAccount);
            bank.AddCustomer(bill);

            /* timeline
             * Day 1 - 500.5
             * Day 2 - 751.2505
             * Day 3 - 1,103.453001
             * Day 4 - 1,205.859907002
             */

            savingsAccount.Deposit(500.0);
            savingsAccount.Deposit(250.0, DateTime.Now.AddDays(1));
            savingsAccount.Deposit(350.0, DateTime.Now.AddDays(2));
            savingsAccount.Deposit(100.0, DateTime.Now.AddDays(3));

            Assert.AreEqual(5.86, Math.Round(bank.totalInterestPaid(), 2), DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsWithdrawalWithin10Days()
        {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            Customer bill = new Customer("Bill").OpenAccount(maxiAccount);
            bank.AddCustomer(bill);

            /* timeline
             * Day 1 - 525
             * Day 2 - 813.75
             * Day 3 - 714.46375
             * Day 4 - 765.22821375
             */

            maxiAccount.Deposit(500.0);
            maxiAccount.Deposit(250.0, DateTime.Now.AddDays(1));
            maxiAccount.Withdraw(100.0, DateTime.Now.AddDays(2));
            maxiAccount.Deposit(50.0, DateTime.Now.AddDays(3));

            Assert.AreEqual(65.23, Math.Round(bank.totalInterestPaid(), 2), DOUBLE_DELTA);
        }
    }
}
