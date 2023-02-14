using System;

using abc_bank.Accounts;
using abc_bank.Utilities;

namespace abc_bank.Transactions {
  public class Deposit : IDeposit {

    #region Properties

    public int Id { get; }

    public int AccountId { get; }

    public DateTime Date { get; }

    public decimal Amount { get; }

    public bool IsFromTransfer { get; }

    public ITransfer TransferDetails { get; }

    #endregion

    #region CTOR

    public Deposit(decimal amount, DateTime transactionDate, IAccount depositAccount, ITransfer transferDetails = null) {
      Validators.TransactionAmount(amount);

      Amount = amount;
      Date = transactionDate;
      AccountId = depositAccount.Id;

      if (transferDetails != null) {
        IsFromTransfer = true;
        TransferDetails = transferDetails;
      }
    }

    #endregion

    #region Public Methods

    public decimal GetStatementAmount() => Amount;

    #endregion
  }
}
