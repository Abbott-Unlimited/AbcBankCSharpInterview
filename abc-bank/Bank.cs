using System;
using System.Collections.Generic;

namespace abc_bank {
  public class Bank {
    private List<Customer> customers;

    #region Properties

    // TODO:  Should be checking if has customers first, then returning first.  No need for error trap.
    public string FirstCustomer {
      get {
        try {
          customers = null;

          return customers[0].Name;
        } catch (Exception e) {
          Console.Write(e.StackTrace);

          return "Error";
        }
      }
    }

    public string CustomerSummary {
      get {
        var summary = "Customer Summary";

        foreach (Customer c in customers) {
          summary += "\n - " + c.Name + " (" + Format(c.NumberOfAccounts, "account") + ")";
        }

        return summary;
      }
    }

    public double TotalInterestPaid {
      get {
        var total = 0.0;

        foreach (Customer c in customers) {
          total += c.TotalInterestEarned;
        }

        return total;
      }
    }

    #endregion

    #region CTOR

    public Bank() {
      customers = new List<Customer>();
    }

    #endregion

    #region Public Methods

    public void AddCustomer(Customer customer) {
      customers.Add(customer);
    }

    #endregion

    #region Private Methods

    //Make sure correct plural of word is created based on the number passed in:
    //If number passed in is 1 just return the word otherwise add an 's' at the end
    private string Format(int number, string word) {
      return number + " " + (number == 1 ? word : word + "s");
    }

    #endregion
  }
}
