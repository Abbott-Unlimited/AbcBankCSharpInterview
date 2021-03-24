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

            checkingAccount.Deposit(100.00m);

            Assert.AreEqual(0.10m, bank.totalInterestPaid());
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(1500.00m);

            Assert.AreEqual(2.00m, bank.totalInterestPaid());
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.00m);

            Assert.AreEqual(170.00m, bank.totalInterestPaid());
        }
    }
}
