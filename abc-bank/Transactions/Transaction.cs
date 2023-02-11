using System;

namespace abc_bank.Transactions {

  public class Transaction : ITransaction {

    #region Properties

    public double Amount { get; }

    public TransactionType TransactionType { get; }

    public DateTime TransactionDate { get; protected set; }

    #endregion

    public Transaction(double amount, TransactionType transactionType) {
      Amount = amount;
      TransactionType = transactionType;
      TransactionDate = DateProvider.getInstance().Now();
    }
  }
}
