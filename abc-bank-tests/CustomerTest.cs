using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            var checkingAccount = new Account(Account.Checking);
            var savingsAccount = new Account(Account.Savings);

            var henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0, DateTime.Now);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "Total $100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $3,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void DepositToAccount()
        {
            var checkingAccount = new Account(Account.Checking);
            var customer        = new Customer("Eric").OpenAccount(checkingAccount);
            checkingAccount.Deposit(500.0);

            var transactions= checkingAccount.Transactions;
            var sum             = transactions.Sum(transaction => transaction.Amount);
           
            Assert.AreEqual(sum, 500.00);
            Assert.IsTrue(customer.GetStatement().Contains("500.00"));
        }

        [TestMethod]
        public void WithdrawFromAccount()
        {
            var checkingAccount = new Account(Account.Checking);
            var customer        = new Customer("Eric").OpenAccount(checkingAccount);
            checkingAccount.Deposit(500.0);
            checkingAccount.Withdraw(100.00, DateTime.Now);

            var transactions = checkingAccount.Transactions;
            var sum          = transactions.Sum(transaction => transaction.Amount);

            Assert.AreEqual(sum, 400.00);
            Assert.IsTrue(customer.GetStatement().Contains("400.00"));
        }

        [TestMethod]
        public void TestOneAccount()
        {
            var oscar = new Customer("Oscar").OpenAccount(new Account(Account.Savings));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            var oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.Savings));
            oscar.OpenAccount(new Account(Account.Checking));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            var oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.Savings));
            oscar.OpenAccount(new Account(Account.Checking));
            oscar.OpenAccount(new Account(Account.MaxiSavings));

            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
