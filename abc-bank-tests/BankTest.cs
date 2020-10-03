using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void SingleCustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void MultipleCustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(AccountType.CHECKING));
            bank.AddCustomer(john);
            Customer mark = new Customer("Mark");
            mark.OpenAccount(new Account(AccountType.SAVINGS));
            bank.AddCustomer(mark);
            Customer sally = new Customer("Sally");
            sally.OpenAccount(new Account(AccountType.MAXI_SAVINGS));
            bank.AddCustomer(sally);
            Customer jessie = new Customer("Jessie");
            jessie.OpenAccount(new Account(AccountType.CHECKING))
                .OpenAccount(new Account(AccountType.SAVINGS))
                .OpenAccount(new Account(AccountType.MAXI_SAVINGS));
            bank.AddCustomer(jessie);

            var expectedString = "Customer Summary\n" +
                " - John (1 account)\n" +
                " - Mark (1 account)\n" +
                " - Sally (1 account)\n" +
                " - Jessie (3 accounts)";

            Assert.AreEqual(expectedString, bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));
            savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account maxiSavings = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavings));

            maxiSavings.Deposit(10000.0);

            Assert.AreEqual(10.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account_negative()
        {
            Bank bank = new Bank();
            Account maxiSavings = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavings));

            maxiSavings.Withdraw(10000.0);

            Assert.AreEqual(0.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account_old()
        {
            Bank bank = new Bank();
            Account maxiSavings = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavings));

            maxiSavings.Deposit(10000.0);

            DateProvider.setCustomDate(DateTime.Now.AddDays(11));
            var interest = bank.totalInterestPaid();
            DateProvider.setCustomDate(null);

            Assert.AreEqual(500.0, interest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void MultipleAccounts()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountType.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Account savingsAccount = new Account(AccountType.SAVINGS);
            bank.AddCustomer(new Customer("Joe").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0);

            Account maxiSavings = new Account(AccountType.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Jane").OpenAccount(maxiSavings));

            maxiSavings.Deposit(10000.0);

            Assert.AreEqual(12.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

    }
}
