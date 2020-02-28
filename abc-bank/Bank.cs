using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Bank : IBank
    {
        private IList<ICustomer> Customers;

        public Bank()
        {
            Customers = new List<ICustomer>();
        }

        public void AddCustomer(ICustomer customer)
        {
            Customers.Add(customer);
        }

        public String GetCustomerSummary() {
            String summary = "Customer Summary";
            foreach (ICustomer c in Customers)
                summary += c.ToString();
            return summary;
        }

        public double GetTotalInterestPaid() {
            double total = 0;
            foreach(ICustomer c in Customers)
                total += c.GetTotalInterestEarned();
            return total;
        }

        public String GetFirstCustomerName()
        {
            try
            {
                return Customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "error";
            }
        }
    }
}
