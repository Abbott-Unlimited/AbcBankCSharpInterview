namespace ABC_bank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateProvider
    {
        private static DateProvider instance = null;

        public static DateProvider GetInstance()
        {
            if (instance == null)
            {
                instance = new DateProvider();
            }

            return instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
