using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestStrategies
{
    public class InterestContext
    {
        InterestStrategy interestStrategy;
        public void SetInterestStrategy(InterestStrategy strategy)
        {
            interestStrategy = strategy;
        }
        public double Calculate(List<Transaction> transactions)
        {
            return interestStrategy.CalculateInterest(transactions);
        }

    }
}
