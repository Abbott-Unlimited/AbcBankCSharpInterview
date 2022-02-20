using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.CustomerTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Account savingsAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

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
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Savings));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Savings));
            oscar.OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Checking));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Savings))
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Checking))
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTransferOfAmountFromAccountAToAccountB()
        {
            Account checkingAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Account savingsAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);

            checkingAccount.Deposit(200.0);
            savingsAccount.Deposit(500.0);

            savingsAccount.Transfer(200.0, checkingAccount);

            double savingsSum = savingsAccount.SumTransactions();
            double checkingSum = checkingAccount.SumTransactions();

            Assert.IsTrue(savingsSum == 300.00 && checkingSum == 400.00);

        }
    }
}
