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
        public void TotalInterestSavings()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            oscar.OpenAccount(savings);
            savings.Deposit(1500.0);

            Assert.AreEqual(1.0 + 1.0, oscar.TotalInterestEarned());
        }


        [TestMethod]
        public void TotalInterestSavingsChecking()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            oscar.OpenAccount(savings);
            savings.Deposit(1500.0);

            Account checking = new Account(Account.CHECKING);
            oscar.OpenAccount(checking);
            checking.Deposit(100.0);

            Assert.AreEqual(1.0 + 1.0 + 0.1, oscar.TotalInterestEarned());
        }

        [TestMethod]
        public void TotalInterestSavingsCheckingMaxi()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            oscar.OpenAccount(savings);
            savings.Deposit(1500.0);
            double savingsInt = 1.0 + 1.0;

            Account checking = new Account(Account.CHECKING);
            oscar.OpenAccount(checking);
            checking.Deposit(100.0);
            double checkingInt = 0.1;

            Account maxi = new Account(Account.MAXI_SAVINGS);
            oscar.OpenAccount(maxi);
            maxi.Deposit(2500.0);
            //double maxiInt = 20.0 + 50.0 + 50.0;
            double maxiInt = 125.0;

            Assert.AreEqual(savingsInt + checkingInt + maxiInt, oscar.TotalInterestEarned());
        }

        [TestMethod]
        public void TransferAccounts()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            Account checking = new Account(Account.CHECKING);
            oscar.OpenAccount(checking).OpenAccount(savings);
            savings.Deposit(200);  

            oscar.Transfer(savings.GetBalance() / 2, savings, checking);

            Assert.AreEqual(savings.GetBalance(), checking.GetBalance());
        }

        [TestMethod]
        public void TranserAccountFailsInvalidAccount()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            Account checking = new Account(Account.CHECKING);
            oscar.OpenAccount(checking);
            savings.Deposit(200);

            try
            {
                oscar.Transfer(savings.GetBalance() / 2, savings, checking);
                Assert.Fail("Expected failed transfer, invalid accounts");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("customer does not own the provided accounts", ex.Message);
            }
        }

        [TestMethod]
        public void TranserAccountFailsInvalidAccountFunds()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            Account checking = new Account(Account.CHECKING);
            oscar.OpenAccount(checking).OpenAccount(savings);
            savings.Deposit(100);

            try
            {
                oscar.Transfer(200, savings, checking);
                Assert.Fail("Expected failed transfer, invalid amount");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("account does not have enough to transfer", ex.Message);
            }
        }

        [TestMethod]
        public void TranserAccountFailsInvalidTransferAmount()
        {
            Customer oscar = new Customer("Oscar");
            Account savings = new Account(Account.SAVINGS);
            Account checking = new Account(Account.CHECKING);
            oscar.OpenAccount(checking).OpenAccount(savings);
            savings.Deposit(100);

            try
            {
                oscar.Transfer(0, savings, checking);
                Assert.Fail("Expected failed transfer, invalid amount, <= 0");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("amount must be greater than 0", ex.Message);
            }
        }
    }
}
