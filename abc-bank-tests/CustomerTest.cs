using System;
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
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

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
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            oscar.OpenAccount(new Account(Account.SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "the user has no accounts")]
        public void TestTransferBetweenAccounts_UserHasNoAccount()
        {
            Customer oscar = new Customer("Oscar");

            oscar.TransferBetweenAccounts(0, 1, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void TestTransferBetweenAccounts_ZeroAmount()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(0, 1, 0.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "amount must be greater than zero")]
        public void TestTransferBetweenAccounts_NegativeAmount()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(0, 1, -800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "source account does not exist")]
        public void TestTransferBetweenAccounts_NegativeSourceAccountNumber()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(-1, 1, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "source account does not exist")]
        public void TestTransferBetweenAccounts_NonExistentSourceAccountNumber()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(2, 1, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "source account does not exist")]
        public void TestTransferBetweenAccounts_NullValuedSourceAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.OpenAccount(null);
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(0, 1, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "destination account does not exist")]
        public void TestTransferBetweenAccounts_NegativeDestinationAccountNumber()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(0, -1, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "destination account does not exist")]
        public void TestTransferBetweenAccounts_NonExistentDestinationAccountNumber()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(0, 2, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "destination account does not exist")]
        public void TestTransferBetweenAccounts_NullValuedDestinationAccount()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(null);

            oscar.TransferBetweenAccounts(0, 1, 800.0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "insufficient funds for transactions")]
        public void TestTransferBetweenAccounts_InsufficientFunds()
        {
            Customer oscar = new Customer("Oscar")
                .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            oscar.TransferBetweenAccounts(0, 1, 800.0);
        }

        [TestMethod]
        public void TestTransferBetweenAccounts_SuccessfulTransfer()
        {
            Customer oscar = new Customer("Oscar");

            Account oscarSavingsAccount = new Account(Account.SAVINGS);
            Account oscarCheckingAccount = new Account(Account.CHECKING);

            oscar.OpenAccount(oscarSavingsAccount);
            oscar.OpenAccount(oscarCheckingAccount);

            oscarSavingsAccount.Deposit(1000.00);

            oscar.TransferBetweenAccounts(0, 1, 800.0);

            Assert.IsTrue(oscarSavingsAccount.sumTransactions() == 200.00);

            Assert.IsTrue(oscarCheckingAccount.sumTransactions() == 800.00);
        }
    }
}
