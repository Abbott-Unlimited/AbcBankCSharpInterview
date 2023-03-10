using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public static class DateProvider
    {
        private static DateTime currentDate = DateTime.Now;
        public static DateTime Now { get { return currentDate; } set { currentDate = value; } }

        public static void Reset()
        {
            currentDate = DateTime.Now;
        }

        ///<summary>
        /// Accepts positive or negative numbers
        ///</summary>
        public static void AdjustDateByDays(int days)
        {
            currentDate = currentDate.AddDays(days);
        }
    }
}
