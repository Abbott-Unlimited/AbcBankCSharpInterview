using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class MaxiSavingsAccount : Account
    {
        public override double InterestEarned()
        {
            double amount = SumTransactions();

            if (amount <= 1000)
                amount *= 0.02;
            else if (amount <= 2000)
                amount = 20 + (amount - 1000) * 0.05;
            else 
                amount = 70 + (amount - 2000) * 0.1;

            return amount;
        }

        public override AccountType GetAccountType()
        {
            return AccountType.MaxiSavings;
        }
    }
}
