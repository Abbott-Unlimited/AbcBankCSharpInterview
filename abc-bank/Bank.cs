using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> Customers;

        public Bank()
        {
            Customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public String CustomerSummary() 
        {
            String summary = "Customer Summary";
            foreach (Customer c in Customers)
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

        public double totalInterestPaid() 
        {
            double total = 0;
            foreach (Customer c in Customers)
            {
                total += c.TotalInterestEarned();
            }

            return total;
        }

        public int TotalNumberAccounts()
        {
            int numaccts = 0;
            foreach(var account in this.Customers)
            {
                numaccts += account.GetNumberOfAccounts();
            }

            return numaccts;
        }

        public String GetFirstCustomer()
        {
            try
            {
                Customers = null;
                return Customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }
    }
}
