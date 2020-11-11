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
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid()); //Checking accounts have a flat rate of 0.1%.
        }

        [TestMethod]
        public void SavingsAccount() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);
            Customer bill = new Customer("Bill").OpenAccount(savingsAccount);
            bank.AddCustomer(bill);

            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid()); //first $1000 have a flat rate of 0.1% which is 1$ then 0.2% which adds up to 2$.
        }

        [TestMethod]
        public void MaxiSavingsAccount() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.AccountType.MAXI_SAVINGS);
            Customer bill = new Customer("Bill").OpenAccount(maxiSavingsAccount);
            bank.AddCustomer(bill);

            maxiSavingsAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid()); //Maxi-Savings accounts have a rate of 2% for the first $1,000 which is 20$ then 50$ for the next $1000 and then $100 for the next $1000.
        }

        [TestMethod]
        public void GetFirstCustomerName()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.AccountType.CHECKING));
            bank.AddCustomer(john);

            Customer bill = new Customer("Bill");
            bill.OpenAccount(new Account(Account.AccountType.SAVINGS));
            bank.AddCustomer(bill);

            Assert.AreEqual("John", bank.GetFirstCustomerName());
        }
    }
}
