using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank.Accounts;
using abc_bank_tests.MockObjects;

namespace abc_bank_tests.TransactionTests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Constructor_CreateInstance_IsInstanceOfTransactionType()
        {
            Transaction transaction = new Transaction(MockDateProvider.Instance, 5, Transaction.TransactionType.Deposit);

            Assert.IsInstanceOfType(transaction, typeof(Transaction));
        }

        [TestMethod]
        public void Constructor_AddInvalidPositiveDouble_ExceptionIsInstanceOfArgumentOutOfRangeException()
        {
            try
            {
                Transaction t = new Transaction(MockDateProvider.Instance, double.MaxValue + 1, Transaction.TransactionType.Deposit);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void Constructor_AddInvalidNegativeDouble_ExceptionIsInstanceOfArgumentOutOfRangeException()
        {
            try
            {
                Transaction t = new Transaction(MockDateProvider.Instance, double.MinValue - 1, Transaction.TransactionType.Deposit);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void Constructor_CreateValidDepositTransaction_TrueIfAllSetCorrectly()
        {
            DateTime date = new DateTime(2021, 1, 1);
            MockDateProvider.Instance.PresetDate(date);

            Transaction transaction = new Transaction(MockDateProvider.Instance, 500.00, Transaction.TransactionType.Deposit);

            Assert.IsTrue(transaction.Amount == 500.00 && transaction.Type == Transaction.TransactionType.Deposit && transaction.TransactionDate == date);
        }

        [TestMethod]
        public void Constructor_CreateValidWithdrawTransaction_TrueIfAllSetCorrectly()
        {
            DateTime date = new DateTime(2021, 1, 1);
            MockDateProvider.Instance.PresetDate(date);

            Transaction transaction = new Transaction(MockDateProvider.Instance, -500.00, Transaction.TransactionType.Deposit);

            Assert.IsTrue(transaction.Amount == -500.00 && transaction.Type == Transaction.TransactionType.Deposit && transaction.TransactionDate == date);
        }
    }
}
