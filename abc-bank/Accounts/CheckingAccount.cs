using abc_bank.Utilities.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class CheckingAccount : Account
    {
        private const double MAX_RATE = .001;

        public override AccountType GetAccountType()
        {
            return AccountType.Checking;
        }

        protected override double GetCurrentRate(DateTime latestWithdrawDate, DateTime currentDate)
        {
            return MAX_RATE;
        }
    }
}
