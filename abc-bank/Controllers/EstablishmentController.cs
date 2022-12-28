using AbcCompanyEstablishmentApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace AbcCompanyEstablishmentApp.Controllers
{
    public static class EstablishmentController
    {
        public static List<Establishment> Establishments = new List<Establishment>();

        public static void AddEstablishment(Establishment establishment)
        {
            Establishments.Add(establishment);
        }

        public static string GetEstablishmentSummary()
        {
            string summary = "Customer Summary";
            foreach (Establishment establishment in Establishments)
            {
                var numberOfAccounts = establishment.Accounts.Count;
                summary += $"\n - {establishment.EstablishmentName} ( {establishment.Type} - {establishment.EstablishmentOwnerName} - {establishment.EstablishmentPhysicalAddress} -  {establishment.EstablishmentContactNumber} )";
            }

            return summary;
        }
    }
}
