using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Accounts;
using abc_bank.Utilities.Date;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.BankTests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary()
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(Account.Create(MockDateProvider.Instance, Account.AccountType.Checking));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount()
        {
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));
            Bank bank = new Bank();
            Account checkingAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(1000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31));

            Assert.AreEqual(1.00, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account()
        {
            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));

            Bank bank = new Bank();
            Account checkingAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(10000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31));

            Assert.AreEqual(20.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Maxi_savings_account() 
        {

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 1, 1));

            Bank bank = new Bank();
            Account maxiSavingsAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(1000.0);

            MockDateProvider.Instance.PresetDate(new DateTime(2021, 12, 31));

            Assert.AreEqual(50.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
    }
}
