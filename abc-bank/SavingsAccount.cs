using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class SavingsAccount : Account
    {
        protected override string GetAccountTypeString()
        {
            return "Savings Account";
        }

        public override double GetInterestEarned()
        {
            double amount = GetAccountBalance();
            if (amount <= 1000)
                return amount * 0.001;
            else
                return 1 + (amount - 1000) * 0.002;
        }
    }
}
