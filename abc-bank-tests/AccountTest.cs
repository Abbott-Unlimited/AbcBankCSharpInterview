using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;
using static abc_bank.Transaction;

namespace abc_bank_tests
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TransferFromCheckingToSavings()
        {
            const double withdrawAmount  = 200.00;
            var          savingsAccount  = new Account(Account.Savings);
            var          checkingAccount = new Account(Account.Checking);

            checkingAccount.Deposit(500.00);
            Account.TransferAmount(withdrawAmount, checkingAccount, savingsAccount, DateTime.Now);

            Assert.AreEqual(savingsAccount.SumTransactions(), 200.00);
            Assert.AreEqual(checkingAccount.SumTransactions(), 300.00);
        }

        [TestMethod]
        public void TransferFromSavingsToChecking()
        {
            const double withdrawAmount  = 200.00;
            var          checkingAccount = new Account(Account.Checking);
            var          savingsAccount  = new Account(Account.Savings);

            savingsAccount.Deposit(500.00);
            Account.TransferAmount(withdrawAmount, savingsAccount, checkingAccount, DateTime.Now);

            Assert.AreEqual(savingsAccount.SumTransactions(), 300.00);
            Assert.AreEqual(checkingAccount.SumTransactions(), 200.00);
        }

        [TestMethod]
        public void InterestSavingsValidationLessOneThousand()
        {
            var account = new Account(Account.Savings);
            account.Transactions.Add(new Transaction(500, DateTime.Now, TransactionTypeEnum.Deposit));
            var interestEarned = account.InterestEarned();

            Assert.AreEqual(interestEarned, 0.5);

            account.Transactions.Add(new Transaction(0.5, DateTime.Now, TransactionTypeEnum.Deposit));
            var totalBalance = account.Transactions.Sum(x => x.Amount);

            Assert.AreEqual(account.SumTransactions(), totalBalance);
        }

        [TestMethod]
        public void InterestMaxiSavingsFivePercent()
        {
            var account     = new Account(Account.MaxiSavings);
            account.Transactions.AddRange(new List<Transaction>
            {
                new Transaction(500.00, DateTime.Today, TransactionTypeEnum.Deposit),
                new Transaction(200.00, DateTime.Today.AddDays(-2), TransactionTypeEnum.Transfer)
            });
            var interestEarned = account.InterestEarned();

            Assert.AreEqual(interestEarned, 15.00);

            account.Transactions.Add(new Transaction(interestEarned, DateTime.Now, TransactionTypeEnum.Deposit));
            var totalBalance = account.SumTransactions();

            Assert.AreEqual(account.SumTransactions(), totalBalance);
        }

        [TestMethod]
        public void InterestMaxiSavingsOnePercent()
        {
            var account = new Account(Account.MaxiSavings);
            account.Transactions.AddRange(new List<Transaction>
            {
                new Transaction(500.00, DateTime.Today, TransactionTypeEnum.Deposit),
                new Transaction(200.00, DateTime.Today.AddDays(-2), TransactionTypeEnum.Withdraw)
            });
            var interestEarned = account.InterestEarned();

            Assert.AreEqual(interestEarned, 3.00);

            account.Transactions.Add(new Transaction(interestEarned, DateTime.Now, TransactionTypeEnum.Deposit));
            var totalBalance = account.SumTransactions();

            Assert.AreEqual(account.SumTransactions(), totalBalance);
        }
    }
}