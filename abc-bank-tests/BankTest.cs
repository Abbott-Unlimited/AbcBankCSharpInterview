using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.AccountType.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account account = new Account(Account.AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(account);
            bank.AddCustomer(bill);

            account.Deposit(100);

            Assert.AreEqual(0.1M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void SavingsAccount() {
            Bank bank = new Bank();
            Account account = new Account(Account.AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(account));

            account.Deposit(1500);

            Assert.AreEqual(2, bank.totalInterestPaid());
        }

        [TestMethod]
        public void MaxiSavingsAccount() {
            Bank bank = new Bank();
            Account account = new Account(Account.AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(account));

            account.Deposit(3000);

            Assert.AreEqual(170, bank.totalInterestPaid());
        }


        [TestMethod]
        public void InterestPaid()
        {
            Bank bank = new Bank();

            Customer bill = new Customer("Bill");

            bill.OpenAccount(new Account(Account.AccountType.CHECKING).Deposit(100));
            bill.OpenAccount(new Account(Account.AccountType.SAVINGS).Deposit(1500));
            bill.OpenAccount(new Account(Account.AccountType.MAXI_SAVINGS).Deposit(3000));
            bank.AddCustomer(bill);

            Customer john = new Customer("John");

            john.OpenAccount(new Account(Account.AccountType.CHECKING));
            bank.AddCustomer(john);


            Assert.AreEqual(bank.totalInterestPaid(), .1M + 2 + 170);
         }
    }
}
