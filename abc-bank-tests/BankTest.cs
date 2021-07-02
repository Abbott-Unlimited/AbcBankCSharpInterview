using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        //private static readonly decimal DECIMAL_DELTA = 1e-15m;

        [TestMethod]
        public void CustomerSummaryOneAccount() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }
        [TestMethod]
        public void CustomerSummaryManyAccount()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            john.OpenAccount(new Account(Account.CHECKING));
            john.OpenAccount(new Account(Account.CHECKING));
            john.OpenAccount(new Account(Account.CHECKING));
            john.OpenAccount(new Account(Account.CHECKING));
            john.OpenAccount(new Account(Account.SAVINGS));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (6 accounts)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(10000);
            checkingAccount.AddDailyInterest();

            Assert.AreEqual(0.0274m, Decimal.Round(bank.totalInterestPaid(), 4));
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(10000);
            savingsAccount.AddDailyInterest();

            Assert.AreEqual(0.0521m, Decimal.Round(bank.totalInterestPaid(), 4));
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(10000);
            savingsAccount.AddDailyInterest();

            Assert.AreEqual(1.3699m, Decimal.Round(bank.totalInterestPaid(), 4));
        }
    }
}
