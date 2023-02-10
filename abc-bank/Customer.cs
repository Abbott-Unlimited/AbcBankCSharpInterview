using System;
using System.Collections.Generic;

using abc_bank.Accounts;
using abc_bank.Transactions;

namespace abc_bank {
  public class Customer {

    #region Properties

    public string Name { get; }

    public List<IAccount> Accounts { get; protected set; }

    public int NumberOfAccounts {
      get {
        return Accounts.Count;
      }
    }

    public string Statement {
      get {
        var statement = "Statement for " + Name + "\n";
        var total = 0.0;

        foreach (var account in Accounts) {
          statement += "\n" + StatementForAccount(account) + "\n";
          total += account.CurrentBalance;
        }

        statement += "\nTotal In All Accounts " + ToDollars(total);

        return statement;
      }
    }

    public double TotalInterestEarned {
      get {
        var total = 0.0;

        foreach (IAccount account in Accounts) {
          total += account.InterestEarned;
        }

        return total;
      }
    }

    #endregion

    #region CTOR

    public Customer(string name) {
      Name = name;
      Accounts = new List<IAccount>();
    }

    #endregion

    #region Public Methods

    public Customer OpenAccount(IAccount account) {
      Accounts.Add(account);
      return this;
    }

    public Customer OpenAccount(AccountType accountType, double initialDeposit = 0.00) {
      return OpenAccount(AccountCreator.GetAccount(accountType, initialDeposit));
    }

    public void TransferFunds() => throw new NotImplementedException;

    #endregion

    #region Protected Methods

    protected string StatementForAccount(IAccount account) {
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
        s += "  " + (t.Amount < 0 ? "withdrawal" : "deposit") + " " + ToDollars(t.Amount) + "\n";
        total += t.Amount;
      }

      s += "Total " + ToDollars(total);

      return s;
    }

    protected string ToDollars(double d) => string.Format("{0:C2}", Math.Abs(d));

    #endregion

  }
}
