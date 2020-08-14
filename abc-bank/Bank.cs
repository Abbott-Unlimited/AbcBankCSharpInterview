using System;
using System.Collections.Generic;
using System.Linq;

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
            {
                summary += "\n - " + c.GetName() + " (" + format(c.GetNumberOfAccounts(), "account") + ")";
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
            foreach (Customer c in customers)
            {
                total += c.TotalInterestEarned();
            }

            return total;
        }

        public String GetFirstCustomer()
        {
            try
            {
                customers = null;

                return customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);

                return "Error";
            }
        }

        public Customer AccessCustomerAccounts(Customer customer)
        {
            try
            {

                return customers.Where(c => c == customer).FirstOrDefault();
            }
            catch (Exception e) 
            {
                throw new ArgumentException("account doesn't exist");
            }
        }
    }
}
