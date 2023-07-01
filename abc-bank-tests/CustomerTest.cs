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
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount).OpenAccount(maxiAccount);

            // checking
            checkingAccount.Deposit(100.0);
            checkingAccount.Withdraw(500);

            bool goodexceptiondep0 = true;
            try
            {
                maxiAccount.Deposit(0);
                goodexceptiondep0 = false;
            }
            catch (Exception)
            {
                // intentionally left blank
                // come here if deposit is zero
            }

            if (!goodexceptiondep0)
            {
                Assert.Fail();
            }
            // savings
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            // First verify that amounts are correct.
            double summedsavings = savingsAccount.SumTransactions();
            double summedchecking = checkingAccount.SumTransactions();
            double summedmaxi = maxiAccount.SumTransactions();

            Assert.AreEqual(summedsavings, 3800);
            Assert.AreEqual(summedchecking, -400);
            Assert.AreEqual(summedmaxi, 0);

            double totalInAccounts = summedsavings + summedchecking + summedmaxi;

            // total in checking and savings
            Assert.AreEqual(totalInAccounts, 3400);

            // test 

            // make it clearer; possibly remove the text in order to verify the amounts only
            var henryStatement = henry.GetStatement();

            Assert.AreEqual(henryStatement, "Statement for Henry\n\nChecking Account\n  deposit $100.00\n  withdrawal " +
                "($500.00)\nTotal ($400.00)\n\nSavings Account\n  deposit $4,000.00\n  withdrawal ($200.00)\nTotal $3,800.00\n\nMaxi Savings Account\nTotal " +
                "$0.00\n\nTotal In All Accounts $3,400.00");
            //Assert.AreEqual("Statement for Henry\n" +
            //        "\n" +
            //        "Checking Account\n" +
            //        "  deposit $100.00\n" +
            //        "Total $100.00\n" +
            //        "\n" +
            //        "Savings Account\n" +
            //        "  deposit $4,000.00\n" +
            //        "  withdrawal $200.00\n" +
            //        "Total $3,800.00\n" +
            //        "\n" +
            //        "Total In All Accounts $3,900.00", henry.GetStatement());
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
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
