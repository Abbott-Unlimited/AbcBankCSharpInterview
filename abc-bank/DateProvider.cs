using System;

namespace abc_bank
{
    public class DateProvider
    {
        #region | Globals |
        private static DateProvider instance = null;
        #endregion

        #region | GetInstance |
        public static DateProvider GetInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }
        #endregion

        #region | Now |
        public DateTime Now()
        {
            return DateTime.Now;
        }
        #endregion
    }
}
