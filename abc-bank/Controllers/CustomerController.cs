using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompanyEstablishmentApp.Controllers
{
    public static class CustomerController
    {
        public static List<Customer> Customers = new List<Customer>();

        public static Customer GetCustomerByCustomerID(Guid customerID)
        {
            return Customers.FirstOrDefault(x => x.CustomerID == customerID);
        }

        public static Customer GetCustomerByExactFullName(string customerFullName)
        {
            return Customers.FirstOrDefault(x => x.FullName == customerFullName);
        }

        public static string GetCustomerNameByID(Guid customerID)
        {
            return Customers.FirstOrDefault(x => x.CustomerID == customerID).FullName;
        }

        public static int GetNumberOfAccountsByID(Guid customerID)
        {
            return Customers.FirstOrDefault(x => x.CustomerID == customerID).Accounts.Count;
        }
    }
}
