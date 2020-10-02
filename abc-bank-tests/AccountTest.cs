using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestInterestEarned()
        {
            Account MaxiAccount = new Account(Account.MAXI_SAVINGS);
            MaxiAccount.Deposit(200);
            Transaction t = new Transaction(-100);
            t.transactionDate = DateTime.Today.AddDays(-2);
            MaxiAccount.transactions.Add(t);
            Assert.AreEqual(0.1, MaxiAccount.InterestEarned());

            MaxiAccount = new Account(Account.MAXI_SAVINGS);
            MaxiAccount.Deposit(200);
            t = new Transaction(-100);
            t.transactionDate = DateTime.Today.AddDays(-20);
            MaxiAccount.transactions.Add(t);
            Assert.AreEqual(5, MaxiAccount.InterestEarned());
        }
    }
}
