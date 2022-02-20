using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Utilities.Date;

namespace abc_bank_tests.MockObjects
{
    public class MockDateProvider : IDateProvider
    {
        private static MockDateProvider _instance = null;
        private DateTime _current = new DateTime();

        public static MockDateProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MockDateProvider();
                return _instance;
            }
        }

        public void PresetDate(DateTime date)
        {
            _current = date;
        }

        public DateTime Now()
        {
            return _current;
        }
    }
}
