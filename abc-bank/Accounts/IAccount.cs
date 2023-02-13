using System.Collections.Generic;

using abc_bank.Transactions;

namespace abc_bank.Accounts {
  public interface IAccount {

    #region Properties

    int Id { get; }

    int CustomerId { get; }

    string ReportLabel { get; }

    AccountType AccountType { get; }

    List<ITransaction> Transactions { get; }

    bool HasTransactions { get; }

    double CurrentBalance { get; }

    double InterestEarned { get; }

    #endregion

    #region Public Methods

    IDeposit Deposit(double amount);

    IWithdraw Withdraw(double amount);

    #endregion

  }
}
