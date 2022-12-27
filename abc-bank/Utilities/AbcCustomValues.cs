using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcCompanyEstablishmentApp.Utilities
{
    public class AbcCustomValues
    {
        public const decimal POINT_ONE_PERCENT_INTEREST = 0.001M;
        public const decimal POINT_TWO_PERCENT_INTEREST = 0.002M;
        public const decimal TWO_PERCENT_INTEREST = 0.001M;
        public const decimal FIVE_PERCENT_INTEREST = 0.001M;
        public const decimal TEN_PERCENT_INTEREST = 0.001M;

        public enum AccountType
        {
            CHECKING = 0,
            SAVINGS = 1,
            MAXI_SAVINGS = 2
        }

        public enum TransactionType
        {
            DEPOSIT = 0,
            WITHDRAWAL = 1
        }

        public enum EstablishmentType
        {
            BANK = 0,
            RENTAL_AGENCY = 1,
            LOAN_ORIGINATION_FIRM = 2,
            LOAN_SERVICER = 3
        }
    }
}
