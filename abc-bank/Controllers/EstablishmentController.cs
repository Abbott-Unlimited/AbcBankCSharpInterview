using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp.Controllers
{
    internal static class EstablishmentController
    {
        public static List<Customer> Customers = new List<Customer>();

        public static void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public static string CustomerSummary()
        {
            string summary = "Customer Summary";
            foreach (Customer customer in Customers)
            {
                var numberOfAccounts = customer.Accounts.Count;
                summary += "\n - " + customer.FullName + " (" + numberOfAccounts + AbcFunctions.MakeWordPlural(numberOfAccounts, "account") + ")";
            }

            return summary;
        }
    }
}
