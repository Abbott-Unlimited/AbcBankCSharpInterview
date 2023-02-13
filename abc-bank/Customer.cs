using System;
using System.Collections.Generic;

using abc_bank.Accounts;

namespace abc_bank {
  public class Customer {

    #region Properties

    public int Id { get; }

    public string Name { get; }

    public List<IAccount> Accounts { get; protected set; } = new List<IAccount>();

    public int NumberOfAccounts {
      get => Accounts.Count;
    }

    public string Statement => abc_bank.Reports.Statements.CustomerStatement(this);

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

    public Customer(string name, int lastCustomerId) {
      Id = lastCustomerId + 1;
      Name = name;
    }

    #endregion

    #region Public Methods

    public Customer OpenAccount(IAccount account) {
      Accounts.Add(account);

      return this;
    }

    public uint TransferFunds(uint fromAccountId, uint toAccountId, double amount) => throw new NotImplementedException();

    #endregion

  }
}
