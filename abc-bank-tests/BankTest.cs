using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        //private static readonly double DOUBLE_DELTA = 1e-15;
        //private static readonly decimal DOUBLE_DELTA = 0M;

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
        public void AllInterest()
        {
            Bank USB = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer oscar = new Customer("Oscar").OpenAccount(savingsAccount);
            USB.AddCustomer(oscar);
            savingsAccount.Deposit(2000.0M);
            Assert.AreEqual("3.00", USB.totalInterestPaid().ToString("N"));
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0M);

            //Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
            Assert.AreEqual(0.1M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0M);

            //Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
            Assert.AreEqual(2.0M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0M);

            //Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
            Assert.AreEqual(30.0M, bank.totalInterestPaid());
        }
    }
}
