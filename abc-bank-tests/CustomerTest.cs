using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp() {
            Account checkingAccount = new Account(Account.AccountType.CHECKING);
            Account savingsAccount = new Account(Account.AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);
            savingsAccount.Withdraw(200.0M);

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
        public void TestOneAccount() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.OpenAccount(new Account(Account.AccountType.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.OpenAccount(new Account(Account.AccountType.CHECKING));
            oscar.OpenAccount(new Account(Account.AccountType.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void MakeDeposit() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.MakeDeposit(0, 50);

            Assert.AreEqual(50M, oscar.getAccountBalance(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void MakeDepositBadAmount() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.MakeDeposit(0, -50M);

            Assert.AreEqual(50M, oscar.getAccountBalance(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Account Number Invalid")]
        public void MakeDepositBadAccount() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.MakeDeposit(1, 50);
        }

        [TestMethod]
        public void MakeWithdrawl() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.MakeWithdrawl(0, 50);

            Assert.AreEqual(-50M, oscar.getAccountBalance(0));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void MakeWithdrawlBadAmount() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.MakeDeposit(0, -5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Account Number Invalid")]
        public void MakeWithdrawlBadAccount() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.MakeWithdrawl(5, 100M);
        }

        [TestMethod]
        public void TransferFunds() {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountType.SAVINGS));
            oscar.OpenAccount(new Account(Account.AccountType.CHECKING));
            oscar.MakeDeposit(0, 100);
            oscar.TransferFunds(0, 1, 50);

            Assert.AreEqual(oscar.getAccountBalance(0), oscar.getAccountBalance(1));
        }
    }
}
