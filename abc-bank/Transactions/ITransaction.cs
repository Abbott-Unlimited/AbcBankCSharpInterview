using System;

namespace abc_bank.Transactions {
  public interface ITransaction {
    DateTime Date { get; }
    decimal Amount { get; }
    decimal GetStatementAmount();
  }
}
