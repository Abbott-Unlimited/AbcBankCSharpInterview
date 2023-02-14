using System;

namespace abc_bank.Exceptions {
  public class InvalidAccountException : Exception {
    public override string Message => "Referenced Account is Invalid.";
  }
}
