using System;
using System.Collections.Generic;
using System.Globalization;

namespace abc_bank
{
   public class Customer
   {
      private String name;
      public List<Account> accounts;

      public Customer(String name)
      {
         this.name = name;
         this.accounts = new List<Account>();
      }

      public String GetName()
      {
         return name;
      }

      public Customer OpenAccount(Account account)
      {
         accounts.Add(account);
         return this;
      }

      public void Transfer(Account fromAccount, Account toAccount, double amount)
      {
         if (amount < 0)
         {
            throw new ArgumentException("Transfer amount must be positive");
         }
         else
         {
            fromAccount.Withdraw(amount);
            toAccount.Deposit(amount);
         }
      }

      public int GetNumberOfAccounts()
      {
         return accounts.Count;
      }

      public double TotalInterestEarned()
      {
         double total = 0;
         foreach (Account a in accounts)
            total += a.InterestEarned();
         return total;
      }

      public String GetStatement()
      {
         String statement = null;
         statement = $"Statement for {name}\n";
         double total = 0.0;
         foreach (Account a in accounts)
         {
            statement += $"\n{statementForAccount(a)}\n";
            total += a.sumTransactions();
         }
         statement += $"\nTotal In All Accounts {ToDollars(total)}";
         return statement;
      }

      private String statementForAccount(Account a)
      {
         String s = "";

         //Translate to pretty account type
         switch (a.GetAccountType())
         {
            case Account.AccountTypes.CHECKING:
               s += "Checking Account\n";
               break;

            case Account.AccountTypes.SAVINGS:
               s += "Savings Account\n";
               break;

            case Account.AccountTypes.MAXI_SAVINGS:
               s += "Maxi Savings Account\n";
               break;
         }

         //Now total up all the transactions
         double total = 0.0;
         foreach (Transaction t in a.transactions)
         {
            s += $"  {(t.amount < 0 ? "withdrawal" : "deposit")} {ToDollars(t.amount)}\n";
            total += t.amount;
         }
         s += "Total " + ToDollars(total);
         return s;
      }

      private String ToDollars(double d)
      {
         return d.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));
      }
   }
}