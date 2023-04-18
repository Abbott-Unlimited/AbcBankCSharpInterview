using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public Bank AddCustomer(Customer customer)
        {
            customers.Add(customer);

            return this;
        }

        public String CustomerSummary()
        {
            StringBuilder summary = new StringBuilder("Customer Summary");

            foreach (Customer customer in customers)
            {
                String numberOfAccounts = format(customer.GetNumberOfAccounts(), "account");

                summary.Append($"\n - {customer.GetName()} ({numberOfAccounts})");
            }

            return summary.ToString();
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String format(int number, String word)
        {
            word = number == 1 ? word : $"{word}s";

            return $"{number} {word}";
        }

        public double TotalInterestPaid()
        {
            return customers.Sum(x => x.TotalInterestEarned());
        }

        public String GetFirstCustomer()
        {
            try
            {
                return customers[0].GetName();
            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace);

                return "Error";
            }
        }
    }
}