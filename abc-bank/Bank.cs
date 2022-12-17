using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Bank
    {
        #region | Globals |
        private List<Customer> customers;

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
        #endregion

        #region | Constructors |
        public Bank()
        {
            customers = new List<Customer>();
        }
        #endregion

        #region | AddCustomer |
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }
        #endregion

        #region | CustomerSummary |
        public String CustomerSummary()
        {
            String summary = "Customer Summary";
            foreach (Customer c in customers)
                summary += "\n - " + c.GetName() + " (" + Format(c.GetNumberOfAccounts(), "account") + ")";
            return summary;
        }
        #endregion

        #region | Format |
        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private String Format(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
        #endregion

        #region | TotalInterestPaid |
        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in customers)
                total += c.TotalInterestEarned();
            return total;
        }
        #endregion
    }
}
