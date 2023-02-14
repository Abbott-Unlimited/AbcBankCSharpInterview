using System;

using abc_bank.Accounts;
using abc_bank.Exceptions;
using abc_bank.Utilities;

namespace abc_bank.Transactions {
  public class Transfer : ITransfer {

    #region Properties

    public DateTime Date { get; }

    public decimal Amount { get; }

    public int OriginAccountId { get; }

    public int DestinationAccountId { get; }

    #endregion

    #region CTOR

    public Transfer(decimal transferAmount, DateTime transactionDate, IAccount originAccount, IAccount destinationAccount) {

      #region Validation

      if (originAccount.CustomerId != destinationAccount.CustomerId) {
        throw new InvalidTransactionRequestException();
      }

      Validators.FundsAvailableForWithdraw(originAccount.CurrentBalance, transferAmount);

      #endregion

      Amount = transferAmount;
      Date = transactionDate;
      OriginAccountId = originAccount.Id;
      DestinationAccountId = destinationAccount.Id;

      if (originAccount.Withdraw(this)) {
        destinationAccount.Deposit(this);
      }
    }

    #endregion

    #region Unused in Transfer transactions, however it's part of the ITransaction interface.

    public decimal GetStatementAmount() => throw new NotImplementedException();

    #endregion

  }
}
