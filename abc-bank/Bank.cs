using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    public class Bank
    {
        private readonly List<Customer> _customers;

        public Bank()
        {
            _customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _customers.Add(customer);
        }

        public void AddCustomers(List<Customer> customers)
        {
            _customers.AddRange(customers);
        }

        public string CustomerSummary()
        {
            return _customers.Aggregate("Customer Summary", (current, c) => current + ("\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")"));
        }

        public string GetFirstCustomer(string customerName)
        {
            return GetFirstCustomer();
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private static string Format(int number, string word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid()
        {
            return _customers.Sum(c => c.TotalInterestEarned());
        }

        private string GetFirstCustomer()
        {
            return _customers.Count > 2
                ? _customers.Select(x => x.GetName().FirstOrDefault()).ToString()
                : _customers.FirstOrDefault()?.GetName();
        }
    }
}
