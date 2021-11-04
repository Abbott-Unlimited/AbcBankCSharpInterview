using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestHenryApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            //Note, since they were all deposited and withdrawn moments ago, they have no interest
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
        public void TestOscarApp()
        {
            //Oscar
            Account oscarCheckingAccount = new Account(Account.CHECKING);
            Account oscarMaxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Account oscarSavingsAccount = new Account(Account.SAVINGS);

            oscarCheckingAccount.Deposit(1000.0);            
            oscarSavingsAccount.Deposit(200.0);
            oscarMaxiSavingsAccount.Deposit(4000.0);

            Customer oscar = new Customer("Oscar")
                    .OpenAccount(oscarCheckingAccount);            
            oscar.OpenAccount(oscarSavingsAccount);
            oscar.OpenAccount(oscarMaxiSavingsAccount);

            //Note, since they were all deposited and withdrawn moments ago, they have no interest
            Assert.AreEqual("Statement for Oscar\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $1,000.00\n" +
                    "Total $1,000.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $200.00\n" +
                    "Total $200.00\n" +
                    "\n" +
                   "Maxi Savings Account\n" +
                    "  deposit $4,000.00\n" +
                    "Total $4,000.00\n" +
                    "\n" +
                    "Total In All Accounts $5,200.00", oscar.GetStatement());
        }

        [TestMethod]
        public void TestTransferApp()
        {
            //Oscar
            Account oscarCheckingAccount = new Account(Account.CHECKING);
            Account oscarSavingsAccount = new Account(Account.SAVINGS);

            oscarCheckingAccount.Deposit(1000.0);

            Customer oscar = new Customer("Oscar")
                    .OpenAccount(oscarCheckingAccount);
            oscar.OpenAccount(oscarSavingsAccount);
            oscar.Transfer(oscarCheckingAccount, oscarSavingsAccount, 200.00);

            //Note, since they were all deposited and withdrawn moments ago, they have no interest
            Assert.AreEqual("Statement for Oscar\n" +
                    "\n" +
                    "Checking Account\n" +
                    "  deposit $1,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total $800.00\n" +
                    "\n" +
                    "Savings Account\n" +
                    "  deposit $200.00\n" +
                    "Total $200.00\n" +
                    "\n" +
                    "Total In All Accounts $1,000.00", oscar.GetStatement());
        }

        [TestMethod]
        public void TestExpectedIntrestApp()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            //checkingAccount.Deposit(100.0);
            //savingsAccount.Deposit(4000.0);
            //savingsAccount.Withdraw(200.0);
            checkingAccount.transactions.Clear();
            DateTime currentTime = DateProvider.getInstance().Now();
            DateTime lastYear = new DateTime(currentTime.Year - 1, currentTime.Month, currentTime.Day);
            //Note, having to backdate them to allow intrest to be calculated 
            checkingAccount.transactions.Add(new Transaction(100.00, lastYear));
            savingsAccount.transactions.Add(new Transaction(4000.0, lastYear));
            savingsAccount.transactions.Add(new Transaction(-200.0, lastYear));

            //Oscar
            Account oscarCheckingAccount = new Account(Account.CHECKING);
            Account oscarMaxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            Account oscarSavingsAccount = new Account(Account.SAVINGS);

            //oscarCheckingAccount.Deposit(1000.0);            
            //oscarSavingsAccount.Deposit(200.0);
            //oscarMaxiSavingsAccount.Deposit(4000.0);
            //Note, having to backdate them to allow intrest to be calculated 
            oscarCheckingAccount.transactions.Add(new Transaction(1000.0, lastYear));
            oscarSavingsAccount.transactions.Add(new Transaction(200.0, lastYear));
            oscarMaxiSavingsAccount.transactions.Add(new Transaction(4000.0, lastYear));

            Customer oscar = new Customer("Oscar")
                    .OpenAccount(oscarCheckingAccount);            
            oscar.OpenAccount(oscarSavingsAccount);
            oscar.OpenAccount(oscarMaxiSavingsAccount);

            Assert.AreEqual((.1+6.6).ToString("C2"), henry.TotalInterestEarnedSinceLastTransaction().ToString("C2"));
            Assert.AreEqual((5.2).ToString("C2"), oscar.TotalInterestEarnedSinceLastTransaction().ToString("C2"));
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
            oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
