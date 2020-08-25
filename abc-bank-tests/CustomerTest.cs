using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

//WT2 Claude Collins August 2020
//    Was ignored due to fail. Only two accounts were added, not three.

//WT3 Claude Collins August 2020 (xfer between accounts)

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

		//WT3 added below TestXfer()
		[TestMethod]
		public void TestXfer()
		{
			Account checkingAccount = new Account(Account.CHECKING);
			Account savingsAccount = new Account(Account.SAVINGS);

			Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

			checkingAccount.Deposit(100);

			henry.Xfer(checkingAccount, savingsAccount, 50);
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
		//[Ignore] //WT2 commented out
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
			oscar.OpenAccount(new Account(Account.MAXI_SAVINGS)); //added WT2
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
