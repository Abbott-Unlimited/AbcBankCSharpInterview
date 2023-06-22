using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using abc_bank.Extensions;

namespace abc_bank_tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Transaction()
        {
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }
        [TestMethod]
        public void TractionFromSavingsToCheckings()
        {
            //Arrange
            Customer bill = new Customer("Bill");

            Account savingsAccount = new Account(Account.AccountType.Savings);
            Account checkingsAccount = new Account(Account.AccountType.Checking);

            bill.OpenAccount(savingsAccount);
            bill.OpenAccount(checkingsAccount);

            savingsAccount.Deposit(200);

            //Action
            savingsAccount.TransferTo(checkingsAccount, 100);

            //Assert
            Assert.AreEqual(100, savingsAccount.SumTransactions());
            Assert.AreEqual(100, checkingsAccount.SumTransactions());
        }
    }
}
