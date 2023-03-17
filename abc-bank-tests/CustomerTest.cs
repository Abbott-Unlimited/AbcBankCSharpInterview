using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void CanOpenAccountSuccessfully()
        {
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer david = new Customer("David");
            david.OpenAccount(checkingAccount);

            Assert.AreEqual(true, david.Accounts.Exists(x => x.AccountType == AccountTypeEnum.CHECKING));
        }

        [TestMethod]
        public void CanOpenMultipleAccountsSuccessfully()
        {
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer david = new Customer("David");
            david.OpenAccount(checkingAccount).OpenAccount(checkingAccount);

            Assert.AreEqual(true, david.Accounts.Exists(x => x.AccountType == AccountTypeEnum.CHECKING));
            Assert.AreEqual(2, david.NumberOfAccounts);
        }

        [TestMethod]
        public void CanCountCustomerAccountsSuccessfully()
        {
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer david = new Customer("David");
            david.OpenAccount(checkingAccount).OpenAccount(checkingAccount);

            Assert.AreEqual(true, david.Accounts.Exists(x => x.AccountType == AccountTypeEnum.CHECKING));
            Assert.AreEqual(2, david.NumberOfAccounts);
        }

        [TestMethod]
        public void CanGetCustomerAccountInterestEarned()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid());
        }

        [TestMethod]
        public void CanGetCustomerStatement()
        {
            Bank bank = new Bank();
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Customer bill = new Customer("Bill");
            bank.OpenAccount(bill, checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual("Statement for Bill\n\nChecking Account\n  deposit $100.00\nTotal $100.00\n\nTotal In All Accounts $100.00", bill.GetStatement());
        }

        [TestMethod]
        public void TestApp()
        {
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);

            Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

            var statement = henry.GetStatement();

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
                    "Total In All Accounts $3,900.00", statement);
        }

        [TestMethod]
        public void TestOneAccount()
        {
            Customer oscar = new Customer("Oscar").OpenAccount(new Account(AccountTypeEnum.SAVINGS));
            Assert.AreEqual(1, oscar.NumberOfAccounts);
        }

        [TestMethod]
        public void TestTwoAccount()
        {
            Customer oscar = new Customer("Oscar")
                 .OpenAccount(new Account(AccountTypeEnum.SAVINGS));
            oscar.OpenAccount(new Account(AccountTypeEnum.CHECKING));
            Assert.AreEqual(2, oscar.NumberOfAccounts);
        }

        [TestMethod]
        public void TestThreeAccounts()
        {
            Customer oscar = new Customer("Oscar")
                    .OpenAccount(new Account(AccountTypeEnum.SAVINGS));
            oscar.OpenAccount(new Account(AccountTypeEnum.CHECKING));
            oscar.OpenAccount(new Account(AccountTypeEnum.MAXI_SAVINGS));
            Assert.AreEqual(3, oscar.NumberOfAccounts);
        }

        [TestMethod]
        public void TransferFromOneAccountToAnotherWorksSuccessfully()
        {
            Customer oscar = new Customer("Oscar");
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);

            checkingAccount.Deposit(400);
            savingsAccount.Deposit(100);
            oscar.OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            string result = oscar.Transfer(checkingAccount, savingsAccount, 200);

            Assert.AreEqual(checkingAccount.sumTransactions(), 200);
            Assert.AreEqual(savingsAccount.sumTransactions(), 300);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TransferFromOneAccountToAnotherThrowsArgumentException()
        {
            Customer oscar = new Customer("Oscar");
            Account checkingAccount = null;
            Account savingsAccount = null;

            oscar.OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            string result = oscar.Transfer(checkingAccount, savingsAccount, 2000);
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void TransferFromOneAccountToAnotherThrowsException()
        {
            Customer oscar = new Customer("Oscar");
            Account checkingAccount = new Account(AccountTypeEnum.CHECKING);
            Account savingsAccount = new Account(AccountTypeEnum.SAVINGS);

            checkingAccount.Deposit(400);
            savingsAccount.Deposit(100);

            oscar.OpenAccount(checkingAccount).OpenAccount(savingsAccount);

            string result = oscar.Transfer(checkingAccount, savingsAccount, 2000);
        }
    }
}
