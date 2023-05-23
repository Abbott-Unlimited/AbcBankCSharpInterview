using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;
        private static DateTime dateToday = DateProvider.getInstance().Now();
        private static int currentYear = dateToday.Year;
        private static int daysInYear = DateTime.IsLeapYear(currentYear) ? 366 : 365;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new Account(Account.CHECKING));
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new Account(Account.CHECKING);
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingsAccount = new Account(Account.SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingsAccount));

         savingsAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
      [Ignore]
        public void Maxi_savings_account() {
            Bank bank = new Bank();
            Account maxiAccount = new Account(Account.MAXI_SAVINGS);
            bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

            maxiAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

      [TestMethod]
      public void Maxi_savings_account_no_penalty()
      {
         // no withdrawals, 5% interest on all
         Bank bank = new Bank();
         Account maxiAccount = new Account(Account.MAXI_SAVINGS);
         bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

         maxiAccount.Deposit(3000.0);

         Assert.AreEqual(150.0, bank.totalInterestPaid(), DOUBLE_DELTA);
      }

      [TestMethod]
      public void Maxi_savings_account_current_year_penalty()
      {
         Bank bank = new Bank();
         Account maxiAccount = new Account(Account.MAXI_SAVINGS);
         bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

         maxiAccount.Deposit(3000.0, new DateTime(currentYear, 1, 1));
         maxiAccount.Withdraw(1000.0, new DateTime(currentYear, 1, 1));

         int regularInterestDays = daysInYear - 10;
         double regularInterest = (regularInterestDays * (0.05 / daysInYear) * 2000);
         double penaltyInterest = (10 * (0.001 / daysInYear) * 2000);

         Assert.AreEqual((regularInterest + penaltyInterest), bank.totalInterestPaid(), DOUBLE_DELTA);
      }

      [TestMethod]
      public void Maxi_savings_account_last_year_penalty()
      {
         Bank bank = new Bank();
         Account maxiAccount = new Account(Account.MAXI_SAVINGS);
         bank.AddCustomer(new Customer("Bill").OpenAccount(maxiAccount));

         maxiAccount.Deposit(3000.0, new DateTime(currentYear-1, 12, 31));
         maxiAccount.Withdraw(1000.0, new DateTime(currentYear-1, 12, 31));

         int regularInterestDays = daysInYear - 10;
         double regularInterest = (regularInterestDays * (0.05 / daysInYear) * 2000);
         double penaltyInterest = (10 * (0.001 / daysInYear) * 2000);

         Assert.AreEqual((regularInterest + penaltyInterest), bank.totalInterestPaid(), DOUBLE_DELTA);
      }
   }
}
