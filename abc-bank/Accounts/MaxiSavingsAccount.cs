using System.Collections.Generic;

namespace abc_bank.Accounts {
  public class MaxiSavingsAccount : IAccount {
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
          return CurrentBalance * 0.02;
        } else if (CurrentBalance <= 2000) {
          return 20 + (CurrentBalance - 1000) * 0.05;
        } else {
          return 70 + (CurrentBalance - 2000) * 0.1;
        }
      }
    }

    #endregion

    #region CTOR

    public MaxiSavingsAccount() {
      AccountType = AccountType.MAXI_SAVINGS;
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
