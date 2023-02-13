using System.Text;

using abc_bank.Accounts;
using abc_bank.Extensions.Formatting;
using abc_bank.Extensions.Reports;
using abc_bank.Transactions;

namespace abc_bank.Reports {
  public static class Statements {

    public static string CustomerStatement(Customer c) {
      var sb = new StringBuilder();

      sb.AppendLine($"Statement for {c.Name}");
      sb.AppendLine();

      var total = 0.0;

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
      var total = 0.0;

      sb.AppendLine(account.ReportLabel);

      //Now total up all the transactions

      foreach (var t in account.Transactions) {
        // really not liking that this is so far the only way that I can get
        // the actual transation type due to the implicit cast in List<ITransaction>
        var lineItem = t is IDeposit
          ? (t as IDeposit).GetLineItem()
          : (t as IWithdraw).GetLineItem();

        sb.AppendLine(lineItem);
        total += t.GetStatementAmount();
      }

      sb.AppendLine("Total " + total.ToDollars());

      return sb.ToString();
    }

  }
}
