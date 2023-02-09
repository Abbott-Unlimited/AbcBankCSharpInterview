using System.Collections.Generic;

namespace abc_bank.Accounts {
  public class CheckingAccount : IAccount {
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
        return CurrentBalance * 0.001;
      }
    }

    #endregion

    #region CTOR

    public CheckingAccount() {
      AccountType = AccountType.CHECKING;
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
