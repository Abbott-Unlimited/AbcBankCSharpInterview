using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.InterestStrategies
{
    public class MaxiInterest : InterestStrategy
    {
        private readonly double YearlyInterestRateTier1 = 0.000003; //0.1% simple interest to daily
        private readonly double YearlyInterestRateTier2 = 0.000137; //0.5% simple interest to daily
        public override double CalculateInterest(List<Transaction> transactions)
        {
            DateTime prevDate = transactions.First().transactionDate;
            var accountTotal = 0.0;
            var interestTotal = 0.0;
            //Change Maxi-Savings accounts to have an interest rate of 5% assuming no withdrawals in the past 10 days otherwise 0.1%.
            foreach(var transaction in transactions)
            {
                accountTotal += transaction.amount;
                var days = (transaction.transactionDate - prevDate).Days;
                if(transaction.transactionType == TransactionType.WITHDRAWEL && days >= 10)
                {
                    interestTotal += accountTotal * YearlyInterestRateTier1 * (double)days;
                    accountTotal += interestTotal;
                }
                else
                {
                    interestTotal += accountTotal * YearlyInterestRateTier2 * (double)days;
                    accountTotal += interestTotal;
                }
                prevDate = transaction.transactionDate;
            }
            return AccountingLogicHelpers.ReturnCleanValue(interestTotal);
        }
    }
}
