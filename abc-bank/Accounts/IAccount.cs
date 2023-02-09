using System.Collections.Generic;

namespace abc_bank.Accounts {
  public interface IAccount {

    #region Properties

    AccountType AccountType { get; }
    List<Transaction> Transactions { get; }
    bool HasTransactions { get; }
    double CurrentBalance { get; }
    double InterestEarned { get; }

    #endregion

    #region Public Methods

    void Deposit(double amount);
    void Withdraw(double amount);

    #endregion

  }
}
