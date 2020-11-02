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
            Account checkingAccount = new Account(Account.AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0M);

            Assert.AreEqual(150.0M, bank.totalInterestPaid());
        }
    }
}
