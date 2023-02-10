using System;
using System.Collections.Generic;
using System.Diagnostics;

using abc_bank.Accounts;

namespace abc_bank {
  public class Customer {

    #region Properties

    public string Name { get; }

    private List<IAccount> accounts;
    public int NumberOfAccounts {
      get {
        return accounts.Count;
      }
    }

    public string Statement {
      get {
        var statement = "Statement for " + Name + "\n";
        var total = 0.0;

        foreach (var a in accounts) {
          statement += "\n" + StatementForAccount(a) + "\n";
          total += a.CurrentBalance;
        }

        statement += "\nTotal In All Accounts " + ToDollars(total);
        
        return statement;
      }
    }

    public double TotalInterestEarned {
      get {
        var total = 0.0;

        foreach (IAccount a in accounts) {
          total += a.InterestEarned;
        }

        return total;
      }
    }

    #endregion

    #region CTOR

    public Customer(string name) {
      Name = name;
      accounts = new List<IAccount>();
    }

    #endregion

    #region Public Methods

    public Customer OpenAccount(IAccount account) {
      accounts.Add(account);
      return this;
    }

    public Customer OpenAccount(AccountType accountType) {
      return OpenAccount(AccountCreator.GetAccount(accountType));
    }

    public Customer OpenAccount(AccountType accountType, double initialDeposit) {
      return OpenAccount(AccountCreator.GetAccount(accountType, initialDeposit));
    }

    #endregion

    #region Private Methods

    private string StatementForAccount(IAccount account) {
      string s = "";

      //Translate to pretty account type
      switch (account.AccountType) {
        case AccountType.CHECKING:
          s += "Checking Account\n";
          break;
        case AccountType.SAVINGS:
          s += "Savings Account\n";
          break;
        case AccountType.MAXI_SAVINGS:
          s += "Maxi Savings Account\n";
          break;
      }

      //Now total up all the transactions
      var total = 0.0;

      foreach (Transaction t in account.Transactions) {
        s += "  " + (t.amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.amount) + "\n";
        total += t.amount;
      }

      s += "Total " + ToDollars(total);

      return s;
    }

    private string ToDollars(double d) => string.Format("{0:C2}", Math.Abs(d));

    #endregion

  }
}
