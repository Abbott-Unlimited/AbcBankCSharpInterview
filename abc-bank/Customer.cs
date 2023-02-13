using System;
using System.Collections.Generic;

using abc_bank.Accounts;
using abc_bank.Transactions;
using abc_bank.Reports;

namespace abc_bank {
  public class Customer {

    #region Properties  

    public int Id { get; } = -1;

    public string Name { get; }

    public List<IAccount> Accounts { get; protected set; } = new List<IAccount>();

    public int NumberOfAccounts => Accounts.Count;

    public string Statement => Statements.CustomerStatement(this);

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

    public Customer(string name, int customerId) {
      Id = customerId;
      Name = name;
    }

    #endregion

    #region Public Methods

    public Customer OpenAccount(IAccount account) {
      Accounts.Add(account);

      return this;
    }

    public void TransferFunds(double amount, DateTime transactionDate, IAccount originAccount, IAccount destinationAccount) {
      Utilities.ValidateAccountInCollection(originAccount, Accounts);
      Utilities.ValidateAccountInCollection(destinationAccount, Accounts);
      var transfer = new Transfer(amount, transactionDate, originAccount, destinationAccount);
    }

    #endregion

  }
}
