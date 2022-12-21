using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Utilities
{
    public class BankValues
    {
        public const double POINT_ONE_PERCENT_INTEREST = 0.001;
        public const double POINT_TWO_PERCENT_INTEREST = 0.002;
        public const double TWO_PERCENT_INTEREST = 0.02;
        public const double FIVE_PERCENT_INTEREST = 0.05;
        public const double TEN_PERCENT_INTEREST = 0.1;

        public enum AccountType
        {
            CHECKING = 0,
            SAVINGS = 1,
            MAXI_SAVINGS = 2
        }



    }
}
