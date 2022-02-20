using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Utilities.Date
{
    public class DateProvider : IDateProvider
    {
        private static DateProvider _instance = null;

        public static DateProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DateProvider();
                return _instance;
            }
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
