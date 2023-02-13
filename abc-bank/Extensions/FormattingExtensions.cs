using System;

 namespace abc_bank.Extensions.Formatting {
  public static class FormattingExtensions {

    public static string ToDollars(this double value) => string.Format("{0:C2}", Math.Abs(value));

  }
}
