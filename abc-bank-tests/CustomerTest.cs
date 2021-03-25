using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        /// <summary>
        /// Tests that statements are properly generated for account activity.
        /// </summary>
        [TestMethod]
        public void TestStatementGeneration()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.00m);
            savingsAccount.Deposit(4000.00m);
            savingsAccount.Withdraw(200.00m);

            Assert.AreEqual("Statement for Henry" + Environment.NewLine +
                    Environment.NewLine +
                    "Checking Account" + Environment.NewLine +
                    "  deposit $100.00" + Environment.NewLine +
                    "Total $100.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Savings Account" + Environment.NewLine +
                    "  deposit $4,000.00" + Environment.NewLine +
                    "  withdrawal $200.00" + Environment.NewLine +
                    "Total $3,800.00" + Environment.NewLine +
                    Environment.NewLine +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        /// <summary>
        /// Tests the "number of accounts" functionality when one account exists.
        /// </summary>
        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(AccountType.SAVINGS));
            Assert.AreEqual(1, oscar.NumberOfAccounts);
        }

        /// <summary>
        /// Tests the "number of accounts" functionality when two accounts exist.
        /// </summary>
        [TestMethod]
        public void TestTwoAccounts()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(AccountType.SAVINGS));
            oscar.OpenAccount(new Account(AccountType.CHECKING));
            Assert.AreEqual(2, oscar.NumberOfAccounts);
        }

        /// <summary>
        /// Tests the "number of accounts" functionality when three accounts exist.
        /// </summary>
        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(AccountType.SAVINGS));
            oscar.OpenAccount(new Account(AccountType.CHECKING));
            oscar.OpenAccount(new Account(AccountType.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.NumberOfAccounts);
        }
    }
}
