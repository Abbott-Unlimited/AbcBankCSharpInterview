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

            checkingAccount.Deposit(55000);
            savingsAccount.Deposit(40000);
            savingsAccount.Withdraw(200);
            savingsAccount.AddDailyInterest();
            checkingAccount.AddDailyInterest();

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "#00000001: Checking Account\n" +
                    "  deposit $55,000.00\n" +
                    "Total Interest Earned: $0.15\n" +
                    "Total $55,000.15\n" +
                    "\n" +
                    "#00000002: Savings Account\n" +
                    "  deposit $40,000.00\n" +
                    "  withdrawal $200.00\n" +
                    "Total Interest Earned: $0.22\n" +
                    "Total $39,800.22\n" +
                    "\n" +
                    "Total In All Accounts $94,800.37", henry.GetStatement());
        }

        [TestMethod]
        public void TestTransferSummary()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(10000.50m);
            savingsAccount.Deposit(4000.25m);
            savingsAccount.Withdraw(200.10m);
            henry.Transfer(1000.40m, 1, 2);

            Assert.AreEqual("Statement for Henry\n" +
                    "\n" +
                    "#00000001: Checking Account\n" +
                    "  deposit $10,000.50\n" +
                    "  withdrawal $1,000.40\n" +
                    "Total Interest Earned: $0.00\n" +
                    "Total $9,000.10\n" +
                    "\n" +
                    "#00000002: Savings Account\n" +
                    "  deposit $4,000.25\n" +
                    "  withdrawal $200.10\n" +
                    "  deposit $1,000.40\n" +
                    "Total Interest Earned: $0.00\n" +
                    "Total $4,800.55\n" +
                    "\n" +
                    "Total In All Accounts $13,800.65", henry.GetStatement());
        }
        [TestMethod]
        public void TestTransferSuccess()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(10000.50m);
            savingsAccount.Deposit(4000.25m);
            string status = (henry.Transfer(1000.40m, 1, 2));

            Assert.AreEqual(status, "Transfer complete.");
        }
        [TestMethod]
        public void TestTransferSameNumber()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(10000.50m);
            savingsAccount.Deposit(4000.25m);
            string status = (henry.Transfer(1000.40m, 1, 1));          
            
            Assert.AreEqual(status, "ERROR: Sending and receiving account numbers are the same.");
        }


        [TestMethod]
        public void TestTransferNotEnough()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(800);
            savingsAccount.Deposit(4000.25m);
            string status = (henry.Transfer(1000.40m, 1, 2));

            Assert.AreEqual(status, "ERROR: Insufficient funds.");
        }
        [TestMethod]
        public void TestTransferInvalidFirstNumber()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(10000.50m);
            savingsAccount.Deposit(4000.25m);
            string status = (henry.Transfer(1000.40m, 3, 1));

            Assert.AreEqual(status, "ERROR: One or more account numbers are invalid.");
        }
        public void TestTransferInvalidSecondNumber()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(10000.50m);
            savingsAccount.Deposit(4000.25m);
            string status = (henry.Transfer(1000.40m, 1, 3));

            Assert.AreEqual(status, "ERROR: One or more account numbers are invalid.");
        }
        public void TestTransferInvalidAmount()
        {
            Account checkingAccount = new Account(Account.CHECKING);
            Account savingsAccount = new Account(Account.SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(10000.50m);
            savingsAccount.Deposit(4000.25m);
            string status = (henry.Transfer(-1, 1, 3));

            Assert.AreEqual(status, "ERROR: Transfer amount cannot be less than 1 cent.");
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
            oscar.OpenAccount(new Account(Account.CHECKING));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestInterestChecking()
        {
            Account checkingAccount = new Account(Account.CHECKING);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount);

            decimal target = 55000 + (55000 * 0.001m); //55

            checkingAccount.Deposit(55000);

            for (int i = 0; i < 365; i++)
            {
                checkingAccount.AddDailyInterest();
            }

            Assert.AreEqual(target, Decimal.Round(checkingAccount.sumTransactions()));
        }
        [TestMethod]
        public void TestInterestSavings()
        {
            Account savingsAccount = new Account(Account.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(savingsAccount);

            decimal target1 = 1000 + (1000 * 0.001m); //1
            decimal target2 = 54000 + (54000 * 0.002m); //108

            savingsAccount.Deposit(55000);

            for (int i = 0; i < 365; i++)
            {
                savingsAccount.AddDailyInterest();
            }

            Assert.AreEqual((target1 + target2), Decimal.Round(savingsAccount.sumTransactions()));
        }
        [TestMethod]
        public void TestInterestMaxiHigh()
        {
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(maxiAccount);
            decimal target = 57820;
            maxiAccount.Deposit(55000);

            for (int i = 0; i < 365; i++)
            {
                maxiAccount.AddDailyInterest();
            }

            Assert.AreEqual(target, Decimal.Round(maxiAccount.sumTransactions()));
        }
      
        [TestMethod]
        public void TestInterestMaxiLow()
        {
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            Customer henry = new Customer("Henry").OpenAccount(maxiAccount);
            decimal target = 55055;
            maxiAccount.Deposit(56000);
            maxiAccount.Withdraw(1000);

            for (int i = 0; i < 365; i++)
            {
                maxiAccount.AddDailyInterest();
            }

            Assert.AreEqual(target, Decimal.Round(maxiAccount.sumTransactions()));
        }



    }
}
