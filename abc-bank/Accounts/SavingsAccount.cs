using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class SavingsAccount : Account
    {
        public override double InterestEarned()
        {
            double amount = SumTransactions();

            if (amount <= 1000.0)
                amount *= 0.001;
            else
                amount = 1 + (amount - 1000) * 0.002;

            return amount;
        }

        public override AccountType GetAccountType()
        {
            return AccountType.Savings;
        }
    }
}
