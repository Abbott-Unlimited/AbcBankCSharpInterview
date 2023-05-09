using System;

namespace abc_bank
{
    public class DateProvider
    {
        private static DateProvider instance;

        public static DateProvider getInstance()
        {
            return instance ?? (instance = new DateProvider());
        }

        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}
