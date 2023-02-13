using System.Collections.Generic;

namespace abc_bank {
  public class Bank {

    #region Properties

    protected List<Customer> Customers { get; } = new List<Customer>();

    public int NumberOfCustomers => Customers.Count;

    public bool HasCustomers => Customers.Count > 0;

    // todo:  Shouldn't this be based on sign-up date or something?  
    //        C# handles ordering in Lists/Arrays better than Javascript - but still poor practice
    //        to depend over-much on first index being actual first 'anything' in any collection.
    public string FirstCustomer => HasCustomers
          ? Customers[0].Name
          : Messages.NO_CUSTOMERS_MSG;

    public string CustomerSummary => HasCustomers
        ? Reports.CustomerSummary.Report(Customers)
        : Messages.NO_CUSTOMERS_MSG;

    public double TotalInterestPaid {
      get {
        var total = 0.0;

        foreach (var c in Customers) {
          total += c.TotalInterestEarned;
        }

        return total;
      }
    }

    #endregion

    #region Public Methods

    public void AddCustomer(Customer customer) => Customers.Add(customer);

    #endregion

  }
}
