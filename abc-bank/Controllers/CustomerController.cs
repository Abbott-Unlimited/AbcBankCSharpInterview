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

        public static Customer GetCustomerByID(Guid customerID)
        {
            return Customers.FirstOrDefault(x => x.CustomerID == customerID);
        }
        /// <summary>
        /// NOTE: This method is not future proof and needs to be more elegant and safe.
        /// </summary>
        /// <param name="customerFullName"></param>
        /// <returns>
        /// The first customer that matches the exact 
        /// name from a list of customers that match the exact name.
        /// </returns>
        public static Customer GetCustomerByExactFullName(string customerFullName)
        {
            return Customers.FirstOrDefault(x => x.FullName == customerFullName);
        }

        public static string GetCustomerNameByID(Guid customerID)
        {
            return Customers.FirstOrDefault(x => x.AccountID == customerID).FullName;
        }

        public static int GetNumberOfAccountsByID(Guid customerID)
        {
            return Customers.FirstOrDefault(x => x.AccountID == customerID).Accounts.Count;
        }
    }
}
