using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Accounts
{
    public class CheckingAccount : Account
    {
        public override double InterestEarned()
        {
            double amount = SumTransactions();
            //            case SUPER_SAVINGS:
            //                if (amount <= 4000)
            //                    return 20;
            return amount * 0.001;
        }

        public override AccountType GetAccountType()
        {
            return AccountType.Checking;
        }
    }
}
