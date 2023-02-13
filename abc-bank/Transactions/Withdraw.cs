using System;

using abc_bank.Accounts;

namespace abc_bank.Transactions {

  public class Withdraw : IWithdraw {

    #region Properties

    public int Id => throw new NotImplementedException();

    public int AccountId { get; }

    public DateTime Date { get; }

    public double Amount { get; }

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
