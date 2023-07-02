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
            Transaction t = new Transaction(5);
            //t instanceOf Transaction
            Assert.IsTrue(t.GetType() == typeof(Transaction));
        }

        [TestMethod]
        public void TransferBetweenAccounts()
        {
            Bank bank = new Bank();

            Customer john = new Customer("John");
            Account johnacctchecking = new Account(Account.CHECKING);
            john.OpenAccount(johnacctchecking);
            johnacctchecking.Deposit(5000);

            bank.AddCustomer(john);

            // add savings account
            Account johnacctsave = new Account(Account.SAVINGS);
            john.OpenAccount(johnacctsave);
            johnacctsave.Deposit(3000);

            johnacctchecking.DoTransferIntoThisAccount(300, johnacctsave);

            Assert.AreEqual(johnacctsave.Transactions[1].amount, -300);
            Assert.AreEqual(johnacctchecking.Transactions[1].amount, 300);

            var sumchecking = johnacctchecking.SumTransactions();
            var sumsavings = johnacctsave.SumTransactions();

            Assert.AreEqual(sumchecking, 5300);
            Assert.AreEqual(sumsavings, 2700);

        }
    }
}
