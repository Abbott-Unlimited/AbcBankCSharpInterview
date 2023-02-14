using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;

namespace abc_bank_tests
{
    /// <summary>
    /// Tests to ensure customer can open an account, deposit or withdraw funds, gets request statement when asked.
    /// </summary>
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = AccountFactory.OpenAccount(AccountTypes.CHECKING);
            Account savingsAccount = AccountFactory.OpenAccount(AccountTypes.SAVINGS);

            Customer henry = new Customer("Henry");
            henry.AddAccount(checkingAccount);
            henry.AddAccount(savingsAccount);

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
            Customer oscar = new Customer("Oscar");
            Account savingsAccount = AccountFactory.OpenAccount(AccountTypes.SAVINGS);
            oscar.AddAccount(savingsAccount);
            var accounts = oscar.GetAccounts();
            Assert.AreEqual(1, accounts.Count);
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar");
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.SAVINGS));
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.CHECKING));
            var accounts = oscar.GetAccounts();
            Assert.AreEqual(2, accounts.Count);
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar");
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.SAVINGS));
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.CHECKING));
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.MAXI_SAVINGS));
            var accounts = oscar.GetAccounts();
            Assert.AreEqual(3, accounts.Count);
        }

        [TestMethod]
        public void ShouldTransferValues()
        {
            Customer oscar = new Customer("Oscar");
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.SAVINGS));
            oscar.AddAccount(AccountFactory.OpenAccount(AccountTypes.CHECKING));

            var checkingAccount = oscar.GetAccount(AccountTypes.CHECKING);
            checkingAccount.Deposit(200);
            var savingsAccount = oscar.GetAccount(AccountTypes.SAVINGS);
            var result = oscar.TransferAccountValue(checkingAccount, 200, savingsAccount);

            Assert.IsTrue(result.Success);
            
            Assert.IsTrue(oscar.GetAccount(AccountTypes.CHECKING).SumTransactions() == 0);
            Assert.IsTrue(oscar.GetAccount(AccountTypes.SAVINGS).SumTransactions() == 200);

        }
    }
}
