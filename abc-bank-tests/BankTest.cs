using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using System.Diagnostics;

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
            Assert.AreEqual(0, bank.totalInterestPaid(), DOUBLE_DELTA);

            checkingAccount.transactions.Clear();
            DateTime currentTime = DateProvider.getInstance().Now();
            DateTime lastYear = new DateTime(currentTime.Year - 1, currentTime.Month, currentTime.Day);
            checkingAccount.transactions.Add(new Transaction(100.00, lastYear));
            //Debug.WriteLine(lastYear.ToShortDateString());

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.transactions.Clear();
            DateTime currentTime = DateProvider.getInstance().Now();
            DateTime lastYear = currentTime.AddDays(-365);
            checkingAccount.transactions.Add(new Transaction(1500.0, lastYear));

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.transactions.Clear();
            DateTime currentTime = DateProvider.getInstance().Now();
            DateTime lastYear = currentTime.AddDays(-182);
            checkingAccount.transactions.Add(new Transaction(3000.0, lastYear));

            Assert.AreEqual(1.5, bank.totalInterestPaid(), DOUBLE_DELTA);

            checkingAccount.transactions.Clear();
            lastYear = new DateTime(currentTime.Year - 1, currentTime.Month, currentTime.Day);
            checkingAccount.transactions.Add(new Transaction(3000.0, lastYear));

            Assert.AreEqual(3, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
