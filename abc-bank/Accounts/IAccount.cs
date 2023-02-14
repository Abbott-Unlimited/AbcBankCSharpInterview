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

    decimal CurrentBalance { get; }

    decimal InterestEarned { get; }

    #endregion

    #region Public Methods

    IDeposit Deposit(decimal amount);

    bool Deposit(ITransfer fromTransfer);

    IWithdraw Withdraw(decimal amount);

    bool Withdraw(ITransfer fromTransfer);

    #endregion

  }
}
