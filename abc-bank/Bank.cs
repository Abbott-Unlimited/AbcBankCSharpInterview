using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> customers;

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
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " (" + FormatWordToSingularOrPlural(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }


        /// <summary>
        /// Formats a word to the plural version if the number of items passed is more than 1.
        /// </summary>
        /// <param name="numberOfItems">Number of items you're passing in.</param>
        /// <param name="word">The word you want to format.</param>
        /// <returns></returns>
        private String FormatWordToSingularOrPlural(int numberOfItems, String word)
        {
            return numberOfItems + " " + (numberOfItems > 1 ? word + "s" : word );
        }

        public double TotalInterestPaidForOneDayAllAccounts() {
            double total = 0;
            foreach(Customer c in customers)
                total += c.TotalInterestEarnedForOneDay();
            return total;
        }

    }
}
