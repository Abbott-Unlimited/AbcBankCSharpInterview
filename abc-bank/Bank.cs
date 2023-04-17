﻿using System;
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

        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }

        //Solution for Requirement: Interest rates should accrue daily including weekends, rates above are per-annum (for each year).
        public void DailyInterestCalculation()
        {
            foreach(var customer in customers)
            {
                customer.CalculateAndDepositInterest();
            }

        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.TotalInterestEarned();
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
