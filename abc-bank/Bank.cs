using System;
using System.Collections.Generic;

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

        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer customer in customers)
            {
                var numberOfAccounts = customer.GetNumberOfAccounts();
                summary += "\n - " + customer.GetName() + " (" + numberOfAccounts + MakeWordPlural(numberOfAccounts, "account") + ")";
            }

            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String MakeWordPlural(int number, String word)
        {
            return (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer customer in customers)
                total += customer.TotalInterestEarned();
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
    }
}
