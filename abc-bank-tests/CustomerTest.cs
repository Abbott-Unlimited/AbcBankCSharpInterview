using abc_bank;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace abc_bank_tests
{
   [TestClass]
   public class CustomerTest
   {
      [TestMethod]
      public void TestApp()
      {
         Account checkingAccount = new Account(Account.AccountTypes.CHECKING);
         Account savingsAccount = new Account(Account.AccountTypes.SAVINGS);

         Customer henry = new Customer("Henry").OpenAccount(checkingAccount).OpenAccount(savingsAccount);

         checkingAccount.Deposit(100.0);
         savingsAccount.Deposit(4000.0);
         savingsAccount.Withdraw(200.0);
         string sTestValue = "Statement for Henry\n" +
                 "\n" +
                 "Checking Account\n" +
                 "  deposit $100.00\n" +
                 "Total $100.00\n" +
                 "\n" +
                 "Savings Account\n" +
                 "  deposit $4,000.00\n" +
                 "  withdrawal ($200.00)\n" +
                 "Total $3,800.00\n" +
                 "\n" +
                 "Total In All Accounts $3,900.00";
         Assert.AreEqual(sTestValue, henry.GetStatement());
      }

      [TestMethod]
      public void TestOneAccount()
      {
         Customer oscar = new Customer("Oscar").OpenAccount(new Account(Account.AccountTypes.SAVINGS));
         Assert.AreEqual(1, oscar.GetNumberOfAccounts());
      }

      [TestMethod]
      public void TestTwoAccount()
      {
         Customer oscar = new Customer("Oscar")
              .OpenAccount(new Account(Account.AccountTypes.SAVINGS));
         oscar.OpenAccount(new Account(Account.AccountTypes.CHECKING));
         Assert.AreEqual(2, oscar.GetNumberOfAccounts());
      }

      [TestMethod]
      [Ignore]
      public void TestThreeAccounts()
      {
         Customer oscar = new Customer("Oscar");
         oscar.OpenAccount(new Account(Account.AccountTypes.SAVINGS));
         oscar.OpenAccount(new Account(Account.AccountTypes.CHECKING));
         Assert.AreEqual(3, oscar.GetNumberOfAccounts());
      }
      /// <summary>
      /// Verify the interest calculaitons for the accrued interest.  Tested against excel future value function
      /// Note: It appears that the accrued interest calculation is not correct, it shows how I handled it, not
      /// sure why the method is not correct.  
      /// </summary>
      [TestMethod]
      public void GetInterestEarned()
      {
         Customer oscar = new Customer("Oscar");
         oscar.OpenAccount(new Account(Account.AccountTypes.SAVINGS));
         oscar.OpenAccount(new Account(Account.AccountTypes.CHECKING));
         oscar.OpenAccount(new Account(Account.AccountTypes.MAXI_SAVINGS));
         double runningTotalInterest = 0.0;
         foreach (Account account in oscar.accounts)
         {
            Transaction trans1 = new Transaction(500, DateTime.Now.AddYears(-1));
            account.transactions.Add(trans1);
            switch (account.GetAccountType())
            {
               case Account.AccountTypes.SAVINGS:
                  double savingsInterest = account.InterestEarned();
                  Assert.AreEqual(0.50, Math.Round(savingsInterest, 2), "Savings Interest Calc");
                  runningTotalInterest += savingsInterest;
                  break;
               case Account.AccountTypes.CHECKING:
                  double checkingInterest = account.InterestEarned();
                  Assert.AreEqual(0.50, Math.Round(checkingInterest, 2), "Checking Interest Calc");
                  runningTotalInterest += checkingInterest;
                  break;
               case Account.AccountTypes.MAXI_SAVINGS:
                  double maxiSavingsEarned = account.InterestEarned();
                  Assert.AreEqual(25.63, Math.Round(maxiSavingsEarned, 2), "Maxi Savings Interest Calc");
                  runningTotalInterest += maxiSavingsEarned;
                  break;

            }
         }

         double totalInterest = oscar.TotalInterestEarned();
         Assert.AreEqual(runningTotalInterest, totalInterest, "Verify Total Interest Earned");
      }
   }
}