using abc_bank;
using abc_bank.Services.AccountServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank_tests
{
    [TestClass]
    public class AddTransactionToAccountServiceTest
    {
        private AddTransactionToAccountService addTransactionToAccountService;

        [TestInitialize]
        public void Initialize()
        {
            this.addTransactionToAccountService = new AddTransactionToAccountService();
        }

        [TestMethod]
        public void Add_StubADepositTransactionTypeAndANegativeAmount_VerifyExceptionThrown()
        {
            // Arrange
            var account = new Account(AccountType.CHECKING, "John Doe Checking Account");
            var depositTransactionType = TransactionType.DEPOSIT;
            var negativeAmount = -10m;

            // Act
            try
            {
                this.addTransactionToAccountService.Add(account, depositTransactionType, negativeAmount);
                Assert.Fail("An exception should have been thronw.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("You cannot deposit a non-positive amount.", ex.Message);
            }
        }

        [TestMethod]
        public void Add_StubAWithdrawalTransactionTypeAndAPositiveAmount_VerifyExceptionThrown()
        {
            // Arrange
            var account = new Account(AccountType.CHECKING, "John Doe Checking Account");
            var depositTransactionType = TransactionType.WITHDRAWAL;
            var negativeAmount = 10m;

            // Act
            try
            {
                this.addTransactionToAccountService.Add(account, depositTransactionType, negativeAmount);
                Assert.Fail("An exception should have been thronw.");
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual("You cannot withdraw a non-negative amount.", ex.Message);
            }
        }

        [TestMethod]
        public void Add_StubADepositTransactionTypeAndAPositiveAmount_VerifyResults()
        {
            // Arrange
            var account = new Account(AccountType.CHECKING, "John Doe Checking Account");
            var depositTransactionType = TransactionType.DEPOSIT;
            var amount = 10m;

            // Act

            this.addTransactionToAccountService.Add(account, depositTransactionType, amount);

            // Assert
            Assert.AreEqual(account.transactions.Sum(x => x.amount), 10m);
        }

        [TestMethod]
        public void Add_StubAWithdrawalTransactionTypeAndANegativeAmount_VerifyResults()
        {
            // Arrange
            var account = new Account(AccountType.CHECKING, "John Doe Checking Account");
            var depositTransactionType = TransactionType.WITHDRAWAL;
            var amount = -10m;

            // Act

            this.addTransactionToAccountService.Add(account, depositTransactionType, amount);

            // Assert
            Assert.AreEqual(account.transactions.Sum(x => x.amount), -10m);
        }
    }
}
