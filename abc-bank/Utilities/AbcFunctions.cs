using Pluralize.NET;

namespace AbcCompanyEstablishmentApp.Utilities
{
    public static class AbcFunctions
    {
        public static string CheckPluralization(int count, string word)
        {
            IPluralize pluralizer = new Pluralizer();
            if (count > 1)
            {
                return pluralizer.Pluralize(word);
            } else
            {
                return pluralizer.Singularize(word);
            }                
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
