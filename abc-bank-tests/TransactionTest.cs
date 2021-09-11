using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            var t = new Transaction(5, TransactionType.Deposit, DateTime.Now);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }

        [TestMethod]
        public void Transfer()
        {
            var cAccount = new Account(AccountType.Checking);
            var sAccount = new Account(AccountType.Savings);
            var frank = new Customer("Frank");
            frank.OpenAccount(cAccount);
            frank.OpenAccount(sAccount);
            cAccount.Deposit(100, DateTime.Now);
            sAccount.Deposit(100, DateTime.Now);
            cAccount.Transfer(100, sAccount);
            Assert.IsTrue(sAccount.SumTransactions() == 200.0);
        }
    }
}