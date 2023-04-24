using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CheckingAccount : Account
    {
        //public override string AccountName { get { return "Checking Account"; }  }
        public override string AccountName { get => "Checking Account"; }

        public override decimal GetInterestEarned() 
        {
            // Checking accounts have a flat rate of 0.1 %.
            const decimal interestRate1 = 0.001m;

            decimal amount = SumTransactions();

            return amount * interestRate1;
        }
    }
}
