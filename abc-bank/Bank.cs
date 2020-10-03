using System;
using System.Collections.Generic;
using System.Linq;

namespace abc_bank
{
    /// <summary>
    /// Bank containing all bank customers (and therefore all information relating to the customers such as accounts and transactions)
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// All the customers for the bank, hashset to prevent duplicates
        /// </summary>
        private HashSet<Customer> customers;

        /// <summary>
        /// Constructor initializes emtpty customer list
        /// </summary>
        public Bank()
        {
            customers = new HashSet<Customer>();
        }

        /// <summary>
        /// Adds a customer to the bank
        /// </summary>
        /// <param name="customer">The new customer's name</param>
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        /// <summary>
        /// Gets number of accounts between all customers
        /// </summary>
        /// <returns>String to display all customers and how many accounts each has</returns>
        public string CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " (" + format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }

        /// <summary>
        /// Formats individual customer line for CustomerSummary(), specifically formats
        /// the word "account" as "accounts" if the customer has more than one account
        /// </summary>
        /// <param name="number">Number of accounts the customer has</param>
        /// <param name="word">The word "account"</param>
        /// <returns></returns>
        private string format(int number, String word)
        {
            //Make sure correct plural of word is created based on the number passed in:
            //If number passed in is 1 just return the word otherwise add an 's' at the end
            return number + " " + (number == 1 ? word : word + "s");
        }

        /// <summary>
        /// Gets total interest paid by the bank to its customers
        /// </summary>
        /// <returns></returns>
        public double totalInterestPaid() {
            double total = 0;
            foreach(Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        /// <summary>
        /// Gets the first customer, throws error on empty hashset
        /// </summary>
        /// <returns>The first customer or an error if no customers</returns>
        public string GetFirstCustomer()
        {
            try
            {
                customers = null;
                return customers.First().GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }
    }
}
