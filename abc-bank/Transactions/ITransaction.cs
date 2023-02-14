using System;

namespace abc_bank.Transactions {
  public interface ITransaction {
    DateTime Date { get; }
    double Amount { get; }
    double GetStatementAmount();
  }
}
