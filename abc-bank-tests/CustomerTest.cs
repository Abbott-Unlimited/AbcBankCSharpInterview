using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestCategory("GetStatement"), TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Customer henry = new Customer("Henry");

            henry.OpenAccount(checkingAccount);
            henry.OpenAccount(savingsAccount);
            henry.OpenAccount(maxiSavingsAccount);

            checkingAccount.Deposit(100.0m);
            savingsAccount.Deposit(4000.0m);
            savingsAccount.Withdraw(200.0m);
            maxiSavingsAccount.Deposit(25000m);
            maxiSavingsAccount.Withdraw(5000m);

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
                            "Maxi Savings Account\n" +
                            "  deposit $25,000.00\n" +
                            "  withdrawal $5,000.00\n" +
                            "Total $20,000.00\n" +
                            "\n" +
                            "Total In All Accounts $23,900.00", henry.GetStatement());
        }

        [TestCategory("GetNumberOfAccounts"), TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar");

            oscar.OpenAccount(new Account(Account.SAVINGS));

            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestCategory("GetNumberOfAccounts"), TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar");

            oscar.OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));

            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestCategory("GetNumberOfAccounts"), TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar");

            oscar.OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));

            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestCategory("Transfer"), TestMethod]
        public void Transfer()
        {
            Customer oscar = new Customer("Oscar");
            Account to = new Account(Account.SAVINGS);
            Account from = new Account(Account.CHECKING);
            Random randomizer = new Random();

            decimal toDeposit = randomizer.Next(500, 1000);
            decimal fromDeposit = randomizer.Next(500, 1000);
            decimal transferAmount = randomizer.Next(1, 100);

            decimal toFinal = toDeposit + transferAmount;
            decimal fromFinal = fromDeposit - transferAmount;

            oscar.OpenAccount(to);
            oscar.OpenAccount(from);
            to.Deposit(toDeposit);
            from.Deposit(fromDeposit);
            oscar.Transfer(to, from, transferAmount);

            Assert.AreEqual(toFinal, to.sumTransactions());
            Assert.AreEqual(fromFinal, from.sumTransactions());
        }

        [TestCategory("Transfer"), TestMethod]
        [ExpectedException(typeof(ArgumentException), "Account being deposited into does not belong to customer")]
        public void TransferMissingTo()
        {
            Customer oscar = new Customer("Oscar");
            Account to = new Account(Account.SAVINGS);
            Account from = new Account(Account.CHECKING);
            Random randomizer = new Random();

            decimal toDeposit = randomizer.Next(500, 1000);
            decimal fromDeposit = randomizer.Next(500, 1000);
            decimal transferAmount = randomizer.Next(1, 100);

            // oscar.OpenAccount(to);
            oscar.OpenAccount(from);
            to.Deposit(toDeposit);
            from.Deposit(fromDeposit);

            oscar.Transfer(to, from, transferAmount);
        }

        [TestCategory("Transfer"), TestMethod]
        [ExpectedException(typeof(ArgumentException), "Account being withdrawn from does not belong to customer")]
        public void TransferMissingFrom()
        {
            Customer oscar = new Customer("Oscar");
            Account to = new Account(Account.SAVINGS);
            Account from = new Account(Account.CHECKING);
            Random randomizer = new Random();

            decimal toDeposit = randomizer.Next(500, 1000);
            decimal fromDeposit = randomizer.Next(500, 1000);
            decimal transferAmount = randomizer.Next(1, 100);

            oscar.OpenAccount(to);
            // oscar.OpenAccount(from);
            to.Deposit(toDeposit);
            from.Deposit(fromDeposit);
            oscar.Transfer(to, from, transferAmount);
        }

        [TestCategory("Transfer"), TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Insufficient funds")]
        public void TransferInsufficientFunds()
        {
            Customer oscar = new Customer("Oscar");
            Account to = new Account(Account.SAVINGS);
            Account from = new Account(Account.CHECKING);
            Random randomizer = new Random();

            decimal toDeposit = randomizer.Next(500, 1000);
            decimal fromDeposit = randomizer.Next(500, 1000);
            // decimal transferAmount = randomizer.Next(1, 100);
            decimal transferAmount = randomizer.Next(2000, 10000);

            oscar.OpenAccount(to);
            oscar.OpenAccount(from);
            to.Deposit(toDeposit);
            from.Deposit(fromDeposit);
            oscar.Transfer(to, from, transferAmount);
        }

        [TestCategory("Transfer"), TestMethod]
        [ExpectedException(typeof(ArgumentException), "Amount must be greater than zero.")]
        public void TransferNegative()
        {
            Customer oscar = new Customer("Oscar");
            Account to = new Account(Account.SAVINGS);
            Account from = new Account(Account.CHECKING);
            Random randomizer = new Random();

            decimal toDeposit = randomizer.Next(500, 1000);
            decimal fromDeposit = randomizer.Next(500, 1000);
            // decimal transferAmount = randomizer.Next(1, 100);
            decimal transferAmount = randomizer.Next(-50, -1);

            oscar.OpenAccount(to);
            oscar.OpenAccount(from);
            to.Deposit(toDeposit);
            from.Deposit(fromDeposit);
            oscar.Transfer(to, from, transferAmount);
        }
    }
}