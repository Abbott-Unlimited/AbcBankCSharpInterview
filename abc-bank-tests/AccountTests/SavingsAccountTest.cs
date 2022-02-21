using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class SavingsAccountTest
    {
        [TestMethod]
        public void GetAccountType_TrueIfEqualsAccountTypeChecking()
        {
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);

            Assert.AreEqual(Account.AccountType.Checking, acct.GetAccountType());
        }
    }
}
