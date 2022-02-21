using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Utilities.Date
{
    /// <summary>
    /// A singleton to be injected to provide DateTime services.
    /// </summary>
    public class DateProvider : IDateProvider
    {
        private static DateProvider _instance = null;

        // there should only be one instance
        private DateProvider() { }

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
