using System;
using System.Text;

using abc_bank.Accounts;
using abc_bank.Customers;
using abc_bank.Transactions;

namespace abc_bank.Reports {
  public static class Statements {
    // indent by 4 spaces.
    private const string LINE_ITEM_INDENT = "    ";

    public static string CustomerStatement(Customer c) {
      var sb = new StringBuilder();

      sb.AppendLine($"Statement for {c.Name}");
      sb.AppendLine();

      decimal total = 0;

      foreach (var account in c.Accounts) {
        sb.Append(AccountStatement(account));
        sb.AppendLine();
        total += account.CurrentBalance;
      }

      sb.AppendLine($"Total In All Accounts {total.ToDollars()}");

      return sb.ToString();
    }

    public static string AccountStatement(IAccount account) {
      var sb = new StringBuilder();
      decimal total = 0;
      string lineItem;

      sb.AppendLine(account.ReportLabel);

      foreach (var t in account.Transactions) {
        if (t is IDeposit) {
          lineItem = (t as IDeposit).GetLineItem();
        } else {
          lineItem = (t as IWithdraw).GetLineItem();
        }

        sb.Append(LINE_ITEM_INDENT);
        sb.AppendLine(lineItem);
        total += t.GetStatementAmount();
      }

      sb.AppendLine("Total " + total.ToDollars());

      return sb.ToString();
    }

    #region Utilities, Helpers and Extension Methods

    public static string ToDollars(this decimal value, bool isWithdraw = false) {
      return $"{(isWithdraw ? "-" : "")}{string.Format("{0:C2}", Math.Abs(value))}";
    }

    #region GetLineItem

    public static string GetLineItem(this IDeposit t) {
      string details = string.Empty;

      if (t.IsFromTransfer) {
        int accountId = t.TransferDetails.DestinationAccountId;
        details = GetTransferLineItemDetails("from", accountId);
      }

      return $"{FormatLineItemLabel("deposit")}{FormatLineItemAmount(t)}{details}";
    }

    public static string GetLineItem(this IWithdraw t) {
      string details = string.Empty;

      if (t.IsFromTransfer) {
        int accountId = t.TransferDetails.DestinationAccountId;
        details = GetTransferLineItemDetails("to", accountId);
      }
      return $"{FormatLineItemLabel("withdrawal")}{FormatLineItemAmount(t)}{details}";
    }

    public static string GetLineItem(this ITransaction t) {
      string lineItem;

      if (t is IDeposit) {
        lineItem = (t as IDeposit).GetLineItem();
      } else if (t is IWithdraw) {
        lineItem = (t as IWithdraw).GetLineItem();
      } else {
        // guard against future me (or someone like me anyway)
        throw new System.Exception("Unknown transaction type arg to GetLineItem(this ITransaction t)");
      }

      return lineItem;
    }

    #endregion

    #region Line Item Helpers


    private static string FormatLineItemLabel(string line) => (line).PadRight(10);

    // padding the amount by 15 preserve alignment of all statement line items (variable dollar amounts).
    private static string FormatLineItemAmount(ITransaction t) => t.GetStatementAmount().ToDollars(t is IWithdraw).PadLeft(15);

    private static string GetTransferLineItemDetails(string direction, int accountId) {
      var details = $"transfer {direction} acct #{FormatLineItemAccountId(accountId)}";
      return details.PadLeft(30);
    }

    private static string FormatLineItemAccountId(int accountId) => accountId.ToString().PadLeft(8, '0');

    #endregion

    #endregion

  }
}
