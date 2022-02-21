using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;
using abc_bank.Customers;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.CustomerTests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void Constructor_PassNullName_TrueIfArgumentException()
        {
            try
            {
                Customer customer = new Customer(null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [TestMethod]
        public void Constructor_PassEmptyStringName_TrueIfArgumentNullException()
        {
            try
            {
                Customer customer = new Customer("");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [TestMethod]
        public void GetName_CreateCustomer_TrueIfNameIsCorrect()
        {
            Customer customer = new Customer("John");
            Assert.AreEqual("John", customer.GetName());
        }

        [TestMethod]
        public void OpenAccount_OpenAccount_TrueIfNameIsCorrectAndNumberOfAccountsIsCorrect()
        {
            Customer customer = new Customer("John");
            Customer testCustomer = customer.OpenAccount(new CheckingAccount());
            Assert.IsTrue("John" == testCustomer.GetName() && 1 == testCustomer.GetNumberOfAccounts());
        }

        public void GetNumberOfAccounts_AddOneAccount_TrueIfNumberOfAccountsIsOne()
        {
            Customer customer = new Customer("John");
            customer.OpenAccount(new CheckingAccount());

            Assert.AreEqual(1, customer.GetNumberOfAccounts());
        }

        [TestMethod]
        public void OpenAccount_NullAccount_TrueIfArgumentNullException()
        {
            try
            {
                Customer customer = new Customer("John");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [TestMethod]
        public void TotalInterestEarned_AddTwoAccountDeposits_TrueIfInterestEqualsExpected()
        {
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));

            Account checking = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Account savings = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);

            Customer customer = new Customer("John");
            customer.OpenAccount(checking)
                    .OpenAccount(savings);

            checking.Deposit(10000.0); // 10.0
            savings.Deposit(100000.0); // 200.0

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31));

            Assert.AreEqual(210.0, customer.TotalInterestEarned());
        }

        [TestMethod]
        public void GetStatement_StatementPrintsCorrectly_TrueIfStatementsAreEqual()
        {
            Account checkingAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Account savingsAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            Assert.AreEqual(
                    "Statement for Henry"  + Environment.NewLine +
                    Environment.NewLine    +
                    "Checking Account"     + Environment.NewLine +
                    "  deposit $100.00"    + Environment.NewLine +
                    "Total $100.00"        + Environment.NewLine +
                    Environment.NewLine    +
                    "Savings Account"      + Environment.NewLine +
                    "  deposit $4,000.00"  + Environment.NewLine +
                    "  withdrawal $200.00" + Environment.NewLine +
                    "Total $3,800.00"      + Environment.NewLine +
                    Environment.NewLine +
                    "Total In All Accounts $3,900.00", henry.GetStatement());
        }

        [TestMethod]
        public void GetNumberOfAccounts_OpenOneAccount_TrueIfNumberOfAccountsIsOne()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Savings));
            Assert.AreEqual(1, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void GetNumberOfAccounts_OpenTwoAccounts_TrueIfNumberOfAccountsIsTwo()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Savings))
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Checking));
            Assert.AreEqual(2, oscar.GetNumberOfAccounts());
        }

        [TestMethod]
        public void GetNumberOfAccounts_OpenThreeAccounts_TrueIfNumberOfAccountsIsThree()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Savings))
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Checking))
                    .OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings));
            Assert.AreEqual(3, oscar.GetNumberOfAccounts());
        }
    }
}
