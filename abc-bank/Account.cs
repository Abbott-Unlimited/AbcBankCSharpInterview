using System.Collections.Generic;

namespace abc_bank {

  public class Account {
    #region Properties

    public AccountType AccountType {
      get;
    }

    public List<Transaction> Transactions {
      get;
    }

    #region These need corrected.

    public double SumTransactions {
      get {
        return CheckIfTransactionsExist(true);
      }
    }

    private double CheckIfTransactionsExist(bool checkAll) {
      var amount = 0.0;

      foreach (Transaction t in Transactions) {
        amount += t.amount;
      }

      return amount;
    }

    #endregion

    public double InterestEarned {
      get {
        double amount = SumTransactions;

        switch (AccountType) {
          case AccountType.SAVINGS: {
            if (amount <= 1000) {
              return amount * 0.001;
            } else {
              return 1 + (amount - 1000) * 0.002;
            }
          }
          case AccountType.MAXI_SAVINGS: {
            if (amount <= 1000) {
              return amount * 0.02;
            } else if (amount <= 2000) {
              return 20 + (amount - 1000) * 0.05;
            } else {
              return 70 + (amount - 2000) * 0.1;
            }
          }
          default:
            return amount * 0.001;
        }
      }
    }

    #endregion

    #region CTOR

    public Account(AccountType accountType) {
      AccountType = accountType;
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
      } else {
        Transactions.Add(new Transaction(-amount));
      }
    }

    #endregion
  }
}
