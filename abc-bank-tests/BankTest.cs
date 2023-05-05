using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John")
                .OpenAccount(new Account(Account.CHECKING));
            Customer joe = new Customer("Joe")
                .OpenAccount(new Account(Account.CHECKING))
                .OpenAccount(new Account(Account.SAVINGS))
                .OpenAccount(new Account(Account.MAXI_SAVINGS));

            bank.AddCustomer(john);
            bank.AddCustomer(joe);

            string expectedCustomerSummary = "Customer Summary" +
                                        "\n - John (1 account)" +
                                        "\n - Joe (3 accounts)";

            string actualCustomerSummary = bank.CustomerSummary();

            Assert.AreEqual(expectedCustomerSummary, actualCustomerSummary);
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            bank.AddCustomer(new Customer("Bill")
                .OpenAccount(checkingAccount));

            checkingAccount.Deposit(100.0);

            double expectedInterestOneYear = 0.1/365;
            double actualInterest = bank.TotalInterestPaidForOneDayAllAccounts();

            Assert.AreEqual(expectedInterestOneYear, actualInterest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void SavingsAccount() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill")
                .OpenAccount(savingsAccount));

            savingsAccount.Deposit(1500.0);

            double expectedInterestOneYear = 2.0/365;
            double actualInterest = bank.TotalInterestPaidForOneDayAllAccounts();

            Assert.AreEqual(expectedInterestOneYear, actualInterest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccountNoWithdrawal() {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill")
                .OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0);

            double expectedInterestOneYear = 150.0/365;
            double actualInterest = bank.TotalInterestPaidForOneDayAllAccounts();

            Assert.AreEqual(expectedInterestOneYear, actualInterest, DOUBLE_DELTA);
        }

        [TestMethod]
        public void MaxiSavingsAccountWithWithdrawal()
        {
            Bank bank = new Bank();
            Account maxiSavingsAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill")
                .OpenAccount(maxiSavingsAccount));

            maxiSavingsAccount.Deposit(3000.0);
            maxiSavingsAccount.Withdraw(150.0);

            double expectedInterestOneYear = 28.5/365;
            double actualInterest = bank.TotalInterestPaidForOneDayAllAccounts();

            Assert.AreEqual(expectedInterestOneYear, actualInterest, DOUBLE_DELTA);
        }
    }
}
