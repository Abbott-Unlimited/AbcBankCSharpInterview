using System;

using abc_bank.Accounts;
using abc_bank.Exceptions;
using abc_bank.Utilities;

namespace abc_bank.Transactions {
  public class Transfer : ITransfer {

    #region Properties

    public DateTime Date { get; }

    public double Amount { get; }

    public int OriginAccountId { get; }

    public int DestinationAccountId { get; }

    #endregion

    #region CTOR

    public Transfer(double transferAmount, DateTime transactionDate, IAccount originAccount, IAccount destinationAccount) {

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

      // process the transfer as a normal Withdraw/Deposit - with some addtional information
      if (originAccount.Withdraw(this)) {
        // don't try the deposit unless the withdraw was successful.
        destinationAccount.Deposit(this);
      }
    }

    #endregion

    #region Unused in Transfer transactions.

    public double GetStatementAmount() => throw new NotImplementedException();

    #endregion

  }
}
