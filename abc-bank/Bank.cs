using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Bank
    {
        public List<Customer> customers;

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
                summary += "\n - " + c.GetName() + " (" + format(c.GetNumberOfAccounts(), "account") + ")";
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
                customers = null;
                return customers[0].GetName();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                return "Error";
            }
        }

        public void AddChanges(Customer oldCustomerStatus, Customer customerWithChanges)
        {
            customers[customers.FindIndex(c => c == oldCustomerStatus)] = customerWithChanges; 
        }

        public String Transfer(Customer personToTransferTo, Account accountToTransferTo,Customer customerToTransferFrom, Account accountToTransferFrom, double amount)
        {
            try { 
                customerToTransferFrom.accounts[customerToTransferFrom.accounts.FindIndex(a => a.GetAccountType() == accountToTransferFrom.GetAccountType())].Withdraw(amount);
                personToTransferTo.accounts[personToTransferTo.accounts.FindIndex(a => a.GetAccountType() == accountToTransferTo.GetAccountType())].Deposit(amount);
                return "Success";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
