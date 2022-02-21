using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Customers;

namespace abc_bank
{
    /// <summary>
    /// An abstraction of a Bank.
    /// </summary>
    public class Bank
    {
        private List<Customer> customers;

        /// <summary>
        /// Creates an instance of a bank with zero customers.
        /// </summary>
        public Bank()
        {
            customers = new List<Customer>();
        }

        /// <summary>
        /// Adds a Customer to the Bank.
        /// </summary>
        /// <param name="customer">Instance of a Customer.</param>
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        /// <summary>
        /// Creates a string representation of the current state of the account.
        /// </summary>
        /// <returns>A string representation of a statement.</returns>
        public String CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer customer in customers)
                summary += "\n - " + customer.GetName() + " (" + Format(customer.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        /// <summary>
        /// Sums the total interest earned by each customer.
        /// </summary>
        /// <returns>The total interest paid out by the bank.</returns>
        public double TotalInterestPaid() {
            double total = 0;
            foreach(Customer customer in customers)
                total += customer.TotalInterestEarned();
            return total;
        }

        /// <summary>
        /// Returns the first customer of the bank.
        /// </summary>
        /// <returns>The first customer who opened an account.</returns>
        public Customer GetFirstCustomer()
        {
            Customer customer = null;

            if (customers.Count() > 0)
            {
                customer = customers.First();
            }

            return customer;
        }
    }
}
