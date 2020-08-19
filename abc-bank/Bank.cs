using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> Customers { get; }

        public Bank()
        {
            Customers = new List<Customer>();
        }

        public double TotalInterestPaid =>
            Customers.Sum(customer => customer.TotalInterestEarned);

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public String GetCustomerSummaryReport()
        {
            String summary = "Customer Summary";

            foreach (Customer customer in Customers)
            {
                summary += $"\n - {customer.ToNameAndAccountsCountString()}";
            }

            return summary;
        }
    }
}
