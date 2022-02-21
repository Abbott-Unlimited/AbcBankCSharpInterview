using abc_bank.Accounts;
using abc_bank_tests.MockObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests.AccountTests
{
    [TestClass]
    public class CheckingAccountTest
    {
        [TestMethod]
        public void GetAccountType_TrueIfEqualsAccountTypeChecking()
        {
            Account acct = Account.Create(MockDateProvider.Instance, Account.AccountType.Checking);

            Assert.AreEqual(Account.AccountType.Checking, acct.GetAccountType());
        }
    }
}
