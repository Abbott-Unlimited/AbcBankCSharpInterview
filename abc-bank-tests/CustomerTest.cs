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
            //Arrange
            var expected = "Statement for Henry\n" +
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
                    "Total In All Accounts $3,900.00";
            Account checkingAccount = new Account(Account.AccountType.Checking);
            Account savingsAccount = new Account(Account.AccountType.Savings);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            //Act
            var actual = henry.GetStatement();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestOneAccount()
        {
            //Arrange
            var account = new Account(Account.AccountType.Savings);
            Customer oscar = new Customer("Oscar").OpenAccount(account);

            //Act
            var numberOfAccounts = oscar.GetNumberOfAccounts();
            
            //Assert
            Assert.AreEqual(1, numberOfAccounts);
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            //Arrange
            Customer oscar = new Customer("Oscar");
            Account savingsAccount = new Account(Account.AccountType.Savings);
            Account checkingsAccount = new Account(Account.AccountType.Checking);
            oscar.OpenAccount(savingsAccount);
            oscar.OpenAccount(checkingsAccount);

            //Act
            var numberOfAccounts = oscar.GetNumberOfAccounts();

            //Assert
            Assert.AreEqual(2, numberOfAccounts);
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar");
            Account savingsAccount = new Account(Account.AccountType.Savings);
            Account checkingsAccount = new Account(Account.AccountType.Checking);
            Account maxiSavingsAccount = new Account(Account.AccountType.MaxiSavings);
            oscar.OpenAccount(savingsAccount);
            oscar.OpenAccount(checkingsAccount);
            oscar.OpenAccount(maxiSavingsAccount);
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
