using System;

using abc_bank.Accounts;

namespace abc_bank.Transactions {

  public class Withdraw : IWithdraw {

    #region Properties

    public int Id { get; }

    public int AccountId { get; }

    public DateTime Date { get; }

    public double Amount { get; }

    public bool IsFromTransfer { get; }

    public ITransfer TransferDetails { get; }

    #endregion

    #region CTOR

    public Withdraw(double amount, DateTime transactionDate, IAccount withdrawAccount) {
      Utilities.ValidateFundsAvailable(withdrawAccount.CurrentBalance, amount);

      Amount = amount;
      Date = transactionDate;
      AccountId = withdrawAccount.Id;
    }

    #endregion

    #region Public Methods

    public double GetStatementAmount() {
      return Amount * -1;
    }

    #endregion
    
  }
}
