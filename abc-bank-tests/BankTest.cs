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
            john.OpenAccount(AccountType.Checking);
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }
        [TestMethod]
        public void CustomerSummaryMultipleAccounts()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(AccountType.Checking);
            john.OpenAccount(AccountType.Checking);

            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (2 accounts)", bank.CustomerSummary());
        }

        [TestMethod]
        public void TestChecking() {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            Account checkingAccount = bill.OpenAccount(AccountType.Checking);

            checkingAccount.Deposit(100.0);

            DateProvider.AdjustDateByDays(1);

            Assert.AreEqual(0.1, bank.GetTotalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestSavings() {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            Account savingsAccount = bill.OpenAccount(AccountType.Savings);

            savingsAccount.Deposit(1500.0);

            DateProvider.AdjustDateByDays(1);

            var interest = bank.GetTotalInterestPaid();

            Assert.AreEqual(2.0, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        [Ignore]
        public void TestMaxiSavingsOld() {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            Account maxiAccount = bill.OpenAccount(AccountType.Maxi);

            maxiAccount.Deposit(3000.0);

            DateProvider.AdjustDateByDays(1);

            var interest = bank.GetTotalInterestPaid();

            Assert.AreEqual(170.0, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestMaxiSavingsWithNoRecentWithdrawal()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            Account maxiAccount = bill.OpenAccount(AccountType.Maxi);

            maxiAccount.Deposit(100.00);

            DateProvider.AdjustDateByDays(12);

            var interest = bank.GetTotalInterestPaid();

            Assert.AreEqual(60, interest, DOUBLE_DELTA);
        }
        [TestMethod]
        public void TestMaxiSavingsWithRecentWithdrawal()
        {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            bank.AddCustomer(bill);
            Account maxiAccount = bill.OpenAccount(AccountType.Maxi);

            maxiAccount.Deposit(101.00);
            maxiAccount.Withdraw(1.00);

            DateProvider.AdjustDateByDays(12);

            var interest = bank.GetTotalInterestPaid();

            Assert.AreEqual(6.1, interest, DOUBLE_DELTA);
        }
    }
}
