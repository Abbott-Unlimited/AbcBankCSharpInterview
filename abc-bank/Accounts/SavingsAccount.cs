using abc_bank.Utilities.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class SavingsAccount : Account
    {
        private const double MAX_RATE = .002;
        private const double MIN_RATE = .001;

        public override AccountType GetAccountType()
        {
            return AccountType.Savings;
        }

        protected override double GetCurrentRate(DateTime latestWithdrawDate, DateTime currentDate)
        {
            double amount = SumTransactions();
            double rate = MIN_RATE;

            // have a rate of MIN_RATE for the first $1,000 then MAX_RATE
            if (amount > 1000.0)
                rate = MAX_RATE;

            return rate;
        }
    }
}
