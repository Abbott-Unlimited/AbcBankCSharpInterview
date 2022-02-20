using abc_bank.Utilities.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class MaxiSavingsAccount : Account
    {
        // number of days since last withdrawal for max interest accrual
        private const int DAYS_DELTA = 10;

        // rate to be used if last withdrawal was more than 10 days ago
        private const double MAX_RATE = .05;

        // rate to be used if last withdrawal was less than 10 days ago
        private const double MIN_RATE = 0.001;

        protected override double GetCurrentRate(DateTime latestWithdrawDate, DateTime currentDate)
        {
            double currentInterestRate = 0.0;
            TimeSpan daysDelta = new TimeSpan(DAYS_DELTA, 0, 0, 0);

            if (latestWithdrawDate != default(DateTime))
            {
                if (currentDate - latestWithdrawDate < daysDelta)
                {
                    currentInterestRate = MIN_RATE;
                }
                else
                {
                    currentInterestRate = MAX_RATE;
                }
            }
            else
            {
                // assume max rate is the starting rate of the account
                currentInterestRate = MAX_RATE;
            }

            return currentInterestRate;
        }

        public override AccountType GetAccountType()
        {
            return AccountType.MaxiSavings;
        }
    }
}
