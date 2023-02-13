using abc_bank.Extensions.Formatting;
using abc_bank.Transactions;

namespace abc_bank.Extensions.Reports {
  public static class StatementExtensions {

    #region GetStatementAmount

    public static double GetStatementAmount(this IDeposit t) => t.Amount;
    public static double GetStatementAmount(this IWithdraw t) => (t.Amount * -1);
    //public static double GetStatementAmount(this ITransfer t) => doSomething...

    #endregion


    #region GetLineItem

    public static string GetLineItem(this IDeposit t) => $"  deposit {t.GetStatementAmount().ToDollars()}";
    public static string GetLineItem(this IWithdraw t) => $"  withdrawal {t.GetStatementAmount().ToDollars()}";
    //public static string GetLineItem(this ITransfer t) => doSomething...

    #endregion

  }
}
