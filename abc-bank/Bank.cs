namespace ABC_bank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Bank
    {
        private List<Customer> customers;

        public Bank()
        {
            this.customers = new List<Customer>();
        }

        public void AddCustomer(Customer customer)
        {
            this.customers.Add(customer);
        }

        public string CustomerSummary()
        {
            string summary = "Customer Summary";

            foreach (Customer c in this.customers)
            {
                summary += Environment.NewLine + " - " + c.GetName() + " (" + this.Format(c.GetNumberOfAccounts(), "account") + ")";
            }

            return summary;
        }

        public double TotalInterestPaid()
        {
            double total = 0;
            foreach (Customer c in this.customers)
            {
                total += c.TotalInterestEarned();
            }

            return total;
        }

        public string GetFirstCustomer()
        {
            try
            {
                this.customers = null;
                return this.customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }

        // Make sure correct plural of word is created based on the number passed in:
        // If number passed in is 1 just return the word otherwise add an 's' at the end
        private string Format(int number, string word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
