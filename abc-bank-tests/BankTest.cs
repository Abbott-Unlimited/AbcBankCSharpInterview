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
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestDailyInterest()
        {
            Bank bank = new Bank();
            Account checking = new Account(Account.CHECKING);
            Account savings = new Account(Account.SAVINGS);
            Account maxi = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checking).OpenAccount(savings).OpenAccount(maxi));

            checking.Deposit(1000);
            savings.Deposit(2500);
            maxi.Deposit(500);

            // Get the total interest earned by each account after 90 days.
            double checkingInterestGained = Math.Round(checking.DailyInterest() * 90, 2);
            Assert.AreEqual(0.25, checkingInterestGained);
            double savingsInterestGained = Math.Round(savings.DailyInterest() * 90, 2);
            Assert.AreEqual(0.99, savingsInterestGained);
            double maxiInterestGained = Math.Round(maxi.DailyInterest() * 90, 2);
            Assert.AreEqual(6.16, maxiInterestGained);
        }
    }
}
