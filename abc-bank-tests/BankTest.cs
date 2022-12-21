﻿using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double INTEREST_DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void Check_That_Customer_Summary_Is_Correct()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void Check_That_CheckingAccount_Interest_Paid_Is_Within_Acceptable_Delta()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.TotalInterestPaid(), INTEREST_DOUBLE_DELTA);
        }

        [TestMethod]
        public void Check_That_SavingsAccount_Interest_Paid_Is_Within_Acceptable_Delta()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.TotalInterestPaid(), INTEREST_DOUBLE_DELTA);
        }

        [TestMethod]
        public void Check_That_MAXI_SavingsAccount_Interest_Paid_Is_Within_Acceptable_Delta()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.TotalInterestPaid(), INTEREST_DOUBLE_DELTA);
        }
    }
}
