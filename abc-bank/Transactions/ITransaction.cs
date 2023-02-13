using System;

namespace abc_bank.Transactions {
  public interface ITransaction {

    int Id { get; }

    DateTime Date { get; }

    double Amount { get; }

    double GetStatementAmount();    
    
  }
}
