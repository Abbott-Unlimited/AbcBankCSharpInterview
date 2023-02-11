using System;

namespace abc_bank.Transactions {
  public interface ITransaction {

    double Amount { get; }

    TransactionType TransactionType { get; }

    DateTime TransactionDate { get; }

  }
}
