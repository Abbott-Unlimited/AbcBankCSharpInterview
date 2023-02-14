using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class AccountingLogicHelpers
    {
        public static double ReturnCleanValue(double amount)
        {
            return Math.Round(amount, 2);
        }

        public static int GetDays(DateTime? start, DateTime end)
        {
            if(end == null)
            {
                return 0;
            }
            if(start == null)
            {
                return 0;
            }
            return (end - (DateTime)start).Days;
        }

    }
}
