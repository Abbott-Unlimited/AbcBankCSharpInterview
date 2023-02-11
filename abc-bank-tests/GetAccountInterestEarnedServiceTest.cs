using abc_bank;
using abc_bank.Services.AccountServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Services.DateServices;

namespace abc_bank_tests
{
    [TestClass]
    public class GetAccountInterestEarnedServiceTest
    {
        private DateTime currentDate;
        private GetAccountInterestedEarnedService getAccountInterestedEarnedService;
        private AddTransactionToAccountService addTransactionToAccountService;

        [TestInitialize]
        public void Initialize()
        {
            this.currentDate = DateTime.Now;

            var mockGetCurrentDateService = new Mock<IGetCurrentDateService>();

            mockGetCurrentDateService.Setup(x => x.Get()).Returns(() => this.currentDate);

            this.getAccountInterestedEarnedService = new GetAccountInterestedEarnedService(mockGetCurrentDateService.Object);
            this.addTransactionToAccountService = new AddTransactionToAccountService();
        }

        [TestMethod]
        public void Get_StubACheckingAccountWithNoTransactions_VerifyResults()
        {
            // Arrange
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(checkingAccount);

            // Assert
            Assert.AreEqual(0, interestEarned);
        }

        [TestMethod]
        public void Get_StubASavingsAccountWithNoTransactions_VerifyResults()
        {
            // Arrange
            var savingsAccount = new Account(AccountType.SAVINGS, "John Doe Checking Account");

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(savingsAccount);

            // Assert
            Assert.AreEqual(0, interestEarned);
        }

        [TestMethod]
        public void Get_StubAMaxiSavingsAccountWithNoTransactions_VerifyResults()
        {
            // Arrange
            var maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, "John Doe Checking Account");

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(maxiSavingsAccount);

            // Assert
            Assert.AreEqual(0, interestEarned);
        }

        [TestMethod]
        public void Get_StubACheckingAccountWithANegativeTransactionTotal_VerifyResults()
        {
            // Arrange
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(checkingAccount, TransactionType.WITHDRAWAL, -50);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(checkingAccount);

            // Assert
            Assert.AreEqual(0, interestEarned);
        }

        [TestMethod]
        public void Get_StubASavingsAccountWithANegativeTransactionTotal_VerifyResults()
        {
            // Arrange
            var savingsAccount = new Account(AccountType.SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.WITHDRAWAL, -50);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(savingsAccount);

            // Assert
            Assert.AreEqual(0, interestEarned);
        }

        [TestMethod]
        public void Get_StubAMaxiSavingsAccountWithANegativeTransactionTotal_VerifyResults()
        {
            // Arrange
            var maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.WITHDRAWAL, -50);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(maxiSavingsAccount);

            // Assert
            Assert.AreEqual(0, interestEarned);
        }

        [TestMethod]
        public void Get_StubACheckingAccountWithAPositiveTransactionTotal_VerifyResults()
        {
            // Arrange
            var checkingAccount = new Account(AccountType.CHECKING, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(checkingAccount, TransactionType.DEPOSIT, 50);
            this.addTransactionToAccountService.Add(checkingAccount, TransactionType.WITHDRAWAL, -25);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(checkingAccount);

            // Assert
            Assert.AreEqual(0.025m, interestEarned);
        }

        [TestMethod]
        public void Get_StubASavingsAccountWithAPositiveTransactionTotalLessThan1000_VerifyResults()
        {
            // Arrange
            var savingsAccount = new Account(AccountType.SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.DEPOSIT, 1000);
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.WITHDRAWAL, -1);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(savingsAccount);

            // Assert
            Assert.AreEqual(.999m, interestEarned);
        }

        [TestMethod]
        public void Get_StubASavingsAccountWithAPositiveTransactionOf1000_VerifyResults()
        {
            // Arrange
            var savingsAccount = new Account(AccountType.SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.DEPOSIT, 2000);
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.WITHDRAWAL, -1000);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(savingsAccount);

            // Assert
            Assert.AreEqual(1m, interestEarned);
        }

        [TestMethod]
        public void Get_StubASavingsAccountWithAPositiveTransactionTotalGreaterThan1000_VerifyResults()
        {
            // Arrange
            var savingsAccount = new Account(AccountType.SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.DEPOSIT, 2000);
            this.addTransactionToAccountService.Add(savingsAccount, TransactionType.WITHDRAWAL, -500);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(savingsAccount);

            // Assert
            Assert.AreEqual(2m, interestEarned);
        }

        [TestMethod]
        public void Get_StubAMaxiSavingsAccountWithAPositiveTransactionTotalAndOneWithdrawalNotWithinTheLast10Days_VerifyResults()
        {
            // Arrange
            var maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, "John Doe Checking Account");

            var currentDate = DateTime.Now;
            var secondsIn10DaysPlus1 = 60 * 60 * 24 * 10 + 1;
            var currentDateMinus10DaysAndOneSecond = currentDate.AddSeconds(-secondsIn10DaysPlus1);

            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.DEPOSIT, 2000);
            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.WITHDRAWAL, -500, currentDateMinus10DaysAndOneSecond);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(maxiSavingsAccount);

            // Assert
            Assert.AreEqual(75m, interestEarned);
        }

        [TestMethod]
        public void Get_StubAMaxiSavingsAccountWithAPositiveTransactionTotalAndOneWithdrawalsExactly10DaysAgo_VerifyResults()
        {
            // Arrange
            var maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.DEPOSIT, 2000);

            var currentDate = DateTime.Now;
            var secondsIn10Day = 60 * 60 * 24 * 10;
            var currentDateMinus10DaysAndOneSecond = currentDate.AddSeconds(-secondsIn10Day);

            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.WITHDRAWAL, -500, currentDateMinus10DaysAndOneSecond);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(maxiSavingsAccount);

            // Assert
            Assert.AreEqual(15m, interestEarned);
        }

        [TestMethod]
        public void Get_StubAMaxiSavingsAccountWithAPositiveTransactionTotalAndOneWithdrawalsWithinTheLast10Days_VerifyResults()
        {
            // Arrange
            var maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, "John Doe Checking Account");
            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.DEPOSIT, 2000);

            var currentDate = DateTime.Now;
            var secondsIn10DayMinus1Second = 60 * 60 * 24 * 10 -1;
            var currentDateMinus10DaysAndOneSecond = currentDate.AddSeconds(-secondsIn10DayMinus1Second);

            this.addTransactionToAccountService.Add(maxiSavingsAccount, TransactionType.WITHDRAWAL, -500, currentDateMinus10DaysAndOneSecond);

            // Act
            var interestEarned = this.getAccountInterestedEarnedService.Get(maxiSavingsAccount);

            // Assert
            Assert.AreEqual(15m, interestEarned);
        }
    }
}
