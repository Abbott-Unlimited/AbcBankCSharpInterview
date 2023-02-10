using System.Collections.Generic;

using abc_bank.Exceptions;

namespace abc_bank.Accounts {
  public abstract class AccountBase : IAccount {

    public virtual AccountType AccountType { get; }

    public virtual double CurrentBalance {
      get {
        var amount = 0.0;

        foreach (Transaction t in Transactions) {
          amount += t.amount;
        }

        return amount;
      }
    }

    public virtual List<Transaction> Transactions { get; protected set; }

    public virtual bool HasTransactions {
      get {
        return Transactions.Count > 0 ? true : false;
      }
    }

    public abstract double InterestEarned { get; }

    protected AccountBase(AccountType accountType) {
      AccountType = accountType;
      Transactions = new List<Transaction>();
    }

    protected double CalculateInterest(double interestRate) {
      return CurrentBalance * interestRate;
    }

    public void Deposit(double amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      } else {
        Transactions.Add(new Transaction(amount));
      }
    }

    public void Withdraw(double amount) {
      if (amount <= 0) {
        throw new InvalidTransactionAmountException();
      } else if (amount > CurrentBalance) {
        throw new InsufficientFundsException();
      } else {
        Transactions.Add(new Transaction(-amount));
      }
    }
  }
}
