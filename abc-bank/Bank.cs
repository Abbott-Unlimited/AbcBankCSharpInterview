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

        public Customer GetCustomer(string customerName)
        {
            return customers.Where(x => x.GetName() == customerName).FirstOrDefault();
        }
        public String CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
            {
                var accounts = c.GetAccounts();
                summary += "\n - " + c.GetName() + " (" + format(accounts.Count, "account") + ")";
            }
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double totalInterestPaid() {
            double total = 0;
            foreach(Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public String GetFirstCustomer()
        {
            try
            {
                // I don't know if this is intentional but you would always throw here if you set customers to null
                // customers = null;
                return customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }
    }
}
