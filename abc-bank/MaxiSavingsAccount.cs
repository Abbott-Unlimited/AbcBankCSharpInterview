using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingsAccount : Account
    {
        //public override string AccountName { get { return "Maxi Savings Account"; } }
        public override string AccountName { get => "Maxi Savings Account"; }

        public override decimal GetInterestEarned() 
        {
            // Maxi - Savings accounts have a rate of 2 % for the first $1, 000 then 5 % for the next $1, 000 then 10 %.
            const decimal interestRate1 = 0.002m;
            const decimal interestRate2 = 0.005m;
            const decimal interestRate3 = 0.010m;

            decimal amount = SumTransactions();

            if (amount <= 1000)
            {
                decimal earnedRate1 = amount * interestRate1;
                
                return earnedRate1;
            }
            else if (amount <= 2000)
            {
                decimal earnedRate1 = 1000 * interestRate1;
                decimal earnedRate2 = (amount - 1000) * interestRate2;
                
                return earnedRate1 + earnedRate2;
            }
            else
            {
                decimal earnedRate1 = 1000 * interestRate1;
                decimal earnedRate2 = 1000 * interestRate2;
                decimal earnedRate3 = (amount - 2000) * interestRate3;

                return earnedRate1 + earnedRate2 + earnedRate3;
            }
        }
    }
}
