using System.Collections.Generic;
using System.Linq;
using Utilities;


namespace abc_bank
{
    public class Bank
    {
        private List<Customer> _Customers;

        public Bank()
        {
            _Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            _Customers.Add(customer);
        }

        public string CustomersSummary()
        {
            string result = "Customers Summary";

            _Customers.ForEach(customer =>
                result += string.Format("\n - {0} ({1})", 
                customer.Name, 
                customer.Accounts.Count.FormatMorpheme("account")));

            return result;
        }

        public double TotalInterestPaid()
        {
            return _Customers.Sum(customer => customer.TotalInterestEarned());
        }

        public string GetFirstCustomer()
        {
            return _Customers.FirstOrDefault().Name;
        }
    }
}
