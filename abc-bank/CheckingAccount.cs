using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CheckingAccount : Account
    {
        public override AccountType AccountType { get; } = AccountType.CHECKING;

        public override double InterestEarned()
        {
            return base.sumTransactions() * 0.001;
        }
    }
}
