using System.Collections.Generic;
using System.Text;

using abc_bank.Customers;

namespace abc_bank.Reports {
  public static class CustomerSummary {

    public static string Report(List<Customer> Customers) {
      int numOfAccts;

      var sb = new StringBuilder();
      sb.AppendLine("Customer Summary");

      foreach (var c in Customers) {
        numOfAccts = c.NumberOfAccounts;

        sb.AppendLine($" - {c.Name} ({numOfAccts} account{(numOfAccts == 1 ? "" : "s")})");
      }

      return sb.ToString();
    }
  }
}
