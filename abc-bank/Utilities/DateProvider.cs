using System;

namespace abc_bank
{
    public class DateProvider
    {
        private static DateProvider instance = null;

        public static DateProvider GetInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
