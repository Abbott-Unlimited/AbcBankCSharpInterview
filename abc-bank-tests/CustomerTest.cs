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
                    "  withdrawal ($200.00)\n" +		// mn6473 - Added parentheses around withdrawal amount
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
        [Ignore]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(Account.SAVINGS));
            oscar.OpenAccount(new Account(Account.CHECKING));
			oscar.OpenAccount(new Account(Account.MAXI_SAVINGS));	// mn6473 - Added account
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

		[TestMethod]
		public void TestTransferFunds()
		{
			Account savingsAccount = new Account(Account.CHECKING);

			Customer henry = new Customer("Henry").OpenAccount(savingsAccount);

			savingsAccount.Deposit(4500);
			henry.Transfer(Account.CHECKING, Account.SAVINGS, 200);

			Assert.AreEqual("Statement for Henry\n" +
					"\n" +
					"Checking Account\n" +
					"  deposit $4,500.00\n" +
					"  withdrawal ($200.00)\n" +
					"Total $4,300.00\n" +
					"\n" +
					"Savings Account\n" +
					"  deposit $200.00\n" +
					"Total $200.00\n" +
					"\n" +
					"Total In All Accounts $4,500.00", henry.GetStatement());

			// Now test it for the condition where the "toAccount" already exists
			henry.Transfer(Account.CHECKING, Account.SAVINGS, 100);


			Assert.AreEqual("Statement for Henry\n" +
					"\n" +
					"Checking Account\n" +
					"  deposit $4,500.00\n" +
					"  withdrawal ($200.00)\n" +
					"  withdrawal ($100.00)\n" +
					"Total $4,200.00\n" +
					"\n" +
					"Savings Account\n" +
					"  deposit $200.00\n" +
					"  deposit $100.00\n" +
					"Total $300.00\n" +
					"\n" +
					"Total In All Accounts $4,500.00", henry.GetStatement());
		}
	}
}
