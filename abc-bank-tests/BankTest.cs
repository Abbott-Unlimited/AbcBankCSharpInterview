using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-5;

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

            Assert.AreEqual(0.00027, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account_greater1000() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0, DateTime.Now.AddDays(-365));

            Assert.AreEqual(2.00547, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account_less1000()
        {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

            savingsAccount.Deposit(900, DateTime.Now.AddDays(-365));

            Assert.AreEqual(.90246, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account_recent_withdraw() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Withdraw(200.0);

            Assert.AreEqual(0.00767, bank.totalInterestPaid(), DOUBLE_DELTA);

        }

        [TestMethod]
        public void Maxi_savings_account_no_recent_withdraw()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));
            DateTime? depositDate = DateTime.Now.AddDays(-20);
            maxiSavingsAccount.Deposit(1500, depositDate);
            DateTime? withdrawalDate = DateTime.Now.AddDays(-15);
            maxiSavingsAccount.Withdraw(200, withdrawalDate);
            //Interest should be 1500 *  6 days + 1300 * 15 days

            Assert.AreEqual(1.956986, bank.totalInterestPaid(), DOUBLE_DELTA);

        }



    }
}
