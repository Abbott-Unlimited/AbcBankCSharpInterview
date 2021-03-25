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
    }
}
