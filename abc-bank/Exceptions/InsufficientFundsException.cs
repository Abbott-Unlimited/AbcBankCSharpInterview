using System;

namespace abc_bank.Exceptions {
  [Serializable]
  public class InsufficientFundsException : Exception {
    public override string Message => "Insufficient funds to complete this transaction.";
  }
}
