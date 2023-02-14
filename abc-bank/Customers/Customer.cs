using System;
using System.Collections.Generic;
using System.Linq;

using abc_bank.Accounts;
using abc_bank.Reports;
using abc_bank.Transactions;
using abc_bank.Utilities;

namespace abc_bank.Customers {
  public class Customer {

    #region Properties  

    public int Id { get; } = -1;

    public string Name { get; }

    public List<IAccount> Accounts { get; protected set; } = new List<IAccount>();

    public int NumberOfAccounts => Accounts.Count;

    public string Statement => Statements.CustomerStatement(this);

    public decimal TotalInterestEarned {
      get {
        decimal total = 0;

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

    public void TransferFunds(decimal amount, DateTime transactionDate, IAccount originAccount, IAccount destinationAccount) {
      Validators.AccountIsInCollection(originAccount, Accounts);
      Validators.AccountIsInCollection(destinationAccount, Accounts);

      new Transfer(amount, transactionDate, originAccount, destinationAccount);
    }

    #endregion   

    public IAccount GetAccountById(int accountId) => Accounts.Find(acct => acct.Id == accountId);

    public IList<IAccount> GetAccountsByType(AccountType acctType) => Accounts.Where(acct => acct.AccountType == acctType).ToList();

  }
}
