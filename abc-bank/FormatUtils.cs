using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    static class FormatUtils
    {
        /// <summary>
        /// Formats a value as a dollar amount.
        /// </summary>
        /// <param name="d">The decimal amount.</param>
        /// <returns>string representation of the value as a dollar amount.</returns>
        public static string ToDollars(decimal d)
        {
            return string.Format("${0:N2}", Math.Abs(d));
        }

        /// <summary>
        /// Attempts to pluralize the given word if the number specified is not 1.
        /// </summary>
        /// <param name="number">The number associated with the word.</param>
        /// <param name="word">The word to pluralize.</param>
        /// <returns>The given word, pluralized as apporpriate to match the number.</returns>
        public static string pluralize(int number, string word)
        {
            return number + " " + (number == 1 ? word : word + "s");
        }
    }
}
