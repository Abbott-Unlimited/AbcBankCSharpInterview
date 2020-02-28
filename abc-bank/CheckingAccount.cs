using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CheckingAccount : Account
    {
        protected override string GetAccountTypeString()
        {
            return "Checking Account";
        }

        public override double GetInterestEarned()
        {
            return GetAccountBalance() * 0.001;
        }
    }
}
