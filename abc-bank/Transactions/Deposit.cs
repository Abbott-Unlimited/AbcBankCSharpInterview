using System;

using abc_bank.Accounts;

namespace abc_bank.Transactions {
  public class Deposit : IDeposit {

    #region Properties

    public int Id { get; }

    public int AccountId { get; }

    public DateTime Date { get; }

    public double Amount { get; }

    #endregion

    #region CTOR

    public Deposit(double amount, DateTime transactionDate, IAccount depositAccount) {
      Utilities.ValidateTransactionAmount(amount);

      Amount = amount;
      Date = transactionDate;
      AccountId = depositAccount.Id;
    }

    #endregion

    #region Public Methods

    public double GetStatementAmount() {
      return Amount;
    }

    #endregion
  }
}
