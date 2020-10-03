using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class DateProvider
    {
        private static DateProvider instance = null;

        private static DateTime? time;

        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        public static void setInstance(DateTime? mockTime)
        {
            time = mockTime;
        }

        public DateTime Now()
        {
            return time.HasValue ? time.Value : DateTime.Now;
        }
    }
}
