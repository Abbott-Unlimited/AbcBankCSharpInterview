using System;
using System.Collections.Generic;

namespace abc_bank
{
    public class Bank
    {
        private List<Customer> _customers;

        public Bank()
        {
            _customers = new List<Customer>();
        }

        public List<Customer> Customers { get { return _customers; } }

        public void AddCustomer(Customer customer)
        {
            if (DoesCustomerExist(customer) == true)
            {
                throw new Exception("A customer with this name alreadty exists!");
            }
            _customers.Add(customer);
        }

        public string CustomerSummary() {
            String summary = "Customer Summary";
            foreach (Customer c in _customers)
                summary += "\n - " + c.CustomerName + " (" + format(c.NumberOfAccounts, "account") + ")";
            return summary;
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        private string format(int number, string word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public double totalInterestPaid() {
            double total = 0;
            foreach(Customer c in _customers)
                total += c.TotalInterestEarned();
            return total;
        }

        public void OpenAccount(Customer customer, Account account)
        {
            bool doesAccountAlreadyExist = customer.Accounts.Exists(x => x.AccountType == account.AccountType);

            if(doesAccountAlreadyExist == false)
            {
                customer.OpenAccount(account);
            }
            else
            {
                throw new Exception("Customer already has an account of this type!");
            }
        }

        public bool DoesCustomerExist(Customer customer)
        {
            bool customerExists = _customers.Exists(x => x.CustomerName == customer.CustomerName);
            return customerExists;
        }
    }
}
