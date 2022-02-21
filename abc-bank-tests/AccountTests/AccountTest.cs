using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class AccountTest
    {

        [TestMethod]
        public void Create_DateProviderNotIncluded_ReturnsArgumentNullException()
        {
            try
            {
                Account acct = Account.Create(null, Account.AccountType.MaxiSavings);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [TestMethod]
        public void Create_CheckingAccount_TrueIfTypeIsCheckingAccount()
        {
            Account account = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Assert.IsTrue(account is CheckingAccount);
        }

        [TestMethod]
        public void Create_SavingsAccount_TrueIfTypeIsSavingsAccount()
        {
            Account account = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);
            Assert.IsTrue(account is SavingsAccount);
        }

        [TestMethod]
        public void Create_MaxiSavingsAccount_TrueIfTypeIsMaxiSavingsAccount()
        {
            Account account = Account.Create(MockDateProvider.Instance, Account.AccountType.MaxiSavings);
            Assert.IsTrue(account is MaxiSavingsAccount);
        }

        [TestMethod]
        public void Create_UnknownAccount_TrueIfExceptionIsArgumentException()
        {
            try
            {
                Account account = Account.Create(MockDateProvider.Instance, default);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
            }
        }

        [TestMethod]
        public void Transfer_AmountFromAccountAtoAccountB_TrueIfAccountsAreAccurate()
        {
            Account checkingAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);
            Account savingsAccount = Account.Create(MockDateProvider.Instance, Account.AccountType.Savings);

            checkingAccount.Deposit(1000.0);
            savingsAccount.Deposit(5000.0);

            savingsAccount.Transfer(1000.0, checkingAccount);

            double savingsSum = savingsAccount.SumTransactions();
            double checkingSum = checkingAccount.SumTransactions();

            Assert.IsTrue(savingsSum == 4000.0 && checkingSum == 2000.0);

        }
    }
}
