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
            oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        //I want to use [DataRow()]s to run multiple test betwen the different accounts 
        //I'm having issue finding the missing configuration that allws me to do this
        //[DataRow(Account.SAVINGS, Account.MAXI_SAVINGS,1000.0)]
        //[DataRow(Account.SAVINGS, Account.CHECKING,1000.0)]
        //[DataRow(Account.CHECKING, Account.SAVINGS,1000.0)]
        //[DataRow(Account.CHECKING, Account.MAXI_SAVINGS,1000.0)]
        //[DataRow(Account.MAXI_SAVINGS, Account.SAVINGS, 1000.0)]
        //[DataRow(Account.MAXI_SAVINGS, Account.CHECKING, 1000.0)]

        public void TestTransfer()
        {
            int fromAccountType = Account.SAVINGS;
            int toAccountType = Account.MAXI_SAVINGS;
            double amount = 1000.0;

            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Account maxi_SavingsAccount = new Account(Account.MAXI_SAVINGS);

            Customer Mike = new Customer("Oscar")
                        .OpenAccount(checkingAccount)
                        .OpenAccount(savingsAccount)
                        .OpenAccount(maxi_SavingsAccount);

            checkingAccount.Deposit(4000.0);
            savingsAccount.Deposit(4000.0);
            maxi_SavingsAccount.Deposit(4000.0);

            Account fromAccount = savingsAccount;
            Account toAccount = savingsAccount;

            switch (fromAccountType)
            {
                case Account.SAVINGS:
                    fromAccount = savingsAccount;
                    break;
                case Account.MAXI_SAVINGS:
                    fromAccount = maxi_SavingsAccount;
                    break;
                case Account.CHECKING:
                    fromAccount = checkingAccount;
                    break;
            }

            switch (toAccountType)
            {
                case Account.SAVINGS:
                    toAccount = savingsAccount;
                    break;
                case Account.MAXI_SAVINGS:
                    toAccount = maxi_SavingsAccount;
                    break;
                case Account.CHECKING:
                    toAccount = checkingAccount;
                    break;
            }

            bool success = Mike.Transfer(fromAccount, toAccount, amount);
            
            Assert.AreEqual(true, success);
            Assert.AreEqual(4000.0 - amount, fromAccount.sumTransactions());
            Assert.AreEqual(4000.0 + amount, toAccount.sumTransactions());
        }

    }

}

