using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
   [TestClass]
   public class AccountTests
   {
      [TestMethod]
      public void TestInterestRateCalcs()
      {
         Account checkingAccount1 = new Account(Account.AccountTypes.CHECKING);
         checkingAccount1.Deposit(1000);
         // Passing 1 simulates an annual interest
         double checkingInterest = checkingAccount1.CalculateCompoundingPeriodInterest(checkingAccount1.sumTransactions(), 1);
         Assert.AreEqual(1.00, Math.Round(checkingInterest, 2), "Checking Interest");

         Account savingsAccount1 = new Account(Account.AccountTypes.SAVINGS);
         savingsAccount1.Deposit(500);
         double savingsInterest = savingsAccount1.CalculateCompoundingPeriodInterest(savingsAccount1.sumTransactions(), 1);
         Assert.AreEqual(0.50, Math.Round(savingsInterest, 2), "Savings Below $1000 Interest");
         savingsAccount1.Deposit(500);
         savingsInterest = savingsAccount1.CalculateCompoundingPeriodInterest(savingsAccount1.sumTransactions(), 1);
         Assert.AreEqual(1.00, Math.Round(savingsInterest, 2), "Savings Equals $1000 Interest");
         savingsAccount1.Deposit(1000);
         savingsInterest = savingsAccount1.CalculateCompoundingPeriodInterest(savingsAccount1.sumTransactions(), 1);
         Assert.AreEqual(3.00, Math.Round(savingsInterest, 2), "Savings > $1000 Interest");

         Account maxiAccount1 = new Account(Account.AccountTypes.MAXI_SAVINGS);
         maxiAccount1.Deposit(1000);
         double maxiInterest = maxiAccount1.CalculateCompoundingPeriodInterest(maxiAccount1.sumTransactions(), 1);
         Assert.AreEqual(50.00, Math.Round(maxiInterest, 2), "Maxi Savings no recent withdrawals Interest");
         maxiAccount1.transactions.Add(new Transaction(-200, DateTime.Now.AddDays(-9)));
         maxiInterest = maxiAccount1.CalculateCompoundingPeriodInterest(maxiAccount1.sumTransactions(), 1);
         Assert.AreEqual(0.80, Math.Round(maxiInterest, 2), "Maxi Savings recent withdrawals Interest");
      }

      [TestMethod]
      public void TestOverdraft()
      {
         Account checkingAccount1 = new Account(Account.AccountTypes.CHECKING);

         try
         {
            checkingAccount1.Deposit(1000);
            checkingAccount1.Withdraw(2000);
         }
         catch (Exception ex)
         {
            Assert.AreEqual($"Insufficient funds in {checkingAccount1.GetAccountType()} account", ex.Message);
         }

      }
   }
}