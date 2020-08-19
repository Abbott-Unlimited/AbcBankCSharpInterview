using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Bank
    {
        public List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public String CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer cust in customers)
                summary += $"\n - {cust.name} ({cust.accounts.Count} account{(cust.accounts.Count == 1 ? "" : "s")})";
            return summary;
        }

        public double CalculateTotalInterestPaid() {
            double total = 0;
            foreach (var account in customers.SelectMany(c => c.accounts))
            {
                total += account.CalculateInterestEarned();
            }

            return total;
        }
    }
}
