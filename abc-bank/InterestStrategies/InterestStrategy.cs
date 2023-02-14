using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestStrategies
{
    public abstract class InterestStrategy
    {
        public abstract double CalculateInterest(List<Transaction> transactions);
    }
}
