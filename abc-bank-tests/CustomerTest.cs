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
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);
            savingsAccount.Withdraw(200.0M);

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
        public void TestZeroAccount()
        {
            Customer oscar = new Customer("Oscar");

            Assert.AreEqual(0, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(AccountType.SAVINGS));

            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(AccountType.SAVINGS));
            oscar.OpenAccount(new Account(AccountType.CHECKING));

            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(AccountType.SAVINGS));
            oscar.OpenAccount(new Account(AccountType.CHECKING));
            oscar.OpenAccount(new Account(AccountType.MAXI_SAVINGS));

            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }


        [TestMethod]
        public void TransferCheckingToSavingsSuccess()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);

            henry.TransferMoney(checkingAccount.GetAccountType(), savingsAccount.GetAccountType(), 50);

            Assert.AreEqual("Statement for Henry\n" +
                            "\n" +
                            "Checking Account\n" +
                            "  deposit $100.00\n" +
                            "  withdrawal $50.00\n" +
                            "Total $50.00\n" +
                            "\n" +
                            "Savings Account\n" +
                            "  deposit $4,000.00\n" +
                            "  deposit $50.00\n" +
                            "Total $4,050.00\n" +
                            "\n" +
                            "Total In All Accounts $4,100.00", henry.GetStatement());
        }

        [TestMethod]
        public void TransferNonExistingAccountFailure()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount);

            checkingAccount.Deposit(100.0M);

            var result = henry.TransferMoney(AccountType.CHECKING, AccountType.SAVINGS, 50);

            Assert.AreEqual(result.Success, false);
            Assert.IsTrue(result.GetType() == typeof(FailureResult));
            if (result.GetType() == typeof(FailureResult))
            {
                Assert.IsTrue(((FailureResult)result).Message == "One Or Both of these accounts do not exist");
            }
        }

        [TestMethod]
        public void TransferTooMuchFailure()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);

            var result = henry.TransferMoney(AccountType.CHECKING, AccountType.SAVINGS, 5000);

            Assert.AreEqual(result.Success, false);
            Assert.IsTrue(result.GetType() == typeof(FailureResult));
            if (result.GetType() == typeof(FailureResult))
            {
                Assert.IsTrue(((FailureResult)result).Message == "withdrawal greater then balance");
            }
        }

        [TestMethod]
        public void TransferNegativeAmountFailure()
        {
            Account checkingAccount = new Account(AccountType.CHECKING);
            Account savingsAccount = new Account(AccountType.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0M);
            savingsAccount.Deposit(4000.0M);

            var result = henry.TransferMoney(AccountType.CHECKING, AccountType.SAVINGS, -5000);

            Assert.AreEqual(result.Success, false);
            Assert.IsTrue(result.GetType() == typeof(FailureResult));
            if (result.GetType() == typeof(FailureResult))
            {
                Assert.IsTrue(((FailureResult)result).Message == "amount must be greater than zero");
            }
        }
    }
}
