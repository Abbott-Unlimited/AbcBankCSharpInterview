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

    public AccountBase(AccountType accountType, int accountId, int customerId, double initialDeposit = 0.00) {
      if (initialDeposit > 0.00) {
        Deposit(initialDeposit);
      }

      AccountType = accountType;
      Id = accountId;
      CustomerId = customerId;
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

    public bool Deposit(ITransfer transfer) {
      try {
        var deposit = new Deposit(transfer.Amount, DateTime.Now, this, transfer);
        Transactions.Add(deposit);
      } catch (Exception e) {
        // real-world implementation, there would be something logging the caught error here.
        // also wouldn't throw the exception - would be handled differently.
        throw new Exception(e.Message);
      }

      return true;
    }

    public bool Withdraw(ITransfer transfer) {
      try {
        var withdraw = new Withdraw(transfer.Amount, DateTime.Now, this, transfer);

        Transactions.Add(withdraw);
      } catch (Exception e) {
        // real-world implementation, there would be something logging the caught error here.
        // also wouldn't throw the exception - would be handled differently.
        throw new Exception(e.Message);
      }

      return true;
    }

    #endregion

  }
}
