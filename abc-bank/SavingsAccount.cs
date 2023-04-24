using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class SavingsAccount : Account
    {
        //public override string AccountName { get { return "Savings Account"; } }
        public override string AccountName { get => "Savings Account"; }

        public override decimal GetInterestEarned() 
        {
            // Savings accounts have a rate of 0.1 % for the first $1, 000 then 0.2 %.
            const decimal interestRate1 = 0.001m;
            const decimal interestRate2 = 0.002m;

            decimal amount = SumTransactions();
            if (amount <= 1000)
            {
                decimal earnedRate1 = 1000 * interestRate1;
                
                return earnedRate1;
            }
            else
            {
                decimal earnedRate1 = 1000 * interestRate1;
                decimal earnedRate2 = (amount - 1000) * interestRate2;

                return earnedRate1 + earnedRate2;
            }
        }
    }
}
