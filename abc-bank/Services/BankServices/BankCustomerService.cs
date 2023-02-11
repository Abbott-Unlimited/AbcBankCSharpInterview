using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Services.BankServices
{
    public class BankCustomerService
    {
        public string GetCustomerSummary(Bank bank)
        {
            var customers = bank.GetCustomers();

            if (customers.Count == 0)
                return "This bank has no customers.";

            string customerSummary = "Customer Summary";

            foreach (var customer in customers)
            {
                customerSummary += $"\n - {customer.GetName()} ({customer.GetNumberOfAccounts()} account{(customer.GetNumberOfAccounts() != 1 ? "s" : "")})";
            }

            return customerSummary;
        }

        public string GetFirstCustomer(Bank bank)
        {
            return bank.GetCustomers().OrderBy(x => x.GetDateCreated()).FirstOrDefault()?.GetName() ?? "There are no customers at this bank.";
        }
    }
}
