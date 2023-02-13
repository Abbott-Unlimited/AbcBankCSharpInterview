
using System;

using abc_bank.Accounts;

namespace abc_bank.Transactions {
  public class Transfer : ITransfer {

    #region Properties

    public int Id => throw new NotImplementedException();
    public DateTime Date { get; }
    public double Amount { get; }
    public int OriginAccountId { get; }
    public int DestinationAccountId { get; }

    #endregion

    #region CTOR

    public Transfer(double transferAmount, DateTime transactionDate, IAccount originAccount, IAccount destinationAccount) {
      Utilities.ValidateFundsAvailable(originAccount.CurrentBalance, transferAmount);
      Date = transactionDate;
      OriginAccountId = originAccount.Id;
      DestinationAccountId = destinationAccount.Id;
    }

    #endregion

    #region Public Methods

    public double GetStatementAmount() => throw new NotImplementedException();

    #endregion

  }
}
