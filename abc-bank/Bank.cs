using System.Collections.Generic;

using abc_bank.Customers;

namespace abc_bank {
  public class Bank {

    #region Properties

    protected List<Customer> Customers { get; } = new List<Customer>();

    public int NumberOfCustomers => Customers.Count;

    public bool HasCustomers => Customers.Count > 0;

    public string FirstCustomer => HasCustomers
          ? Customers[0].Name
          : Messages.NO_CUSTOMERS_MSG;

    public string CustomerSummary => HasCustomers
        ? Reports.CustomerSummary.Report(Customers)
        : Messages.NO_CUSTOMERS_MSG;

    public decimal TotalInterestPaid {
      get {
        decimal total = 0;

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
