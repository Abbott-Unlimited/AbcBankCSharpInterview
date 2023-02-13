using System.Text;

using abc_bank.Extensions.Formatting;
using abc_bank.Transactions;

namespace abc_bank.Extensions.Reports {
  public static class ReportExtensions {

    #region GetStatementAmount

    public static double GetStatementAmount(this IDeposit t) => t.Amount;
    public static double GetStatementAmount(this IWithdraw t) => (t.Amount * -1);

    #endregion

    #region GetLineItem

    public static void GetLineItem(this IDeposit t, ref StringBuilder sb) {
      sb.AppendLine($"  deposit {t.GetStatementAmount().ToDollars()}");

      if (t.IsFromTransfer) {
        sb.AppendLine($"      - transferred from account {t.TransferDetails.OriginAccountId}");
      }
    }

    public static void GetLineItem(this IWithdraw t, ref StringBuilder sb) {
      sb.AppendLine($"  withdrawal {t.GetStatementAmount().ToDollars()}");

      if (t.IsFromTransfer) {
        sb.AppendLine($"      - transferred to account {t.TransferDetails.DestinationAccountId}");
      }
    }

    #endregion

  }
}
