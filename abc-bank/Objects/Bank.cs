using abc_bank.Utilities;
using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> customers = new List<Customer>();

        public Bank()
        {
            customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public string CustomerSummary()
        {
            string summary = "Customer Summary";
            foreach (Customer customer in customers)
            {
                var numberOfAccounts = customer.Accounts.Count;
                summary += "\n - " + customer.FullName + " (" + numberOfAccounts + BankFunctions.MakeWordPlural(numberOfAccounts, "account") + ")";
            }

            return summary;
        }


    }
}
