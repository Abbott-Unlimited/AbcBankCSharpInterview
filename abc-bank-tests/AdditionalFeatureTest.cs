using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class AdditionalFeatureTest
    {
        private static readonly double DOUBLE_DELTA = 1e-2;
        private Account checkingAccount;
        private Account savingsAccount;
        private Account maxiSavingsAccount;
        private Customer customer;

        [TestInitialize]
        public void Setup()
        {
            // Initialize customer and accounts then open accounts and make deposits/withdrawals 
            customer = new Customer("Henry");

            checkingAccount = new Account(AccountType.CHECKING, customer);
            savingsAccount = new Account(AccountType.SAVINGS, customer);
            maxiSavingsAccount = new Account(AccountType.MAXI_SAVINGS, customer);

            customer.OpenAccount(checkingAccount).OpenAccount(savingsAccount);
            checkingAccount.Deposit(100.0);
            savingsAccount.Deposit(4000.0);
            savingsAccount.Withdraw(200.0);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Amount must be greater than zero")]
        public void TestTransferANegativeNumber()
        {
            try
            {
                checkingAccount.Transfer(checkingAccount, -1);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Amount must be greater than zero", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Your current balance is less than the amount you want to transfer")]
        public void TestTransferMoreThenInAccount()
        {
            try
            {
                checkingAccount.Transfer(checkingAccount, 120);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Your current balance is less than the amount you want to transfer", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "You can only transfer between your accounts.")]
        public void TestTransferOutsideAccount()
        {

            var customer2 = new Customer("Henry2");
            var customer2Account = new Account(AccountType.MAXI_SAVINGS, customer2);
            customer2.OpenAccount(customer2Account);
            customer2Account.Deposit(200);
            try
            {
                checkingAccount.Transfer(customer2Account, 40);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("You can only transfer between your accounts", ex.Message);
                throw;
            }
        }

        #region "Feature 1"
        [TestMethod]
        public void TestTransfer()
        {
            checkingAccount.Transfer(savingsAccount, 80);

            Assert.AreEqual(checkingAccount.CurrentBalance, 20);
            Assert.AreEqual(savingsAccount.CurrentBalance, 3880);
        }

        #endregion

        [TestMethod]
        public void TestCompoundInterest()
        {
            var bank = new Bank();
            customer = new Customer("John");
            customer.OpenAccount(checkingAccount);
            bank.AddCustomer(customer);
            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.20, CalculateTotalWithCompoundInterest(200, 0.001, 365, 1), DOUBLE_DELTA);
        }


        #region "Feature 2"

        [TestMethod]
        public void TestMaxiSavingsAccount()
        {
            var bank = new Bank();
            customer = new Customer("Bill");
            bank.AddCustomer(customer.OpenAccount(maxiSavingsAccount));
            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Deposit(100.0);
            maxiSavingsAccount.Withdraw(100.0);

            Assert.AreEqual(153.80, bank.TotalInterestPaid(DateTime.Now.AddYears(1)), DOUBLE_DELTA);
        }

        [TestMethod]
        public void TestMaxiSavingsAccountNineDays()
        {
            var bank = new Bank();
            customer = new Customer("Bill");
            bank.AddCustomer(customer.OpenAccount(maxiSavingsAccount));
            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Deposit(100.0);
            maxiSavingsAccount.Withdraw(100.0);

            Assert.AreEqual(0.07, bank.TotalInterestPaid(DateTime.Now.AddDays(9)), DOUBLE_DELTA);
        }
        #endregion


        private static double CalculateTotalWithCompoundInterest(double principal, double interestRate, int compoundingPeriodsPerYear, double yearCount)
        {
            return principal * (double)Math.Pow((double)(1 + interestRate / compoundingPeriodsPerYear), compoundingPeriodsPerYear * yearCount) - principal;
        }

    }
}
