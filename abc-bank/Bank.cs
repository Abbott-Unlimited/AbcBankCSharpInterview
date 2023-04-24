using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    // A bank manager can get a report showing the list of customers and how many accounts they have.
    // A bank manager can get a report showing the total interest paid by the bank on all accounts.

    public class Bank
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public Bank() { }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public string PrintCustomerSummary() 
        {
            string summary = "Customer Summary";

            foreach (Customer customer in _customers)
            {
                var numberOfAccounts = customer.GetNumberOfAccounts();
                string accountsTense = numberOfAccounts == 1 ? "" : "s";
                summary += $"\n - {customer.Name} ({numberOfAccounts} account{accountsTense})";
            }
            return summary;
        }

        public decimal GetTotalInterestPaid() 
        {
            decimal totalInterestPaid = 0;

            foreach (Customer customer in _customers)
            {
                totalInterestPaid += customer.GetTotalInterestEarned();
            }
            return totalInterestPaid;
        }

        public string GetFirstCustomer()
        {
            if (_customers.Count == 0)
            {
                return "No cutomers found";
            }
            return _customers.First().Name;
        }
    }
}
