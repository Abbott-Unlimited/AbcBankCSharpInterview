using System;
using System.Collections.Generic;
using System.Linq;

using abc_bank.Exceptions;
using abc_bank.Transactions;

namespace abc_bank.Accounts {
  public abstract class AccountBase : IAccount {

    #region Properties  

    public virtual int Id { get; }

    public virtual int CustomerId { get; }

    public virtual AccountType AccountType { get; }

    public abstract double InterestEarned { get; }

    public virtual List<ITransaction> Transactions { get; protected set; } = new List<ITransaction>();

    public virtual bool HasTransactions { get => Transactions.Count() > 0; }

    public virtual double CurrentBalance {
      get {
        var amount = 0.0;

        foreach (ITransaction t in Transactions) {
          amount += t.Amount;
        }

        return amount;
      }
    }

    #endregion

    #region CTOR

    public AccountBase(AccountType accountType, int lastAccountId, double initialDeposit = 0.00) {
      if (initialDeposit > 0.00) {
        Deposit(initialDeposit);
      }

      Id = lastAccountId + 1;
      AccountType = accountType;
    }

    #endregion

    #region Methods

    protected double CalculateInterest(double interestRate) {
      return CurrentBalance * interestRate;
    }

    public void Deposit(double amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      } else {
        Transactions.Add(new Transaction(amount, TransactionType.Deposit));
      }
    }

    public void Withdraw(double amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      } else if (amount > CurrentBalance) {
        throw new InsufficientFundsException();
      } else {
        Transactions.Add(new Transaction(-amount, TransactionType.Withdraw));
      }
    }

    #endregion

  }
}
