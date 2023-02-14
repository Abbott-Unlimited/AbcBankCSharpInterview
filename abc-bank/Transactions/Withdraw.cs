using System;

using abc_bank.Accounts;
using abc_bank.Utilities;

namespace abc_bank.Transactions {

  public class Withdraw : IWithdraw {

    #region Properties

    public int Id { get; }

    public int AccountId { get; }

    public DateTime Date { get; }

    public decimal Amount { get; }

    public bool IsFromTransfer { get; }

    public ITransfer TransferDetails { get; }

    #endregion

    #region CTOR

    public Withdraw(decimal amount, DateTime transactionDate, IAccount withdrawAccount, ITransfer transferDetails = null) {
      Validators.FundsAvailableForWithdraw(withdrawAccount.CurrentBalance, amount);

      Amount = amount;
      Date = transactionDate;
      AccountId = withdrawAccount.Id;

      if (transferDetails != null) {
        IsFromTransfer = true;
        TransferDetails = transferDetails;
      }
    }

    #endregion

    #region Public Methods

    public decimal GetStatementAmount() => Amount * -1;

    #endregion

  }
}
