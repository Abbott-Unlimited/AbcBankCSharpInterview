﻿using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
         Customer john = new Customer("John");
         john.OpenAccount(new Account(Account.AccountTypes.CHECKING));
         bank.AddCustomer(john);

         Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
      }
      //These tests are no longer valid, interest is accrued daily. These were based on annual interest at the current rates.
      //Replaced by the CustomerTest (GetInterestEarned)
      //[TestMethod]
      //public void CheckingAccount()
      //{
      //   Bank bank = new Bank();
      //   Account checkingAccount = new Account(Account.CHECKING);
      //   Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
      //   bank.AddCustomer(bill);

      //   checkingAccount.Deposit(100.0);

      //   Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
      //}

      //[TestMethod]
      //public void Savings_account()
      //{
      //   Bank bank = new Bank();
      //   Account checkingAccount = new Account(Account.SAVINGS);
      //   bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

      //   checkingAccount.Deposit(1500.0);

      //   Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
      //}

      //[TestMethod]
      //public void Maxi_savings_account()
      //{
      //   Bank bank = new Bank();
      //   Account checkingAccount = new Account(Account.MAXI_SAVINGS);
      //   bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

      //   checkingAccount.Deposit(3000.0);

      //   Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
      //}
   }
}