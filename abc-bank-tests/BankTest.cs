using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;
        private static String BILL = "Bill";
        private static String JOHN= "John";
        private static double TOTALINTERESTPAID_01 = 0.1;
        private static double TOTALINTERESTPAID_2 = 2.0;
        private static double TOTALINTERESTPAID_170 = 170.0;
        private static double DEPOSIT_100 = 100.0;
        private static double DEPOSIT_1500 = 1500.0;
        private static double DEPOSIT_3000 = 3000.0;
        private static String CUSTOMER_SUMMARY_JOHN = "Customer Summary\n - John (1 account)";

        [TestMethod]
        public void TestCustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer(JOHN);
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual(CUSTOMER_SUMMARY_JOHN, bank.CustomerSummary());
        }

        [TestMethod]
        public void TestCheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer(BILL).OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(DEPOSIT_100);

            Assert.AreEqual(TOTALINTERESTPAID_01, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestSavingsAccount() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer(BILL).OpenAccount(savingsAccount));

            savingsAccount.Deposit(DEPOSIT_1500);

            Assert.AreEqual(TOTALINTERESTPAID_2, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestMaxiSavingsAccount() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer(BILL).OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(DEPOSIT_3000);

            Assert.AreEqual(TOTALINTERESTPAID_170, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
