using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class DateProvider
    {
        /// <summary>
        /// Holds the singleton instance of DateTime.
        /// </summary>
        /// <remarks>
        /// This has been left intact; but see PR comments for discussion of better alternatives.
        /// </remarks>
        private static DateProvider instance = null;

        /// <summary>
        /// Gets a DateProvider instance.
        /// </summary>
        /// <returns>DateProvider instance.</returns>
        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        /// <summary>
        /// Gets the current DateTime.
        /// </summary>
        /// <returns>DateTime object representing the current time.</returns>
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
