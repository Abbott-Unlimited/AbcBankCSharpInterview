using AbcCompanyEstablishmentApp;
using System;
using System.Collections.Generic;
using System.Linq;
using static AbcCompanyEstablishmentApp.Utilities.AbcCustomValues;

namespace abc_bank_tests.AbcTestFunctions
{
    public static class Random
    {
        const string RANDOM_CHARACTERS = "abcdefghijklmnopqrstuvwxyz0123456789";
        const int RANDOM_STRING_LENGTH = 4;

        private static System.Random random = new System.Random();

        public static string GetRandomString(int stringLength = RANDOM_STRING_LENGTH, bool numbersOnly = false)
        {
            string RANDOM_NUMBERS = RANDOM_CHARACTERS.Split(';')
            .Where(x => !String.IsNullOrEmpty(x))
            .Select(x => x.Substring(0, x.IndexOf('z')) + x.Substring(x.IndexOf('9'))).ToString();

            var randomCharacters = numbersOnly ? RANDOM_CHARACTERS : RANDOM_NUMBERS;

            return new string(Enumerable.Repeat(randomCharacters, stringLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static Establishment GetRandomEstablishment()
        {
            return new Establishment(AbcCompanyEstablishmentApp.Utilities.AbcCustomValues.EstablishmentType.BANK,
                GetRandomString(15),
                GetRandomString(10),
                GetRandomString(30),
                GetRandomString(11, true)
                );
        }

        public static Customer GetRandomCustomer()
        {
            return new Customer(                            
                AccountType.CHECKING,
                0,
                GetRandomString(10),
                GetRandomString(10)
            );
        }
    }
}
