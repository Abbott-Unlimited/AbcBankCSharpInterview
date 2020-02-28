using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingsAccount : Account
    {
        protected override string GetAccountTypeString()
        {
            return "Maxi-Savings Account";
        }

        public override double GetInterestEarned()
        {
            double amount = GetAccountBalance();
            if (amount <= 1000)
                return amount * 0.02;
            if (amount <= 2000)
                return 20 + (amount - 1000) * 0.05;
            return 70 + (amount - 2000) * 0.1;
        }
    }
}
