using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestStatement()
        {
            Account checkingAccount = new CheckingAccount();
            Account savingsAccount = new SavingAccount();

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            


            checkingAccount.Deposit(100.0, false);
            savingsAccount.Deposit(4000.0, false);
            savingsAccount.Withdraw(200.0, false);
            henry.Transfer(AccountType.SAVINGS, AccountType.CHECKING, 1000.00);

            var statememt = henry.GetStatement();

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $100.00\n" +
                    "  transferin $1,000.00\n" +
                    "Total $1,100.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "  transferout $1,000.00\n" +
                    "Total $2,800.00\n" +
                    "\n" +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new SavingAccount());
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new SavingAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        [Ignore]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new SavingAccount());
            oscar.OpenAccount(new CheckingAccount());
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TranferTest()
        {
            Customer john = new Customer("John");
            Account johnCheckingAccount = new CheckingAccount();
            Account johnSavingAccount = new SavingAccount();
            johnCheckingAccount.Deposit(200.00, false);
            john.OpenAccount(johnCheckingAccount);
            john.OpenAccount(johnSavingAccount);
            john.Transfer(AccountType.CHECKING, AccountType.SAVINGS, 100.00);
            Assert.AreEqual(100, johnSavingAccount.Ballance);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TranferSameAccount()
        {
            Customer john = new Customer("John");
            Account johnCheckingAccount = new CheckingAccount();
            johnCheckingAccount.Deposit(200.00, false);
            john.OpenAccount(johnCheckingAccount);
            john.Transfer(AccountType.CHECKING, AccountType.CHECKING, 100.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TranferNegativeAmount()
        {
            Customer john = new Customer("John");
            Account johnCheckingAccount = new CheckingAccount();
            Account johnSavingAccount = new SavingAccount();
            johnCheckingAccount.Deposit(200.00, false);
            john.OpenAccount(johnCheckingAccount);
            john.OpenAccount(johnSavingAccount);
            john.Transfer(AccountType.CHECKING, AccountType.SAVINGS, -100.00);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TranferNotEnough()
        {
            Customer john = new Customer("John");
            Account johnCheckingAccount = new CheckingAccount();
            Account johnSavingAccount = new SavingAccount();
            johnCheckingAccount.Deposit(200.00, false);
            john.OpenAccount(johnCheckingAccount);
            john.OpenAccount(johnSavingAccount);
            john.Transfer(AccountType.CHECKING, AccountType.SAVINGS, 300.00);
        }
    }
}
