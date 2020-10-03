using System;

namespace abc_bank
{
    /// <summary>
    /// Abstraction of DateTime, provides Date information for the project
    /// </summary>
    public class DateProvider
    {
        /// <summary>
        /// Instance of this object
        /// </summary>
        private static DateProvider instance = null;

        /// <summary>
        /// Mocked time for use in tests
        /// </summary>
        private static DateTime? time;

        /// <summary>
        /// Instantiates the DateProvider for the project if it does not exist and returns it
        /// </summary>
        /// <returns>Instance for DateProvider</returns>
        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        /// <summary>
        /// Sets a custom (mocked) date to be provided, primarily for testing
        /// </summary>
        /// <param name="mockTime">Mocked date</param>
        public static void setCustomDate(DateTime? mockTime)
        {
            time = mockTime;
        }

        /// <summary>
        /// Returns the current or mocked time
        /// </summary>
        /// <returns>Current or mocked time</returns>
        public DateTime Now()
        {
            return time.HasValue ? time.Value : DateTime.Now;
        }
    }
}
