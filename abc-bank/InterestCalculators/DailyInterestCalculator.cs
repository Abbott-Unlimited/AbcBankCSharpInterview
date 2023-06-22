using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestCalculators
{
    public class DailyInterestCalculator
    {
        public static DailyInterestCalculator Instance { get; private set; }
        static DailyInterestCalculator()
        {
            if (Instance == null)
            {
                Instance = new DailyInterestCalculator();
            }
        }
        public double Calculate(double amount, double apr)
        {
            var dailyRate = apr / 365;
            return amount * dailyRate;
        }
    }
}
