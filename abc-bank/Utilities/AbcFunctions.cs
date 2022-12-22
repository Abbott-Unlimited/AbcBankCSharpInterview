using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompanyEstablishmentApp.Utilities
{
    public static class AbcFunctions
    {
        public static string ToDollars(double d)
        {
            return string.Format("$%,.2f", Math.Abs(d));
        }

        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        public static string MakeWordPlural(int number, string word)
        {
            return (number == 1 ? word : word + "s");
        }

        public static string GenerateAccountTypeString(AbcCustomValues.AccountType accountType)
        {
            var returnString = "";
            switch (accountType)
            {
                case AbcCustomValues.AccountType.CHECKING:
                    returnString += "Checking Account\n";
                    break;
                case AbcCustomValues.AccountType.SAVINGS:
                    returnString += "Savings Account\n";
                    break;
                case AbcCustomValues.AccountType.MAXI_SAVINGS:
                    returnString += "Maxi Savings Account\n";
                    break;
            }
            return returnString;
        }
    }
}
