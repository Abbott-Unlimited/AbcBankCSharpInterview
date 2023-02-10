using System;
using System.Collections.Generic;

namespace abc_bank {
  public class Bank {
    private List<Customer> _customers;

    #region Properties

    protected List<Customer> Customers {
      get {
        if (_customers == null) {
          _customers = new List<Customer>();
        }
        return _customers;
      }
    }

    public bool HasCustomers => Customers.Count > 0;

    // todo:  Shouldn't this be based on sign-up date or something?  
    //        C# handles ordering in Lists/Arrays better than Javascript - but still poor practice
    //        to depend over-much on first index being actual first 'anything' in any collection.
    public string FirstCustomer {
      get {
        return HasCustomers
          ? Customers[0].Name
          : Messages.NO_CUSTOMERS_MSG;
      }
    }

    public string CustomerSummary {
      get {
        if (HasCustomers) {
          var summary = "Customer Summary";

          foreach (var c in Customers) {
            summary += "\n - " + c.Name + " (" + Format(c.NumberOfAccounts, "account") + ")";
          }

          return summary;
        }

        return Messages.NO_CUSTOMERS_MSG;
      }
    }

    public double TotalInterestPaid {
      get {
        var total = 0.0;

        foreach (var c in Customers) { // should probably be a LINQ query...
          total += c.TotalInterestEarned;
        }

        return total;
      }
    }

    #endregion

    #region Public Methods

    public void AddCustomer(Customer customer) {
      Customers.Add(customer);
    }

    // TODO: in a perfect world, more than just a name should be necessary...
    public Customer AddCustomer(string customerName) {
      var customer = new Customer(customerName);
      AddCustomer(customer);

      return customer;
    }

    #endregion

    #region Private Methods

    // Make sure correct plural of word is created based on the number passed in:
    // If number passed in is 1 just return the word otherwise add an 's' at the end
    //
    // protected, not private so it can be tested.
    // also, I abhor the name Format here - names should be descriptive and when possible, explicit
    protected string Format(int number, string word) {
      return number + " " + (number == 1 ? word : word + "s");
    }

    #endregion
  }
}
