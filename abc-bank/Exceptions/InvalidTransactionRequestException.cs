using System;

namespace abc_bank.Exceptions {
  [Serializable]
  public class InvalidTransactionRequestException : Exception {
    public override string Message => "Requested transaction is invalid.";
  }
}
