using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {
        [TestMethod]
        public void CustomerSummary() {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.AccountType.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0M);

            Assert.AreEqual(0.1M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void SavingsAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.0M);

            Assert.AreEqual(2.0M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void MaxiSavingsAccount() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0M);

            Assert.AreEqual(150.0M, bank.totalInterestPaid());
        }

        [TestMethod]
        public void MixedAccounts() {
            Bank bank = new Bank();
            Customer bill = new Customer("Bill");
            Customer pete = new Customer("Pete");

            bill.OpenAccount(new Account(Account.AccountType.CHECKING));
            bill.OpenAccount(new Account(Account.AccountType.MAXI_SAVINGS));

            bill.MakeDeposit(0, 100);
            bill.MakeDeposit(1, 100);
            bank.AddCustomer(bill);

            pete.OpenAccount(new Account(Account.AccountType.CHECKING));
            pete.OpenAccount(new Account(Account.AccountType.SAVINGS));

            pete.MakeDeposit(0, 100);
            pete.MakeDeposit(1, 1000);
            bank.AddCustomer(pete);            
            
            Assert.AreEqual(6.2M, bank.totalInterestPaid());
        }

    }
}
