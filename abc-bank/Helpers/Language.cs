using System;

namespace abc_bank.Helpers
{
    public static class Language
    {
        //Make sure correct plural of word is created based on the number passed in:
        //If number passed in is 1 just return the word otherwise add an 's' at the end
        public static String FormatPlural(int number, String word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }

        public static String ToDollars(double d)
        {
            return String.Format("{0:C}", Convert.ToDecimal(Math.Abs(d)));
        }
    }
}
