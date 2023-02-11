using System.Collections.Generic;

using abc_bank.Transactions;

namespace abc_bank.Accounts {
  public interface IAccount {

    #region Properties

    int Id { get; }

    int CustomerId { get; }

    AccountType AccountType { get; }

    List<ITransaction> Transactions { get; }

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
