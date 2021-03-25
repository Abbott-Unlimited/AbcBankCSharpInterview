using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Bank
    {
        /// <summary>
        /// The customers of the bank.
        /// </summary>
        private List<Customer> customers;

        /// <summary>
        /// Initializes a new Bank instance.
        /// </summary>
        public Bank()
        {
            customers = new List<Customer>();
        }

        /// <summary>
        /// Adds a new customer to the bank.
        /// </summary>
        /// <param name="customer">The Customer object to add.</param>
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        /// <summary>
        /// Generates a summary of this customer's accounts.
        /// </summary>
        /// <returns>String containing the text of the customer summary.</returns>
        public String CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.Name + " (" + format(c.NumberOfAccounts, "account") + ")";
            return summary;
        }

        /// <summary>
        /// Attempts to pluralize the given word if the number specified is not 1.
        /// </summary>
        /// <param name="number">The number associated with the word.</param>
        /// <param name="word">The word to pluralize.</param>
        /// <returns>The given word, pluralized as apporpriate to match the number.</returns>
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        /// <summary>
        /// Calculates the total interest paid out to all customers.
        /// </summary>
        /// <returns></returns>
        public decimal totalInterestPaid() {
            decimal total = 0.00m;
            foreach(Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        /// <summary>
        /// Returns the bank's first customer.
        /// </summary>
        /// <returns>The bank's first customer.</returns>
        public String GetFirstCustomer()
        {
            try
            {
                customers = null;
                return customers[0].Name;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }
    }
}
