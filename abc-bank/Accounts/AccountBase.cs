using System;
using System.Collections.Generic;
using System.Linq;

using abc_bank.Transactions;

namespace abc_bank.Accounts {
  public abstract class AccountBase : IAccount {

    #region Properties  

    public virtual int Id { get; }

    public virtual int CustomerId { get; }

    public abstract string ReportLabel { get; }

    public virtual AccountType AccountType { get; }

    public abstract double InterestEarned { get; }

    public virtual List<ITransaction> Transactions { get; protected set; } = new List<ITransaction>();

    public virtual bool HasTransactions { get => Transactions.Count() > 0; }

    public virtual double CurrentBalance {
      get {
        var amount = 0.0;

        foreach (ITransaction t in Transactions) {
          amount += t.GetStatementAmount();
        }

        return amount;
      }
    }

    #endregion

    #region CTOR

    public AccountBase(AccountType accountType, int accountId, double initialDeposit = 0.00) {
      if (initialDeposit > 0.00) {
        Deposit(initialDeposit);
      }

      Id = accountId + 1;
      AccountType = accountType;
    }

    #endregion

    #region Methods

    protected double CalculateInterest(double interestRate) {
      return CurrentBalance * interestRate;
    }

    public IDeposit Deposit(double amount) {
      var depositTransaction = new Deposit(amount, DateTime.Now, this);
      Transactions.Add(depositTransaction);

      return depositTransaction;
    }


    public IWithdraw Withdraw(double amount) {
      var withdrawTransaction = new Withdraw(amount, DateTime.Now, this);

      Transactions.Add(withdrawTransaction);
      return withdrawTransaction;
    }


    #endregion

  }
}
