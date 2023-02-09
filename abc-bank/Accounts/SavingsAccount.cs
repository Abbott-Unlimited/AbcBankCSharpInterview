using System.Collections.Generic;

namespace abc_bank.Accounts {
  public class SavingsAccount : IAccount {
    #region Properties

    public AccountType AccountType { get; }

    public List<Transaction> Transactions { get; }

    public bool HasTransactions {
      get {
        return Transactions.Count > 0 ? true : false;
      }
    }

    public double CurrentBalance {
      get {
        var amount = 0.0;

        foreach (Transaction t in Transactions) {
          amount += t.amount;
        }

        return amount;
      }
    }

    public double InterestEarned {
      get {
        if (CurrentBalance <= 1000) {
          return CurrentBalance * 0.001;
        } else {
          return CurrentBalance * 0.002;
          //return 1 + (CurrentBalance - 1000) * 0.002; // not sure what this is supposed to be doing...
        }
      }
    }

    #endregion

    #region CTOR

    public SavingsAccount() {
      AccountType = AccountType.SAVINGS;
      Transactions = new List<Transaction>();
    }

    #endregion

    #region Public Methods

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


    #endregion
  }
}
