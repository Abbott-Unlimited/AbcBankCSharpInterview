using System.Collections.Generic;

using abc_bank.Exceptions;
using abc_bank.Transactions;

namespace abc_bank.Accounts {
  public abstract class AccountBase : IAccount {

    #region Properties

    public virtual AccountType AccountType { get; }

    public virtual double CurrentBalance {
      get {
        var amount = 0.0;

        foreach (Transaction t in Transactions) {
          amount += t.Amount;
        }

        return amount;
      }
    }

    public virtual List<Transaction> Transactions { get; protected set; }

    public virtual bool HasTransactions {
      get {
        return Transactions.Count > 0;
      }
    }

    public abstract double InterestEarned { get; }

    #endregion

    #region CTOR

    public AccountBase(AccountType accountType, double initialDeposit = 0.00) {
      AccountType = accountType;
      Transactions = new List<Transaction>();

      if (initialDeposit > 0.00) {
        Deposit(initialDeposit);
      }
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
