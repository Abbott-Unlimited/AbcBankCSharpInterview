using System;
using System.Collections.Generic;

namespace abc_bank {
  public class Customer {
    #region Properties

    public string Name { get; }

    private List<Account> accounts;
    public int NumberOfAccounts {
      get {
        return accounts.Count;
      }
    }

    public string Statement {
      get {
        var statement = "Statement for " + Name + "\n";
        var total = 0.0;

        foreach (Account a in accounts) {
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

        foreach (Account a in accounts) {
          total += a.InterestEarned;
        }

        return total;
      }
    }

    #endregion

    #region CTOR

    public Customer(string name) {
      Name = name;
      accounts = new List<Account>();
    }

    #endregion

    #region Public Methods

    // TODO:  Examine the following much closer...
    public Customer OpenAccount(Account account) {
      accounts.Add(account);
      return this;
    }

    #endregion

    #region Private Methods

    private string StatementForAccount(Account account) {
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
