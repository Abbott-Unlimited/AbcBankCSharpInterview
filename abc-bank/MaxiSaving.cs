using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingAccount : Account
    {
        public override AccountType AccountType { get; } = AccountType.MAXI_SAVINGS;

        public override double InterestEarned()
        {
            if (transactions.OrderByDescending(t => t.transactionDate).FirstOrDefault().transactionDate < DateTime.Now.AddDays(-10))
                return sumTransactions() * 0.05;
            else
                return sumTransactions() * 0.001;
        }
    }
}
