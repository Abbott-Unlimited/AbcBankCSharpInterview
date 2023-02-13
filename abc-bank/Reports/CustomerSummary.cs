using System.Collections.Generic;
using System.Text;

namespace abc_bank.Reports {
  public static class CustomerSummary {
    public static string Report(List<Customer> Customers) {
      var sb = new StringBuilder();
      sb.AppendLine("Customer Summary");

      foreach (var c in Customers) {
        sb.AppendLine($" - {c.Name} ({Format(c.NumberOfAccounts, "account")})");
      }

      return sb.ToString();
    }

    private static string Format(int number, string word) => $"{number} {(number == 1 ? word : word + "s")}";
  }
}
